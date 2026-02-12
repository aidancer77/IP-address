using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;

namespace IP
{
    public partial class AdminCorrectLineForm : Form
    {
        public AdminCorrectLineForm()
        {
            InitializeComponent();
            GetIPAddress();
            SetTimer();
            ComboBoxLineValue();
            LoadExistingSettings();
        }

        public string comboBoxLineResult
        {
            get { return comboBoxLine.Text; }
            set { comboBoxLine.Text = value; }
        }

        private void SetTimer()
        {
            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
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
                        if (address.ToString() != "::1")
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

        private void ButtonChooseLine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxLine.Text) || comboBoxLine.Text == "Выберите линию")
            {
                MessageBox.Show("Пожалуйста, выберите линию", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedLineName = comboBoxLine.Text;
            int selectedLinelineId = Lines.GetLineIdFromName(selectedLineName);

            // Загружаем существующие настройки или создаем новые
            LineInfo lineInfo = LoadOrCreateLineInfo();

            // Обновляем информацию о линии
            lineInfo.LineName = selectedLineName;
            lineInfo.LineId = selectedLinelineId;

            // Сохраняем информацию о линии в JSON
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = System.Text.Json.JsonSerializer.Serialize(lineInfo, options);
                File.WriteAllText(jsonFilePath, jsonString, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения настроек: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем форму авторизации
            AuthorizationForm authorizationForm = new AuthorizationForm();
            this.Hide();
            authorizationForm.Show();
        }

        private LineInfo LoadOrCreateLineInfo()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var existingInfo = JsonConvert.DeserializeObject<LineInfo>(jsonContent);

                    if (existingInfo != null)
                    {
                        return existingInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки существующих настроек: {ex.Message}");
            }

            // Создаем новые настройки с дефолтными значениями
            return new LineInfo
            {
                Admin = "220832",
                Server = "192.168.77.74:8181",
                Timer = 5
            };
        }

        private void LoadExistingSettings()
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                if (File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var lineInfo = JsonConvert.DeserializeObject<LineInfo>(jsonContent);

                    if (lineInfo != null)
                    {
                        // Устанавливаем выбранную линию, если она есть
                        if (!string.IsNullOrEmpty(lineInfo.LineName))
                        {
                            comboBoxLine.Text = lineInfo.LineName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки настроек: {ex.Message}");
            }
        }

        private void ComboBoxLineValue()
        {
            Lines lines = new Lines();
            comboBoxLine.Items.AddRange(lines.linesName);
            comboBoxLine.Text = "Выберите линию";
        }
    }
}