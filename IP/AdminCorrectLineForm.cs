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
            int selectedLinelineId = GetLineIdFromName(selectedLineName);

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

                MessageBox.Show($"Линия \"{selectedLineName}\" успешно выбрана и сохранена!",
                              "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения настроек: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем форму авторизации
            AuthorizationForm authorizationForm = new AuthorizationForm(selectedLineName, selectedLinelineId);
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

                    // Если файл существует, используем существующие настройки
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
                Admin = "220832", // Дефолтный пароль администратора
                Server = "192.168.77.74:8181", // Дефолтный сервер
                Timer = 2 // Дефолтный таймер
                          // LineId и LineName будут установлены позже
            };
        }

        // Также обновите метод LoadExistingSettings, чтобы отображать сохраненную линию:
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

        private int GetLineIdFromName(string lineName)
        {
            if (string.IsNullOrEmpty(lineName))
                return 0;

            // Используем словарь для более чистой реализации
            var lineMappings = new Dictionary<string, int>
    {
        { "Боссар 1", 1 },
        { "Боссар 2", 2 },
        { "Боссар 3", 3 },
        { "Боссар 5", 4 },
        { "Боссар 8", 5 },
        { "Боссар 9", 6 },
        { "Боссар 10", 7 },
        { "Боссар 11", 8 },
        { "Боссар 13", 9 },
        { "Волпак 3", 10 },
        { "Боссар 6", 11 },
        { "Боссар 7", 12 },
        { "Боссар 12", 13 },
        { "Меспак", 14 },
        { "Волпак 2", 15 },
        { "Волпак 1", 16 },
        { "Волпак 4", 17 },
        { "Боссар 4", 18 },
        { "Стеклобанка", 19 },
        { "ЛМ 1", 20 },
        { "ЛМ 3", 21 },
        { "ЛМ 4", 22 },
        { "ЛК 3", 23 }
    };

            return lineMappings.TryGetValue(lineName, out int lineId) ? lineId : 0;
        }
        private void ComboBoxLineValue()
        {
            Lines lines = new Lines();
            comboBoxLine.Items.AddRange(lines.linesName);
            comboBoxLine.Text = "Выберите линию";
        }
    }
}