using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MessegerClient2
{
    public partial class ConnectionForm : Form
    {
        public static Socket ClientSocket;
        public static string ClientLogin;

        public ConnectionForm()
        {
            InitializeComponent();
            txt_ip.Text = "46.191.235.28";
            txt_port.Text = "8888";
            txt_login.Text = "TestLogin";
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ip.Text) || string.IsNullOrWhiteSpace(txt_port.Text) 
                || string.IsNullOrWhiteSpace(txt_login.Text))
            {
                lbl_status.Visible = true;
                lbl_status.Text = "Пожалуйста, заполните все поля!";
            }
            else if (int.Parse(txt_port.Text) < 0 || int.Parse(txt_port.Text) > 65535)
            {
                lbl_status.Visible = false;
            }
            else
            {
                try
                {
                    Socket UserSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Parse(txt_ip.Text), int.Parse(txt_port.Text));

                    ClientSocket = UserSocket;
                    ClientSocket.Connect(ServerEndPoint);

                    ClientLogin = txt_login.Text.Trim();
                    byte[] LoginByteArray = Encoding.UTF8.GetBytes(ClientLogin);

                    ClientSocket.Send(LoginByteArray);
                    this.Hide();
                    ChatForm chatForm = new ChatForm(ClientSocket, ClientLogin);
                    chatForm.Show();
                    ClientSocket = null;
                    ClientLogin = null;
                }
                catch(Exception ex)
                {
                    lbl_status.Visible = true;
                    lbl_status.Text = "Ошибка подключения: " + ex.Message;
                    ClientSocket.Close();
                }
            }
        }
    }
}
