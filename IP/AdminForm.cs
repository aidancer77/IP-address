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
    public partial class AdminForm : Form
    {
        public AdminForm()
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

        private void ButtonLabelPassword_Click(object sender, EventArgs e)
        {
            AdminCorrectLineForm adminCorrectLineForm = new AdminCorrectLineForm();

            if (textBoxPassword.Text == "1")
            {
                this.Hide();
                adminCorrectLineForm.Show();
            }
            else { MessageBox.Show("Введите верный пароль", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void ButtonLabelBack_Click(object sender, EventArgs e)
        {
            AuthorizationForm authorizationForm = new AuthorizationForm();

            this.Hide();
            authorizationForm.Show();
        }
    }
}