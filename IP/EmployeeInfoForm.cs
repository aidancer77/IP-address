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

            selectedLineName = line;
            employeeName = name;
            department = dept;
            position = pos;

            InitializeForm();
            InitializeSerialPort();
            UpdateEmployeeTextboxes(name, dept, pos);
        }

        // Конструктор только с линией (для обратной совместимости)
        public EmployeeInfoForm(string line) : this(line, "", "", "") { }

        // Пустой конструктор
        public EmployeeInfoForm() : this("") { }

        private void InitializeForm()
        {
            // Устанавливаем линию
            if (labelLineResultEmpl != null)
                labelLineResultEmpl.Text = selectedLineName;

            // Устанавливаем данные сотрудника
            if (textBoxName != null)
                textBoxName.Text = employeeName;
            if (textBoxDepartment != null)
                textBoxDepartment.Text = department;
            if (textBoxPosition != null)
                textBoxPosition.Text = position;

            // Остальная инициализация
            GetIPAddress();
            SetTimer();
        }

        // Метод для установки данных после создания формы
        public void SetEmployeeData(string name, string dept, string pos)
        {
            employeeName = name;
            department = dept;
            position = pos;

            // Если форма уже загружена, обновляем сразу
            if (this.IsHandleCreated && !isFormClosing && !this.IsDisposed)
            {
                UpdateEmployeeTextboxes(name, dept, pos);
            }
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            // Дополнительная инициализация при загрузке формы
            if (!string.IsNullOrEmpty(employeeName) && textBoxName.Text != employeeName)
            {
                textBoxName.Text = employeeName;
                textBoxDepartment.Text = department;
                textBoxPosition.Text = position;
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

            string urlLineIdInfo = $"http://{server}/operators/checkLine?lineId={lineId}";

            string textBoxLineIdInfo = await GetJSONFromURL(urlLineIdInfo);

            if(textBoxLineIdInfo=="")
            {
                AuthorizationForm authorizationForm = new AuthorizationForm();
                this.Hide();
                authorizationForm.Show();

                MessageBox.Show("Линия не запущена. Пожалуйста, повторите авторизацию");
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

        private void InitializeSerialPort()
        {
            bool initialized = SerialPortManager.InitializePort(ProcessCardData);

            if (!initialized)
            {
                MessageBox.Show("Не удалось инициализировать COM-порт. Чтение карт будет недоступно.",
                              "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void ProcessCardInUI(string hexValue)
        {
            // Проверяем, что форма еще не закрывается
            if (isFormClosing || this.IsDisposed || !this.IsHandleCreated)
            {
                Console.WriteLine("Форма закрывается, пропускаем обработку карты");
                return;
            }

            try
            {
                string lineName = selectedLineName;

                if (string.IsNullOrEmpty(lineName))
                {
                    LineInfo lineInfo = new LineInfo();
                    lineName = lineInfo.LineName;
                }

                SerialPortManager.StopReading();

                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");
                string jsonContent = File.ReadAllText(jsonFilePath);

                JsonDocument jsonDocServer = JsonDocument.Parse(jsonContent);
                JsonElement rootServer = jsonDocServer.RootElement;

                JsonDocument jsonDocLineId = JsonDocument.Parse(jsonContent);
                JsonElement rootLineId = jsonDocLineId.RootElement;

                string server = rootServer.GetProperty("Server").GetString();
                int lineId = Int16.Parse(rootLineId.GetProperty("LineId").ToString());

                string urlEmplInfo = $"http://{server}/operators/checkCard?line={lineId}&codekey={hexValue}";

                string textBoxEmplInfo = await GetJSONFromURL(urlEmplInfo);

                if (!string.IsNullOrEmpty(textBoxEmplInfo))
                {
                    try
                    {
                        JsonDocument jsonDocEmployee = JsonDocument.Parse(textBoxEmplInfo);
                        JsonElement rootEmployee = jsonDocEmployee.RootElement;

                        string name_value_empl = rootEmployee.GetProperty("name").GetString();
                        string department_value_empl = rootEmployee.GetProperty("department").GetString();
                        string position_value_empl = rootEmployee.GetProperty("pos").GetString();

                        UpdateEmployeeTextboxes(name_value_empl, department_value_empl, position_value_empl);

                        employeeName = name_value_empl;
                        department = department_value_empl;
                        position = position_value_empl;
                    }
                    catch (JsonException jsonEx)
                    {
                        ClearEmployeeTextboxes();
                        SerialPortManager.ReinitializePort(ProcessCardData);
                    }
                }
                else
                {
                    if (!isFormClosing && this.IsHandleCreated)
                    {
                        MessageBox.Show("Карта не распознана или сервер недоступен", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    ClearEmployeeTextboxes();
                    SerialPortManager.ReinitializePort(ProcessCardData);
                }
            }
            catch (Exception ex)
            {
                if (!isFormClosing && this.IsHandleCreated)
                {
                    MessageBox.Show($"Ошибка обработки карты: {ex.Message}");
                }

                ClearEmployeeTextboxes();
                SerialPortManager.ReinitializePort(ProcessCardData);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateEmployeeTextboxes(string name, string dept, string pos)
        {
            if (isFormClosing || this.IsDisposed || !this.IsHandleCreated)
                return;

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

        private void ClearEmployeeTextboxes()
        {
            UpdateEmployeeTextboxes("", "", "");
        }

        public void ProcessCardData(string message)
        {
            try
            {
                // Проверяем, что форма еще не закрывается и не уничтожена
                if (isFormClosing || this.IsDisposed || !this.IsHandleCreated)
                {
                    MessageBox.Show("Форма не готова к обработке карты");
                    return;
                }

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

                if (string.IsNullOrEmpty(hexValue))
                {
                    return;
                }

                // ВСЕ операции с UI должны быть в UI потоке
                if (this.IsHandleCreated && !this.IsDisposed && !isFormClosing)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        // Проверяем еще раз перед выполнением
                        if (!isFormClosing && !this.IsDisposed && this.IsHandleCreated)
                        {
                            ProcessCardInUI(hexValue);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обработки карты: {ex.Message}");
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