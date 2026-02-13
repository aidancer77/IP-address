using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        private bool isFormClosing = false;
        private object port;

        public AuthorizationForm()
        {
            InitializeComponent();

            this.FormClosing += AuthorizationForm_FormClosing;
            this.FormClosed += AuthorizationForm_FormClosed;

            InitializeSerialPort();
            LoadLineInfoFromJson();
            GetIPAddress();
            SetTimer();
            SetLineLabel();
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

        public void ProcessCardData(string message)
        {
            try
            {
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
        private async void ProcessCardInUI(string hexValue)
        {
            if (isFormClosing || this.IsDisposed || !this.IsHandleCreated)
            {
                Console.WriteLine("Форма закрывается, пропускаем обработку карты");
                return;
            }

            try
            {
                string lineName = selectedLineName;
                int lineId = selectedLineId;

                if (string.IsNullOrEmpty(lineName) || lineId <= 0)
                {
                    LineInfo lineInfo = new LineInfo();

                    if (string.IsNullOrEmpty(lineName))
                    {
                        lineName = lineInfo.LineName;
                    }

                    if (lineId <= 0)
                    {
                        lineId = lineInfo.LineId;
                    }

                    if (string.IsNullOrEmpty(lineName) || lineId <= 0)
                    {
                        MessageBox.Show("Линия не выбрана",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SerialPortManager.ReinitializePort(ProcessCardData);
                        return;
                    }
                }

                SerialPortManager.StopReading();

                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonFilePath = Path.Combine(appFolder, "lineinfo.json");

                if (!File.Exists(jsonFilePath))
                {
                    MessageBox.Show("Файл настроек lineinfo.json не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SerialPortManager.ReinitializePort(ProcessCardData);
                    return;
                }

                string jsonContent = File.ReadAllText(jsonFilePath);
                JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
                JsonElement root = jsonDoc.RootElement;

                string server = root.GetProperty("Server").GetString();

                if (string.IsNullOrEmpty(server))
                {
                    MessageBox.Show("Не указан адрес сервера в настройках", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SerialPortManager.ReinitializePort(ProcessCardData);
                    return;
                }

                string urlEmplInfo = $"http://{server}/operators/checkCard?line={lineId}&codekey={hexValue}";
                string responseJson = await GetJSONFromURL(urlEmplInfo);

                if (!string.IsNullOrEmpty(responseJson))
                {
                    try
                    {
                        JsonDocument jsonDocEmployee = JsonDocument.Parse(responseJson);
                        JsonElement rootEmployee = jsonDocEmployee.RootElement;

                        if (rootEmployee.TryGetProperty("error", out JsonElement errorElement))
                        {
                            string errorMessage = errorElement.GetString() ?? "Неизвестная ошибка";
                            MessageBox.Show($"Карта не зарегистрирована в системе: {errorMessage}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SerialPortManager.ReinitializePort(ProcessCardData);
                            return;
                        }

                        if (!rootEmployee.TryGetProperty("name", out JsonElement nameElement) ||
                            !rootEmployee.TryGetProperty("department", out JsonElement deptElement) ||
                            !rootEmployee.TryGetProperty("pos", out JsonElement posElement))
                        {
                            MessageBox.Show("Неполные данные сотрудника в ответе сервера", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SerialPortManager.ReinitializePort(ProcessCardData);
                            return;
                        }

                        string employeeName = nameElement.GetString() ?? "";
                        string department = deptElement.GetString() ?? "";
                        string position = posElement.GetString() ?? "";

                        if (string.IsNullOrEmpty(employeeName))
                        {
                            MessageBox.Show("Имя сотрудника не может быть пустым", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SerialPortManager.ReinitializePort(ProcessCardData);
                            return;
                        }

                        EmployeeInfoForm employeeForm = new EmployeeInfoForm(lineName, employeeName, department, position);
                        this.Hide();
                        employeeForm.Show();
                    }
                    catch (Newtonsoft.Json.JsonException ex)
                    {
                        MessageBox.Show($"Ошибка парсинга JSON: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SerialPortManager.ReinitializePort(ProcessCardData);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обработки данных сотрудника: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SerialPortManager.ReinitializePort(ProcessCardData);
                }
            }
            catch (Exception ex)
            {
                if (!isFormClosing && this.IsHandleCreated)
                {
                    MessageBox.Show($"Ошибка обработки карты: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SerialPortManager.ReinitializePort(ProcessCardData);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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

        private void LabelAttachCard()
        {
            if (!string.IsNullOrEmpty(selectedLineName) && selectedLineId > 0)
            {
                MessageBox.Show("Пожалуйста, выберите линию");
            }
        }

        private void AuthorizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

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
                    var defaultSettings = new LineInfo();

                    string jsonString = JsonConvert.SerializeObject(defaultSettings, Formatting.Indented);
                    File.WriteAllText(jsonFilePath, jsonString, System.Text.Encoding.UTF8);
                }

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

        //private void ButtonLabelCard_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(selectedLineName) && selectedLineId > 0)
        //    {
        //        try
        //        {
        //            EmployeeInfoForm employeeForm = new EmployeeInfoForm(selectedLineName);

        //            // Инициализируем COM-порт с callback-ом от новой формы
        //            bool portInitialized = SerialPortManager.InitializePort(ProcessCardData);

        //            if (!portInitialized)
        //            {
        //                Console.WriteLine("Не удалось инициализировать COM-порт. Чтение карт будет недоступно.",
        //                              "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            else
        //            {
        //                Console.WriteLine("COM-порт успешно инициализирован для чтения карт");
        //            }

        //            this.Hide();
        //            employeeForm.Show();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Ошибка при создании формы: {ex.Message}",
        //                           "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Пожалуйста, выберите линию", "Внимание",
        //                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}
    }
}