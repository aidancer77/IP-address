using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Ports;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using static IP.LineInfo;

namespace IP
{
    public static class SerialPortManager
    {
        public static JsonDocument JsonDocCOMPort { get; set; }
        public static JsonElement RootCOMPort { get; set; }
        public static string ComPort { get; set; }

        private static SerialPort port_COM_Num = null;
        private static bool isPortInitialized = false;
        private static Thread readThread = null;
        private static bool continueReading = false;
        private static Action<string> onDataReceived = null;
        private static object lockObject = new object();
        private static bool isDisposing = false;

        public static bool InitializePort(Action<string> dataReceivedCallback = null)
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                // Проверяем существование файла
                if (!File.Exists(jsonFilePath))
                {
                    Console.WriteLine("Файл lineinfo.json не найден. Создание настроек по умолчанию...");

                    // Создаем временные настройки по умолчанию
                    var defaultSettings = new
                    {
                        COMNum = "COM7",
                        Server = "localhost",
                        LineId = 1,
                        LineName = "Линия 1"
                    };

                    string defaultJson = Newtonsoft.Json.JsonConvert.SerializeObject(defaultSettings, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFilePath, defaultJson, System.Text.Encoding.UTF8);
                }

                string jsonContent = File.ReadAllText(jsonFilePath);
                JsonDocCOMPort = JsonDocument.Parse(jsonContent);
                RootCOMPort = JsonDocCOMPort.RootElement;

                // Безопасное получение значения COMNum
                if (RootCOMPort.TryGetProperty("COMNum", out JsonElement comElement))
                {
                    ComPort = comElement.GetString();
                }
                else
                {
                    Console.WriteLine("В файле lineinfo.json не найдено поле COMNum. Используется COM7 по умолчанию");
                    ComPort = "COM7";
                }

                JsonDocCOMPort = JsonDocument.Parse(jsonContent);
                RootCOMPort = JsonDocCOMPort.RootElement;
                ComPort = RootCOMPort.GetProperty("COMNum").GetString();

                if (ComPort == "COM7")
                {
                    try
                    {
                        if (port_COM_Num != null && port_COM_Num.IsOpen)
                        {
                            onDataReceived = dataReceivedCallback;

                            if (readThread == null || !readThread.IsAlive)
                            {
                                StartReading();
                            }

                            Console.WriteLine("COM-порт уже инициализирован и открыт");
                            return true;
                        }

                        ClosePortInternal();

                        port_COM_Num = new SerialPort(ComPort, 9600, Parity.None, 8, StopBits.One)
                        //port_COM_Num = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One)
                        {
                            Handshake = Handshake.None,
                            ReadTimeout = 1000,
                            WriteTimeout = 1000,
                            RtsEnable = true,
                            DtrEnable = true,
                            NewLine = "\r\n"
                        };

                        port_COM_Num.Open();
                        port_COM_Num.DiscardInBuffer();
                        port_COM_Num.DiscardOutBuffer();

                        isPortInitialized = true;
                        onDataReceived = dataReceivedCallback;
                        isDisposing = false;

                        StartReading();

                        Console.WriteLine("COM-порт успешно инициализирован");
                        return true;
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Доступ к порту COM-порт запрещен: {ex.Message}\n" +
                                      "Возможно порт занят другой программой.\n" +
                                      "Закройте все программы, использующие COM-порт, и попробуйте снова.");
                        return false;
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Порт COM-порт занят или недоступен: {ex.Message}\n" +
                                      "Подождите несколько секунд и попробуйте снова.");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка инициализации COM-порта: {ex.Message}");
                        return false;
                    }
                }

                MessageBox.Show("Неверный COM-порт. Пожалуйста, выберите другой в настройках");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка при инициализации порта: {ex.Message}");
                return false;
            }
        }

        private static void StartReading()
        {
            lock (lockObject)
            {
                if (readThread != null && readThread.IsAlive)
                {
                    continueReading = false;

                    if (!readThread.Join(1000))
                    {
                        Console.WriteLine("Поток чтения не завершился, продолжаем работу");
                    }
                    readThread = null;
                }

                continueReading = true;
                readThread = new Thread(ReadFromPort)
                {
                    IsBackground = true,
                    Name = "COM-ReadThread"
                };
                readThread.Start();
            }
        }

        private static void ReadFromPort()
        {
            while (continueReading && !isDisposing)
            {
                try
                {
                    if (port_COM_Num == null || !port_COM_Num.IsOpen || isDisposing)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    if (port_COM_Num.BytesToRead > 0)
                    {
                        string message = port_COM_Num.ReadLine().Trim();

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
                    Console.WriteLine("Порт закрыт, завершаем поток чтения");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка чтения из порта: {ex.Message}");
                    Thread.Sleep(100);
                }
            }

            Console.WriteLine("Поток чтения COM_Num завершен");
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
                StopReading();

                if (port_COM_Num != null)
                {
                    if (port_COM_Num.IsOpen)
                    {
                        port_COM_Num.DiscardInBuffer();
                        port_COM_Num.DiscardOutBuffer();
                        port_COM_Num.Close();
                        Console.WriteLine("COM-порт закрыт");
                    }

                    port_COM_Num.Dispose();
                    port_COM_Num = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при закрытии порта: {ex.Message}");
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
                Console.WriteLine("COM_Num порт полностью закрыт и освобожден");
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
        public static bool IsPortOpen => port_COM_Num != null && port_COM_Num.IsOpen;
    }
}