using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace IP
{
    public partial class AuthorizationForm : Form
    {
        private string selectedLineName;
        private int selectedLineId;
        private bool continueReading = true;
        private bool isFormClosing = false;

        public AuthorizationForm()
        {
            InitializeComponent();

            this.FormClosing += AuthorizationForm_FormClosing;
            this.FormClosed += AuthorizationForm_FormClosed;

            LoadLineInfoFromJson();
            GetIPAddress();
            SetTimer();
            SetLineLabel();
        }

        private void AuthorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            // Не закрываем порт полностью, только останавливаем чтение
            SerialPortManager.StopReading();
        }

        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isFormClosing = true;
        }

        private void SetLineLabel()
        {
            if (labelLineResultAuth != null)
            {
                labelLineResultAuth.Text = selectedLineName ?? "";
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
                    var defaultSettings = new
                    {
                        Admin = "220832",
                        Server = "192.168.77.74:8181",
                        Timer = 5,
                        LineId = 0,
                        LineName = "",
                        COMNum = "COM7"
                    };

                    string jsonString = JsonConvert.SerializeObject(defaultSettings, Formatting.Indented);
                    File.WriteAllText(jsonFilePath, jsonString, System.Text.Encoding.UTF8);
                }

                // Читаем и десериализуем файл
                string jsonContent = File.ReadAllText(jsonFilePath);
                var settings = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                selectedLineId = (int)(settings.LineId ?? 0);
                selectedLineName = (string)(settings.LineName ?? "");

                return JsonConvert.DeserializeObject<LineInfo>(jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки настроек линии: {ex.Message}");
                selectedLineName = "";
                selectedLineId = 0;
                return null;
            }
        }

        private void SetTimer()
        {
            Timer FormTimer = new Timer();
            FormTimer.Interval = 1000;
            FormTimer.Tick += (s, e) =>
            {
                if (labelDateTime != null && !isFormClosing && this.IsHandleCreated)
                    labelDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            };
            FormTimer.Start();
        }
        private void GetIPAddress()
        {
            string localIP = GetLocalIPv4Address();
            if (labelIPValue != null)
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
            SerialPortManager.StopReading();

            AdminForm adminForm = new AdminForm();
            this.Hide();
            adminForm.Show();
        }

        private void ButtonLabelCard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedLineName) && selectedLineId > 0)
            {
                try
                {
                    EmployeeInfoForm employeeForm = new EmployeeInfoForm(selectedLineName);

                    // Инициализируем COM-порт с callback-ом от новой формы
                    bool portInitialized = SerialPortManager.InitializePort(employeeForm.ProcessCardData);

                    if (!portInitialized)
                    {
                        Console.WriteLine("Не удалось инициализировать COM-порт. Чтение карт будет недоступно.",
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
                    Console.WriteLine($"Ошибка при создании формы: {ex.Message}",
                                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите линию", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}