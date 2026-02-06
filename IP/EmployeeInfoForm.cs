using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace IP
{
    public partial class EmployeeInfoForm : Form
    {
        private string selectedLine;
        public TextBox NameTextBox => textBoxName;
        public TextBox DepartmentTextBox => textBoxDepartment;
        public TextBox PositionTextBox => textBoxPosition;

        public EmployeeInfoForm(string line)
        {
            InitializeComponent();
            selectedLine = line;
            InitializeControls();
            GetIPAddress();
            SetTimer();
        }

        public EmployeeInfoForm() : this("") { }

        private void InitializeControls()
        {
            if (labelLineResultEmpl != null)
            {
                labelLineResultEmpl.Text = selectedLine;
            }
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
