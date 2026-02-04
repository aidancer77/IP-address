using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IP
{
    public partial class EmployeeInfoForm : Form
    {
        public ListBox NameTextBox => textBoxName;
        public ListBox DepartmentTextBox => textBoxDepartment;
        public ListBox PositionTextBox => textBoxPosition;

        public EmployeeInfoForm()
        {
            InitializeComponent();
            GetIPAddress();
            SetTimer();
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
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
