using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IP
{
    public partial class EmployeeInfoForm : Form
    {
        private string selectedLineName;
        private Timer MyTimer;
        private bool isFormClosing = false;

        // Данные сотрудника
        private string employeeName = "";
        private string department = "";
        private string position = "";

        // Свойства для доступа к элементам
        public TextBox NameTextBox => textBoxName;
        public TextBox DepartmentTextBox => textBoxDepartment;
        public TextBox PositionTextBox => textBoxPosition;

        // Конструктор с полными данными
        public EmployeeInfoForm(string line, string name, string dept, string pos)
        {
            InitializeComponent();

            this.FormClosing += EmployeeInfoForm_FormClosing;
            this.FormClosed += EmployeeInfoForm_FormClosed;

            selectedLineName = line ?? "";
            employeeName = name ?? "";
            department = dept ?? "";
            position = pos ?? "";

            InitializeForm();
        }

        // Конструктор только с линией (для обратной совместимости)
        public EmployeeInfoForm(string line) : this(line, "", "", "") { }

        // Пустой конструктор
        public EmployeeInfoForm() : this("", "", "", "") { }

        private void InitializeForm()
        {
            if (labelLineResultEmpl != null)
            {
                if (labelLineResultEmpl.InvokeRequired)
                {
                    labelLineResultEmpl.Invoke(new Action(() =>
                        labelLineResultEmpl.Text = selectedLineName));
                }
                else
                {
                    labelLineResultEmpl.Text = selectedLineName;
                }
            }

            UpdateEmployeeTextboxes(employeeName, department, position);

            GetIPAddress();
            SetTimer();
        }

        public void SetEmployeeData(string name, string dept, string pos)
        {
            employeeName = name;
            department = dept;
            position = pos;

            if (this.IsHandleCreated && !isFormClosing && !this.IsDisposed)
            {
                UpdateEmployeeTextboxes(name, dept, pos);
            }
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            // Дополнительная проверка при загрузке
            UpdateEmployeeTextboxes(employeeName, department, position);

            // Если данные пустые, показываем предупреждение
            if (string.IsNullOrEmpty(employeeName))
            {
                MessageBox.Show("Данные сотрудника не загружены", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (employeeName == "" && department == "" && position == "")
            {
                MessageBox.Show("Пройдите авторизацию");
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;

                if (MyTimer != null)
                {
                    MyTimer.Stop();
                    MyTimer.Dispose();
                }

                MyTimer = new Timer();

                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");
                string jsonContent = File.ReadAllText(jsonFilePath);

                JsonDocument jsonDocServer = JsonDocument.Parse(jsonContent);
                JsonElement rootServer = jsonDocServer.RootElement;

                int exitTimer = Int16.Parse(rootServer.GetProperty("Timer").ToString());

                MyTimer.Interval = exitTimer * 1000;
                MyTimer.Tick += TimerTickHandler;

                MyTimer.Start();
            }
        }

        private async void TimerTickHandler(object sender, EventArgs e)
        {
            MyTimer.Stop();
            MyTimer.Dispose();

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");
            string jsonContent = File.ReadAllText(jsonFilePath);

            JsonDocument jsonDocServer = JsonDocument.Parse(jsonContent);
            JsonElement rootServer = jsonDocServer.RootElement;

            JsonDocument jsonDocLineId = JsonDocument.Parse(jsonContent);
            JsonElement rootLineId = jsonDocLineId.RootElement;

            string server = rootServer.GetProperty("Server").GetString();
            int lineId = Int16.Parse(rootLineId.GetProperty("LineId").ToString());

            string checkLine = $"http://{server}/operators/checkLine?lineId={lineId}";
            string checkLineJSON = await GetJSONFromURL(checkLine);

            if (checkLineJSON == "")
            {
                AuthorizationForm authorizationForm = new AuthorizationForm();
                this.Hide();
                authorizationForm.Show();

                authorizationForm.BringToFront();
                authorizationForm.Focus();
                MessageBox.Show("Линия не запущена");
            }

        }

        private static async Task<string> GetJSONFromURL(string urlJSON)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    HttpResponseMessage response = await client.GetAsync(urlJSON);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка HTTP: {e.Message}");
                    return null;
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Таймаут запроса к серверу");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Общая ошибка: {ex.Message}");
                    return null;
                }
            }
        }

        private void EmployeeInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            // Только останавливаем чтение, НЕ закрываем порт полностью
            SerialPortManager.StopReading();

            if (MyTimer != null)
            {
                MyTimer.Stop();
                MyTimer.Dispose();
            }
        }

        private void EmployeeInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isFormClosing = true;
        }
        private void UpdateEmployeeTextboxes(string name, string dept, string pos)
        {
            try
            {
                if (textBoxName.InvokeRequired)
                {
                    textBoxName.Invoke(new Action(() =>
                    {
                        if (!isFormClosing && !this.IsDisposed && this.IsHandleCreated)
                        {
                            textBoxName.Text = name;
                            textBoxDepartment.Text = dept;
                            textBoxPosition.Text = pos;
                        }
                    }));
                }
                else
                {
                    textBoxName.Text = name;
                    textBoxDepartment.Text = dept;
                    textBoxPosition.Text = pos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обновления текстовых полей: {ex.Message}");
            }
        }

        private void ClearEmployeeTextboxes()
        {
            UpdateEmployeeTextboxes("", "", "");
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            isFormClosing = true;

            // Только останавливаем чтение
            SerialPortManager.StopReading();

            AuthorizationForm authorizationForm = new AuthorizationForm();

            ClearEmployeeTextboxes();
            this.Hide();
            authorizationForm.Show();
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
    }
}