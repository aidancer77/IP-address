using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace IP
{
    public static class SerialPortManager
    {
        private static SerialPort port_COM7 = null;
        private static bool isPortInitialized = false;
        private static Thread readThread = null;
        private static bool continueReading = false;
        private static Action<string> onDataReceived = null;
        private static object lockObject = new object(); // Добавляем lock для потокобезопасности

        public static bool InitializePort(Action<string> dataReceivedCallback = null)
        {
            lock (lockObject)
            {
                try
                {
                    if (isPortInitialized && port_COM7 != null && port_COM7.IsOpen)
                    {
                        // Порт уже открыт, просто обновляем колбэк
                        onDataReceived = dataReceivedCallback;
                        return true;
                    }

                    // Закрываем старый порт, если он существует
                    ClosePort();

                    // Создаем новый порт
                    port_COM7 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One)
                    {
                        Handshake = Handshake.None,
                        ReadTimeout = 500,
                        WriteTimeout = 500,
                        RtsEnable = true, // Добавляем для стабильности
                        DtrEnable = true  // Добавляем для стабильности
                    };

                    port_COM7.Open();
                    isPortInitialized = true;
                    onDataReceived = dataReceivedCallback;

                    // Запускаем поток чтения
                    StartReading();
                    return true;
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Доступ к порту COM7 запрещен: {ex.Message}\n" +
                                  "Возможно порт занят другой программой.\n" +
                                  "Закройте все программы, использующие COM7, и попробуйте снова.");
                    return false;
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Порт COM7 занят или недоступен: {ex.Message}\n" +
                                  "Подождите несколько секунд и попробуйте снова.");
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка инициализации COM-порта: {ex.Message}");
                    return false;
                }
            }
        }

        private static void StartReading()
        {
            lock (lockObject)
            {
                continueReading = true;

                // Останавливаем старый поток, если он работает
                if (readThread != null && readThread.IsAlive)
                {
                    continueReading = false;
                    if (!readThread.Join(500))
                    {
                        readThread.Abort(); // Принудительно прерываем, если не завершается
                    }
                }

                readThread = new Thread(ReadFromPort);
                readThread.IsBackground = true;
                readThread.Name = "COM7-ReadThread";
                readThread.Start();
            }
        }

        private static void ReadFromPort()
        {
            while (continueReading)
            {
                try
                {
                    if (port_COM7 == null || !port_COM7.IsOpen)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    string message = port_COM7.ReadLine();

                    // Вызываем колбэк, если он установлен
                    if (onDataReceived != null && !string.IsNullOrEmpty(message))
                    {
                        try
                        {
                            onDataReceived.Invoke(message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка в колбэке обработки данных: {ex.Message}");
                        }
                    }
                }
                catch (TimeoutException)
                {
                    Thread.Sleep(100);
                }
                catch (InvalidOperationException)
                {
                    // Порт был закрыт
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка чтения из порта: {ex.Message}");
                    Thread.Sleep(100);
                }
            }
        }

        public static void StopReading()
        {
            lock (lockObject)
            {
                continueReading = false;

                if (readThread != null && readThread.IsAlive)
                {
                    if (!readThread.Join(500))
                    {
                        readThread.Abort();
                    }
                }
            }
        }

        public static void ClosePort()
        {
            lock (lockObject)
            {
                StopReading();

                if (port_COM7 != null)
                {
                    try
                    {
                        if (port_COM7.IsOpen)
                        {
                            port_COM7.Close();
                            Console.WriteLine("COM7 порт закрыт");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка закрытия порта: {ex.Message}");
                    }

                    try
                    {
                        port_COM7.Dispose();
                    }
                    catch { }

                    port_COM7 = null;
                }

                isPortInitialized = false;
                onDataReceived = null;
            }
        }

        public static bool IsPortInitialized => isPortInitialized;
        public static bool IsPortOpen => port_COM7 != null && port_COM7.IsOpen;

        // Новый метод для переподключения
        public static bool ReinitializePort(Action<string> dataReceivedCallback = null)
        {
            lock (lockObject)
            {
                ClosePort();
                Thread.Sleep(500); // Даем время порту освободиться
                return InitializePort(dataReceivedCallback);
            }
        }
    }
}