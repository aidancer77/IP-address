using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IP
{
    public partial class AdminCorrectLineForm : Form
    {
        private bool isFormClosing = false;
        public AdminCorrectLineForm()
        {
            InitializeComponent();

            this.Paint += RoundedForm_Paint;
            this.DoubleBuffered = true;
            this.Resize += (s, e) => this.Invalidate();

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

        private void RoundedForm_Paint(object sender, PaintEventArgs e)
        {
            DrawCenteredRoundedRectangle(e.Graphics, 450, 270, 30);
            RoundedRectangleTop(e.Graphics, 444, 60, 22);
        }
        private void DrawCenteredRoundedRectangle(Graphics g, int width, int height, int radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = (this.ClientSize.Width - width + 30) / 2;
            int y = (this.ClientSize.Height - height + 30) / 2;

            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(x, y, radius, radius, 180, 90);
                path.AddArc(x + width - radius, y, radius, radius, 270, 90);
                path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
                path.AddArc(x, y + height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                using (Pen pen = new Pen(Color.FromArgb(250, Color.LightGray), 6))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.White)))
                {
                    g.FillPath(brush, path);
                    g.DrawPath(pen, path);
                }
            }
        }
        private void RoundedRectangleTop(Graphics g, int width, int height, int radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = (this.ClientSize.Width - width + 30) / 2;
            int y = (this.ClientSize.Height - height - 175) / 2;

            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(x, y, radius, radius, 180, 90);
                path.AddArc(x + width - radius, y, radius, radius, 270, 90);
                path.AddLine(x + width, y + radius, x + width, y + height);
                path.AddLine(x + width, y + height, x, y + height);
                path.AddLine(x, y + height, x, y + radius);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(250, Color.ForestGreen)))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        private async void ButtonChooseLine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxLine.Text) || comboBoxLine.Text == "Выберите линию")
            {
                MessageBox.Show("Пожалуйста, выберите линию", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Lines lines = new Lines();
            string selectedLineName = comboBoxLine.Text;

            // Получаем массив ID (асинхронно)
            string[] allLineIds = await lines.GetAllLinesIdAsync();

            // Находим индекс выбранной линии в ComboBox
            int selectedIndex = comboBoxLine.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < allLineIds.Length)
            {
                // Берем ID по тому же индексу
                string selectedLineId = allLineIds[selectedIndex];

                // Преобразуем в int (если нужно)
                if (int.TryParse(selectedLineId, out int lineIdInt))
                {
                    LineInfo lineInfo = LoadOrCreateLineInfo();

                    lineInfo.LineName = selectedLineName;
                    lineInfo.LineId = lineIdInt; // Используем числовой ID

                    try
                    {
                        string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                        string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        string jsonString = System.Text.Json.JsonSerializer.Serialize(lineInfo, options);
                        File.WriteAllText(jsonFilePath, jsonString, System.Text.Encoding.UTF8);

                        AuthorizationForm authorizationForm = new AuthorizationForm();
                        this.Hide();
                        authorizationForm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения настроек: {ex.Message}", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка преобразования ID линии", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не удалось определить ID выбранной линии", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private LineInfo LoadOrCreateLineInfo()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");
            string jsonContent = File.ReadAllText(jsonFilePath);

            try
            {
                if (File.Exists(jsonFilePath))
                {
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

            return new LineInfo(); 
        }

        private void LoadExistingSettings()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");
            string jsonContent = File.ReadAllText(jsonFilePath);

            try
            {
                if (File.Exists (jsonFilePath))
                {
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

        private async void ComboBoxLineValue()
        {
            try
            {
                Lines lines = new Lines();
                string[] lineNames = await lines.GetAllLinesNameAsync();

                if (lineNames != null && lineNames.Length > 0)
                {
                    comboBoxLine.Items.Clear();
                    comboBoxLine.Items.AddRange(lineNames);
                    comboBoxLine.Text = "Выберите линию";
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить список линий с сервера", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxLine.Text = "Выберите линию";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки линий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxLine.Text = "Выберите линию";
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
        private void UpdateDateTime()
        {
            if (labelDateTime != null && !isFormClosing && this.IsHandleCreated)
            {
                // Используем Invoke если вызываем из другого потока
                if (labelDateTime.InvokeRequired)
                {
                    labelDateTime.Invoke(new Action(() =>
                        labelDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm")));
                }
                else
                {
                    labelDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                }
            }
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
    }
}