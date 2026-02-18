using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace IP
{
    public partial class AdminForm : Form
    {
        private bool continueReading = true;
        private Thread readThread;
        private bool isFormClosing = false;

        public AdminForm()
        {
            InitializeComponent();

            this.Paint += RoundedForm_Paint;
            this.DoubleBuffered = true;
            this.Resize += (s, e) => this.Invalidate();

            StartAdminCardCheck();
            GetIPAddress();
            SetTimer();
        }

        private void RoundedForm_Paint(object sender, PaintEventArgs e)
        {
            DrawCenteredRoundedRectangle(e.Graphics, 450, 270, 30);
            RoundedRectangleTop(e.Graphics, 444, 60, 24);
        }
        private void DrawCenteredRoundedRectangle(Graphics g, int width, int height, int radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = (this.ClientSize.Width - width + 30) / 2;
            int y = (this.ClientSize.Height - height + 28) / 2;

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

        private void StartAdminCardCheck()
        {
            // Инициализируем порт с колбэком для обработки карт администратора
            bool portInitialized = SerialPortManager.InitializePort(ProcessAdminCardData);

            if (!portInitialized)
            {
                Console.WriteLine("Не удалось инициализировать COM-порт для администратора.\n" +
                              "Возможно порт занят или недоступен.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Запускаем поток для периодической проверки состояния
                continueReading = true;

                readThread = new Thread(() =>
                {
                    while (continueReading && !isFormClosing)
                    {
                        // Периодически проверяем состояние порта
                        Thread.Sleep(500);
                    }
                });
                readThread.IsBackground = true;
                readThread.Start();
            }
        }

        private void ProcessAdminCardData(string message)
        {
            try
            {
                if (isFormClosing) return;

                string hexValue = message.Replace(" ", String.Empty);
                int commaIndex = hexValue.IndexOf(",");
                if (commaIndex > 0)
                {
                    hexValue = hexValue.Substring(0, commaIndex);
                }

                int equalsIndex = hexValue.LastIndexOf("=");
                if (equalsIndex > 0)
                {
                    hexValue = hexValue.Substring(equalsIndex + 1);
                }

                // Получаем настройки администратора из JSON
                LineInfo lineInfo = LoadLineInfoFromJson();

                if (lineInfo != null && hexValue == lineInfo.Admin)
                {
                    if (this.IsHandleCreated && !isFormClosing)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!isFormClosing)
                            {
                                // Останавливаем чтение перед переходом
                                SerialPortManager.StopReading();

                                // Переходим к форме выбора линии
                                AdminCorrectLineForm adminCorrectLineForm = new AdminCorrectLineForm();
                                this.Hide();
                                adminCorrectLineForm.Show();
                            }
                        }));
                    }
                }
                // Если это не карта администратора, игнорируем
            }
            catch (Exception ex)
            {
                // Логируем ошибку, но не показываем пользователю
                Console.WriteLine($"Ошибка обработки карты администратора: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Файл настроек lineinfo.json не найден.\n" +
                                  "Пожалуйста, настройте параметры линии в приложении.",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                string jsonContent = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<LineInfo>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки настроек: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ButtonLabelEnter_Click(object sender, EventArgs e)
        {
            LineInfo lineInfo = LoadLineInfoFromJson();

            if (lineInfo != null)
            {
                if (textBoxPassword.Text == lineInfo.Admin)
                {
                    SerialPortManager.StopReading();

                    continueReading = false;
                    if (readThread != null && readThread.IsAlive)
                    {
                        readThread.Join(100);
                    }

                    AdminCorrectLineForm adminCorrectLineForm = new AdminCorrectLineForm();
                    this.Hide();
                    adminCorrectLineForm.Show();
                }
                else
                {
                    MessageBox.Show("Введите верный пароль", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить настройки администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonLabelBack_Click(object sender, EventArgs e)
        {
            // Останавливаем чтение администратора
            SerialPortManager.StopReading();

            continueReading = false;
            if (readThread != null && readThread.IsAlive)
            {
                readThread.Join(100);
            }

            // Возвращаемся к форме авторизации
            AuthorizationForm authorizationForm = new AuthorizationForm();
            this.Hide();
            authorizationForm.Show();
        }

        private void AdminForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !isFormClosing)
            {
                // При повторном показе формы возобновляем проверку карт администратора
                continueReading = true;
                SerialPortManager.InitializePort(ProcessAdminCardData);

                if (readThread == null || !readThread.IsAlive)
                {
                    readThread = new Thread(() =>
                    {
                        while (continueReading && !isFormClosing)
                        {
                            Thread.Sleep(500);
                        }
                    });
                    readThread.IsBackground = true;
                    readThread.Start();
                }
            }
        }

        private void SetTimer()
        {
            UpdateDateTime();

            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
            FormTimer.Interval = 1000;
            FormTimer.Tick += (s, e) => UpdateDateTime();
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
    }
}