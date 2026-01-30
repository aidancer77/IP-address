using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IP
{
    public partial class EmployeeInfoForm : Form
    {
        private SerialPort port_COM7;
        private bool continueReading = true;
        private List<string> dataList = new List<string>();
        private Thread readThread;

        public EmployeeInfoForm()
        {
            InitializeComponent();
            GetIPAddress();
            SetTimer();
            ComboBoxLineValue();
            //RePaint();
            InitializeSerialPort();
        }

        public void Read()
        {
            while (continueReading)
            {
                try
                {
                    string message = port_COM7.ReadLine().ToString();

                    string messageDate = "(" + DateTime.Now + ")" + " " + message;

                    string hexValue = message.Replace(" ", String.Empty);
                    hexValue = hexValue.Substring(0, hexValue.IndexOf(","));
                    hexValue = hexValue.Substring(hexValue.LastIndexOf("=") + 1);

                    string urlEmplInfo = "http://192.168.77.74:8181/operators/checkCard?line=4&codekey=" + $"{hexValue}";
                    string urlLinesInfo = "http://192.168.77.74:8181/operators/get-lines";
                    //Process.Start(urlEmplInfo).ToString();

                    string textBoxEmplInfo = GetJSONFromURL(urlEmplInfo).GetAwaiter().GetResult();
                    string textBoxLinesInfo = GetJSONFromURL(urlLinesInfo).GetAwaiter().GetResult();
                    //string[] textBoxEmplInfoFormat = textBoxEmplInfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    JsonDocument jsonDocEmployee = JsonDocument.Parse(textBoxEmplInfo);
                    JsonElement rootEmployee = jsonDocEmployee.RootElement;
                    string name_value_empl = rootEmployee.GetProperty("name").GetString();
                    string department_value_empl = rootEmployee.GetProperty("department").GetString();
                    string position_value_empl = rootEmployee.GetProperty("pos").GetString();

        //            JsonDocument jsonDocLines = JsonDocument.Parse(textBoxLinesInfo);
        //            JsonElement rootLines = jsonDocLines.RootElement;
        //            string id_value_line = rootLines.GetProperty("id").GetString();
        //            string[] name_value_line = rootLines.GetProperty("name")
        //.EnumerateArray()
        //.Select(element => element.GetString())
        //.ToArray();
        //            string mast_line_id = rootLines.GetProperty("mast_line_id").GetString();

                    //string urlLineJSON = "http://192.168.77.74:8181/operators/checkLine?lineId=" + $"{id_value_line}";

                    this.Invoke(new Action(() =>
                    {
                        textBoxName.Items.Clear();
                        textBoxDepartment.Items.Clear();
                        textBoxPosition.Items.Clear();

                        labelAttachPass.Visible = false;
                        buttonLabelPassword.Visible = false;
                        textBoxName.Visible = true;
                        textBoxDepartment.Visible = true;
                        textBoxPosition.Visible = true;
                        loginButton.Visible = true;
                        comboBoxLine.Visible = true;
                        labelLine.Visible = true;

                        textBoxName.Items.Add(name_value_empl);
                        textBoxDepartment.Items.Add(department_value_empl);
                        textBoxPosition.Items.Add(position_value_empl);
                    }));

                    dataList.Add(messageDate);

                }
                catch (TimeoutException) { }
                catch (InvalidOperationException)
                {
                    continueReading = false;
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Ошибка чтения: {ex.Message}");
                    }));
                    continueReading = false;
                }
            }
        }

        private void ComboBoxLineValue()
        {

            string[] lineName = { "Боссар 1", "Боссар 2", "Боссар 3", "Боссар 4", "Боссар 5", "Боссар 6",
                "Боссар 7", "Боссар 8", "Боссар 9", "Боссар 10", "Боссар 11",
                "Боссар 12", "Боссар 13", "Волпак 1", "Волпак 2", "Волпак 3",
                "Волпак 4", "ЛМ1", "ЛМ2", "ЛМ4", "ЛК3", "Меспак", "Стеклобанка" };
            comboBoxLine.Items.AddRange(lineName);
            comboBoxLine.SelectedIndex = -1; // No item is selected initially
            comboBoxLine.Text = "Выберите линию";
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (comboBoxLine.SelectedIndex != -1)
            {
                WindowState = FormWindowState.Minimized;
            }
            else { MessageBox.Show("Выберите линию"); }
        }

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
        {

        }
        //protected void RePaint()
        //{
        //    GraphicsPath graphicpath = new GraphicsPath();
        //    graphicpath.StartFigure();
        //    graphicpath.AddArc(0, 0, 25, 25, 180, 90);
        //    graphicpath.AddLine(25, 0, this.Width - 25, 0);
        //    graphicpath.AddArc(this.Width - 25, 0, 25, 25, 270, 90);
        //    graphicpath.AddLine(this.Width, 25, this.Width, this.Height - 25);
        //    graphicpath.AddArc(this.Width - 25, this.Height - 25, 25, 25, 0, 90);
        //    graphicpath.AddLine(this.Width - 25, this.Height, 25, this.Height);
        //    graphicpath.AddArc(0, this.Height - 25, 25, 25, 90, 90);
        //    graphicpath.CloseFigure();
        //    this.Region = new Region(graphicpath);
        //}
    }
}


//if (dataList.Count > 0)
//{
//    try
//    {
//        continueReading = false;

//        // Ждем завершения потока
//        if (readThread != null && readThread.IsAlive)
//        {
//            readThread.Join(1000);
//        }

//        if (port_COM7.IsOpen)
//        {
//            port_COM7.Close();
//            MessageBox.Show("Порт COM7 закрыт");
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Ошибка закрытия порта: {ex.Message}");
//    }
//}
