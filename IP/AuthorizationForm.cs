using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP
{
    public partial class AuthorizationForm : Form
    {
        private SerialPort port_COM7;
        private bool continueReading = true;
        private List<string> dataList = new List<string>();
        private Thread readThread;

        public AuthorizationForm()
        {
            InitializeComponent();
            InitializeSerialPort();
            GetIPAddress();
            SetTimer();
        }

        public void Read()
        {
            EmployeeInfoForm employeeInfoForm = new EmployeeInfoForm();

            while (continueReading)
            {
                try
                {
                    string message = port_COM7.ReadLine().ToString();

                    string hexValue = message.Replace(" ", String.Empty);
                    hexValue = hexValue.Substring(0, hexValue.IndexOf(","));
                    hexValue = hexValue.Substring(hexValue.LastIndexOf("=") + 1);

                    string urlEmplInfo = "http://192.168.77.74:8181/operators/checkCard?line=4&codekey=" + $"{hexValue}";
                    //string textBoxEmplInfo = GetJSONFromURL(urlEmplInfo).GetAwaiter().GetResult();

                    //JsonDocument jsonDocEmployee = JsonDocument.Parse(textBoxEmplInfo);
                    //JsonElement rootEmployee = jsonDocEmployee.RootElement;
                    //string name_value_empl = rootEmployee.GetProperty("name").GetString();
                    //string department_value_empl = rootEmployee.GetProperty("department").GetString();
                    //string position_value_empl = rootEmployee.GetProperty("pos").GetString();

                    this.Invoke(new Action(() =>
                    {
                        this.Hide();
                        employeeInfoForm.Show();

                        //employeeInfoForm.NameTextBox.Text = "1";
                        //employeeInfoForm.DepartmentTextBox.Text = department_value_empl;
                        //employeeInfoForm.PositionTextBox.Text = position_value_empl;
                    }));

                    // После успешного открытия формы прекращаем чтение
                    continueReading = false;
                }
                catch (TimeoutException) { }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }));
                    continueReading = false;
                }
            }
        }

        private static async Task<string> GetJSONFromURL(string urlJSON)
        {
            HttpClient client = new HttpClient();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
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
                return null; // или throw; чтобы пробросить исключение дальше
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Общая ошибка: {ex.Message}");
                return null;
            }
        }
        
        private void InitializeSerialPort()
        {
            port_COM7 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);
            port_COM7.Handshake = Handshake.None;
            port_COM7.ReadTimeout = 500;
            port_COM7.WriteTimeout = 500;

            port_COM7.Open();

            continueReading = true;

            readThread = new Thread(Read);
            readThread.IsBackground = true;
            readThread.Start();
        }

        private void SetTimer()
        {
            System.Windows.Forms.Timer FormTimer = new System.Windows.Forms.Timer();
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
                    else { return null; }
                }
            }
            return "IP-адрес не найден";
        }

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();

            this.Hide();
            adminForm.Show();
        }
    }
}
