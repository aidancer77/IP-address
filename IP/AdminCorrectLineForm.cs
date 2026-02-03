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
    public partial class AdminCorrectLineForm : Form
    {
        public AdminCorrectLineForm()
        {
            InitializeComponent();
            GetIPAddress();
            SetTimer(); 
            ComboBoxLineValue();

            this.AcceptButton = buttonChooseLine;
            this.buttonChooseLine.Click += new System.EventHandler(this.ButtonChooseLine_Click);
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

        private void ButtonChooseLine_Click(object sender, EventArgs e)
        {            
            AuthorizationForm authorizationForm = new AuthorizationForm();            

            if (comboBoxLine.SelectedIndex != -1)
            {
                this.Hide();
                authorizationForm.Show();
            }
            else { MessageBox.Show("Выберите линию"); }
        }

        private void ComboBoxLineValue()
        {
            Lines lines = new Lines();
            comboBoxLine.Items.AddRange(lines.lineName);
            comboBoxLine.Text = "Выберите линию";
        }
    }
}
