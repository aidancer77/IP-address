using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    public partial class AuthorizationForm : Form
    {
        private SerialPort port_COM7;
        private bool continueReading = true;
        private Thread readThread;
        private string selectedLine; // Храним выбранную линию
        private bool isPortInitialized = false;

        // Конструктор с параметром для передачи выбранной линии
        public AuthorizationForm(string line)
        {
            InitializeComponent();
            selectedLine = line;
            InitializeSerialPort();
            GetIPAddress();
            SetTimer();
            SetLineLabel(); // Устанавливаем значение в label
        }

        // Старый конструктор (если нужен для обратной совместимости)
        public AuthorizationForm() : this("") { }

        private void SetLineLabel()
        {
            if (labelLineResultAuth != null)
            {
                labelLineResultAuth.Text = selectedLine;
            }
        }

        public void Read()
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

                    if (labelLineResultAuth.Text == "")
                    {
                        MessageBox.Show("Пожалуйста, выберите линию", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string hexValue = message.Replace(" ", String.Empty);
                        hexValue = hexValue.Substring(0, hexValue.IndexOf(","));
                        hexValue = hexValue.Substring(hexValue.LastIndexOf("=") + 1);

                        // Используем выбранную линию в URL
                        string urlEmplInfo = $"http://192.168.77.74:8181/operators/checkCard?line=4&codekey={hexValue}";

                        // Получаем данные асинхронно
                        string textBoxEmplInfo = GetJSONFromURL(urlEmplInfo).GetAwaiter().GetResult();

                        if (!string.IsNullOrEmpty(textBoxEmplInfo))
                        {
                            JsonDocument jsonDocEmployee = JsonDocument.Parse(textBoxEmplInfo);
                            JsonElement rootEmployee = jsonDocEmployee.RootElement;
                            string name_value_empl = rootEmployee.GetProperty("name").GetString();
                            string department_value_empl = rootEmployee.GetProperty("department").GetString();
                            string position_value_empl = rootEmployee.GetProperty("pos").GetString();

                            this.Invoke(new Action(() =>
                            {
                                // Создаем новую форму EmployeeInfoForm с данными
                                EmployeeInfoForm employeeInfoForm = new EmployeeInfoForm();
                                employeeInfoForm.NameTextBox.Text = name_value_empl;
                                employeeInfoForm.DepartmentTextBox.Text = department_value_empl;
                                employeeInfoForm.PositionTextBox.Text = position_value_empl;

                                this.Hide();
                                employeeInfoForm.Show();
                            }));
                        }
                    }

                    // После успешного открытия формы прекращаем чтение
                    continueReading = false;
                }
                catch (TimeoutException)
                {
                    // Игнорируем таймауты - это нормально для последовательного порта
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Ошибка чтения карты: {ex.Message}");
                    }));
                    Thread.Sleep(1000);
                }
            }
        }

        private static async Task<string> GetJSONFromURL(string urlJSON)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(urlJSON);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Ошибка HTTP: {e.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка: {ex.Message}");
                    return null;
                }
            }
        }

        private void InitializeSerialPort()
        {
            try
            {
                if (port_COM7 != null && port_COM7.IsOpen)
                {
                    port_COM7.Close();
                    port_COM7.Dispose();
                }

                port_COM7 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);
                port_COM7.Handshake = Handshake.None;
                port_COM7.ReadTimeout = 500;
                port_COM7.WriteTimeout = 500;
                port_COM7.Open();

                continueReading = true;
                isPortInitialized = true;

                if (readThread != null && readThread.IsAlive)
                {
                    readThread.Join(100);
                }

                readThread = new Thread(Read);
                readThread.IsBackground = true;
                readThread.Start();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Доступ к порту COM7 запрещен: {ex.Message}\n" +
                              "Возможно порт занят другой программой.");
                isPortInitialized = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации COM-порта: {ex.Message}");
                isPortInitialized = false;
            }
        }

        private void SetTimer()
        {
            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
            FormTimer.Interval = 1000; // Обновляем каждую секунду
            FormTimer.Tick += (s, e) =>
            {
                labelDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            };
            FormTimer.Start();
        }
        private void GetIPAddress()
        {
            string localIP = GetLocalIPv4Address();
            labelIPValue.Text = localIP;
        }
        private string GetLocalIPv4Address()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.ToString() != "127.0.0.1") // Исправлено с "::1" на "127.0.0.1"
                        {
                            return address.ToString();
                        }
                    }
                }
                return "IP-адрес не найден";
            }
            catch (Exception)
            {
                return "Ошибка получения IP";
            }
        }

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
        {
            // Закрываем порт перед переходом
            CloseSerialPort();

            AdminForm adminForm = new AdminForm();
            this.Hide();
            adminForm.Show();
        }

        private void CloseSerialPort()
        {
            continueReading = false;

            if (readThread != null && readThread.IsAlive)
            {
                readThread.Join(500); // Даем потоку время на завершение
            }

            if (port_COM7 != null)
            {
                if (port_COM7.IsOpen)
                {
                    try
                    {
                        port_COM7.Close();
                    }
                    catch (Exception) { }
                }
                port_COM7.Dispose();
                port_COM7 = null;
            }
        }

        // Обработчик события закрытия формы
        //private void AuthorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    CloseSerialPort();
        //}

        //// При возвращении на форму заново открываем порт
        //private void AuthorizationForm_VisibleChanged(object sender, EventArgs e)
        //{
        //    if (this.Visible && !isPortInitialized)
        //    {
        //        InitializeSerialPort();
        //    }
        //}
    }
}