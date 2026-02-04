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
        private static SerialPort port_COM7; // Делаем статическим, чтобы был единственным для всех экземпляров
        private bool continueReading = true;
        private List<string> dataList = new List<string>();
        private Thread readThread;
        private string selectedLine;

        // Конструктор с параметром для передачи выбранной линии
        public AuthorizationForm(string line)
        {
            InitializeComponent();
            selectedLine = line;
            InitializeSerialPort();
            GetIPAddress();
            SetTimer();
            SetLineLabel();
        }

        // Старый конструктор
        public AuthorizationForm() : this("") { }

        private void SetLineLabel()
        {
            labelLineResultAuth.Text = selectedLine;
        }

        public void Read()
        {
            while (continueReading)
            {
                try
                {
                    string message = port_COM7.ReadLine();

                    string hexValue = message.Replace(" ", String.Empty);
                    hexValue = hexValue.Substring(0, hexValue.IndexOf(","));
                    hexValue = hexValue.Substring(hexValue.LastIndexOf("=") + 1);

                    // Используем выбранную линию в URL
                    string urlEmplInfo = $"http://192.168.77.74:8181/operators/checkCard?line={selectedLine}&codekey={hexValue}";

                    // Асинхронно получаем данные
                    var responseTask = GetJSONFromURL(urlEmplInfo);
                    responseTask.Wait(); // Блокируем поток до получения ответа

                    if (!string.IsNullOrEmpty(responseTask.Result))
                    {
                        JsonDocument jsonDocEmployee = JsonDocument.Parse(responseTask.Result);
                        JsonElement rootEmployee = jsonDocEmployee.RootElement;
                        string name_value_empl = rootEmployee.GetProperty("name").GetString();
                        string department_value_empl = rootEmployee.GetProperty("department").GetString();
                        string position_value_empl = rootEmployee.GetProperty("pos").GetString();

                        this.Invoke(new Action(() =>
                        {
                            EmployeeInfoForm employeeInfoForm = new EmployeeInfoForm();
                            this.Hide();
                            employeeInfoForm.Show();

                            // Передаем данные в следующую форму
                            employeeInfoForm.NameTextBox.Text = name_value_empl;
                            employeeInfoForm.DepartmentTextBox.Text = department_value_empl;
                            employeeInfoForm.PositionTextBox.Text = position_value_empl;
                        }));

                        // После успешного открытия формы прекращаем чтение
                        continueReading = false;
                    }
                }
                catch (TimeoutException)
                {
                    // Игнорируем таймауты, продолжаем чтение
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }));
                    continueReading = false;
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
            // Проверяем, открыт ли уже порт
            if (port_COM7 != null && port_COM7.IsOpen)
            {
                // Если порт уже открыт, закрываем его перед повторным использованием
                try
                {
                    port_COM7.Close();
                }
                catch { }
            }

            // Создаем новый экземпляр порта
            port_COM7 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);
            port_COM7.Handshake = Handshake.None;
            port_COM7.ReadTimeout = 500;
            port_COM7.WriteTimeout = 500;

            try
            {
                port_COM7.Open();
                continueReading = true;

                // Останавливаем старый поток, если он существует
                if (readThread != null && readThread.IsAlive)
                {
                    continueReading = false;
                    readThread.Join(500);
                }

                // Создаем новый поток для чтения
                readThread = new Thread(Read);
                readThread.IsBackground = true;
                readThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия порта COM7: {ex.Message}");
                continueReading = false;
            }
        }

        private void SetTimer()
        {
            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
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
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (address.ToString() != "::1")
                    {
                        return address.ToString();
                    }
                    else { return null; }
                }
            }
            return "IP-адрес не найден";
        }

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();

            // Закрываем порт перед переходом
            if (port_COM7 != null && port_COM7.IsOpen)
            {
                continueReading = false;
                if (readThread != null && readThread.IsAlive)
                {
                    readThread.Join(500);
                }
                port_COM7.Close();
            }

            this.Hide();
            adminForm.Show();
        }

        // Переопределяем метод закрытия формы для корректного освобождения ресурсов
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Останавливаем поток и закрываем порт при закрытии формы
            continueReading = false;

            if (readThread != null && readThread.IsAlive)
            {
                readThread.Join(1000);
            }

            if (port_COM7 != null && port_COM7.IsOpen)
            {
                port_COM7.Close();
            }
        }
    }
}