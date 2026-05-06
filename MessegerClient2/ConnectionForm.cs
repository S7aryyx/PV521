using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MessegerClient2
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
            txt_ip.Text = "46.191.235.28";   
            txt_port.Text = "8888";
            txt_login.Text = "Ilgam";
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ip.Text) ||
                string.IsNullOrWhiteSpace(txt_port.Text) ||
                string.IsNullOrWhiteSpace(txt_login.Text))
            {
                lbl_status.Visible = true;
                lbl_status.Text = "Заполните все поля!";
                return;
            }

            if (!int.TryParse(txt_port.Text, out int port) || port < 0 || port > 65535)
            {
                lbl_status.Visible = true;
                lbl_status.Text = "Некорректный порт!";
                return;
            }

            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint serverEp = new IPEndPoint(IPAddress.Parse(txt_ip.Text), port);
                socket.Connect(serverEp);
                string login = txt_login.Text.Trim();
                byte[] loginBytes = Encoding.UTF8.GetBytes(login);
                socket.Send(loginBytes);

                this.Hide();
                ChatForm chatForm = new ChatForm(socket, login);
                chatForm.ShowDialog();   
                this.Close();
            }
            catch (Exception ex)
            {
                lbl_status.Visible = true;
                lbl_status.Text = "Ошибка подключения: " + ex.Message;
            }
        }
    }
}