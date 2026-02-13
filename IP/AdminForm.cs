using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
            StartAdminCardCheck();
            GetIPAddress();
            SetTimer();
        }

        private void StartAdminCardCheck()
        {
            // Инициализируем порт с колбэком для обработки карт администратора
            bool portInitialized = SerialPortManager.InitializePort(ProcessAdminCardData);

            if (!portInitialized)
            {
                Console.WriteLine("Не удалось инициализировать COM-порт для администратора.\n" +
                              "Возможно порт занят или недоступен.",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
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
                Console.WriteLine($"Ошибка обработки карты администратора: {ex.Message}");
            }
        }

        private void SetTimer()
        {
            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
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
                                  "Ошибка",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return null;
                }

                string jsonContent = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<LineInfo>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки настроек: {ex.Message}",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return null;
            }
        }

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Введите верный пароль",
                                  "Внимание",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить настройки администратора",
                              "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
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

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Обработка нажатия Enter в поле пароля
            if (e.KeyChar == (char)Keys.Enter)
            {
                ButtonLabelPassword_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}