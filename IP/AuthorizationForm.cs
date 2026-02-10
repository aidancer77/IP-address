using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    public partial class AuthorizationForm : Form
    {
        private string selectedLineName;
        private int selectedLineId;
        private bool continueReading = true;

        public AuthorizationForm(string lineName, int lineId)
        {
            InitializeComponent();

            selectedLineName = lineName;
            selectedLineId = lineId;

            LoadLineInfoFromJson();
            GetIPAddress();
            SetTimer();
            SetLineLabel();
        }

        public AuthorizationForm() : this("", 0) { }

        private void SetLineLabel()
        {
            if (labelLineResultAuth != null)
            {
                labelLineResultAuth.Text = selectedLineName;
            }
        }

        private LineInfo LoadLineInfoFromJson()
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                if (!File.Exists(jsonFilePath))
                {
                    File.Delete(jsonFilePath);
                }

                var defaultSettings = new
                {
                    Admin = "220832",
                    Server = "192.168.77.74:8181",
                    Timer = 5,
                    LineId = selectedLineId,
                    LineName = selectedLineName
                };

                // Используем Newtonsoft.Json для простоты
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(defaultSettings,
                    Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(jsonFilePath, jsonString, System.Text.Encoding.UTF8);

                string jsonContent = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<LineInfo>(jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки настроек линии: {ex.Message}");
                return null;
            }
        }

        private void SetTimer()
        {
            Timer FormTimer = new Timer();
            FormTimer.Interval = 1000;
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
                        if (address.ToString() != "127.0.0.1")
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

        public void ButtonLabelPassword_Click(object sender, EventArgs e)
        {
            // Останавливаем чтение, но не закрываем порт
            SerialPortManager.StopReading();

            AdminForm adminForm = new AdminForm();
            this.Hide();
            adminForm.Show();
        }

        private void ButtonLabelCard_Click(object sender, EventArgs e)
        {
            // Сначала проверяем, не занят ли порт
            if (SerialPortManager.IsPortOpen)
            {
                // Если порт уже открыт, останавливаем чтение и закрываем
                SerialPortManager.StopReading();
                SerialPortManager.ClosePort();

                // Даем время порту освободиться
                System.Threading.Thread.Sleep(200);
            }

            try
            {
                // Создаем EmployeeInfoForm с выбранной линией
                EmployeeInfoForm employeeForm = new EmployeeInfoForm(selectedLineName);

                // Инициализируем COM-порт с callback-ом от новой формы
                bool portInitialized = SerialPortManager.InitializePort(employeeForm.ProcessCardData);

                if (!portInitialized)
                {
                    // Если не удалось инициализировать порт, все равно показываем форму
                    MessageBox.Show("Не удалось инициализировать COM-порт. Чтение карт будет недоступно.",
                                  "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Console.WriteLine("COM-порт успешно инициализирован для чтения карт");
                }

                this.Hide();
                employeeForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании формы: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}