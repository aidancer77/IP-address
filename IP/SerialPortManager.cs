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
        private static object lockObject = new object();
        private static bool isDisposing = false;

        public static bool InitializePort(Action<string> dataReceivedCallback = null)
        {
            lock (lockObject)
            {
                try
                {
                    // Если порт уже инициализирован и открыт
                    if (port_COM7 != null && port_COM7.IsOpen)
                    {
                        onDataReceived = dataReceivedCallback;

                        // Убеждаемся, что поток чтения работает
                        if (readThread == null || !readThread.IsAlive)
                        {
                            StartReading();
                        }

                        Console.WriteLine("COM7 порт уже инициализирован и открыт");
                        return true;
                    }

                    ClosePortInternal();

                    port_COM7 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One)
                    {
                        Handshake = Handshake.None,
                        ReadTimeout = 1000,
                        WriteTimeout = 1000,
                        RtsEnable = true,
                        DtrEnable = true,
                        NewLine = "\r\n"
                    };

                    port_COM7.Open();
                    port_COM7.DiscardInBuffer();
                    port_COM7.DiscardOutBuffer();

                    isPortInitialized = true;
                    onDataReceived = dataReceivedCallback;
                    isDisposing = false;

                    // Запускаем поток чтения
                    StartReading();

                    MessageBox.Show("COM7 порт успешно инициализирован");
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
                // Останавливаем старый поток, если он работает
                if (readThread != null && readThread.IsAlive)
                {
                    continueReading = false;

                    // Ждем завершения потока
                    if (!readThread.Join(1000))
                    {
                        Console.WriteLine("Поток чтения не завершился, продолжаем работу");
                    }
                    readThread = null;
                }

                continueReading = true;
                readThread = new Thread(ReadFromPort);
                readThread.IsBackground = true;
                readThread.Name = "COM7-ReadThread";
                readThread.Start();
            }
        }

        private static void ReadFromPort()
        {
            while (continueReading && !isDisposing)
            {
                try
                {
                    if (port_COM7 == null || !port_COM7.IsOpen || isDisposing)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    // Проверяем, есть ли данные для чтения
                    if (port_COM7.BytesToRead > 0)
                    {
                        string message = port_COM7.ReadLine().Trim();

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
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
                catch (TimeoutException)
                {
                    Thread.Sleep(50);
                }
                catch (InvalidOperationException)
                {
                    // Порт был закрыт - выходим из цикла
                    MessageBox.Show("Порт закрыт, завершаем поток чтения");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка чтения из порта: {ex.Message}");
                    Thread.Sleep(100);
                }
            }

            Console.WriteLine("Поток чтения COM7 завершен");
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
                        Console.WriteLine("Поток чтения не завершился в указанное время");
                    }
                    readThread = null;
                }
            }
        }

        private static void ClosePortInternal()
        {
            try
            {
                // Сначала останавливаем чтение
                StopReading();

                if (port_COM7 != null)
                {
                    if (port_COM7.IsOpen)
                    {
                        port_COM7.DiscardInBuffer();
                        port_COM7.DiscardOutBuffer();
                        port_COM7.Close();
                        MessageBox.Show("COM7 порт закрыт");
                    }

                    port_COM7.Dispose();
                    port_COM7 = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при закрытии порта: {ex.Message}");
            }
        }

        public static void ClosePort()
        {
            lock (lockObject)
            {
                isDisposing = true;
                onDataReceived = null;
                isPortInitialized = false;

                ClosePortInternal();

                isDisposing = false;
                Console.WriteLine("COM7 порт полностью закрыт и освобожден");
            }
        }

        public static bool ReinitializePort(Action<string> dataReceivedCallback = null)
        {
            lock (lockObject)
            {
                ClosePort();
                Thread.Sleep(500); // Даем время порту освободиться
                return InitializePort(dataReceivedCallback);
            }
        }

        public static bool IsPortInitialized => isPortInitialized;
        public static bool IsPortOpen => port_COM7 != null && port_COM7.IsOpen;
    }
}