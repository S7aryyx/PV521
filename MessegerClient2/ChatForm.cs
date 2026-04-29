using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MessegerClient2
{
    public partial class ChatForm : Form
    {
        public static Socket ClientSocket;
        public static string ClientLogin;
        public ChatForm(Socket UserSocket , string UserLogin)
        {
            InitializeComponent();
            ClientLogin = UserLogin;
            ClientSocket = UserSocket;
            lbl_loggedAs.Text = ClientLogin;
            lbl_ConnectionStatus.Text = "Подключено";
            lbl_ConnectionStatus.ForeColor = Color.Green;
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = txt_Message.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Заполните сообщение для ввода", "Ошибка");
                return;
            }
            else
            {
                try
                {
                    byte[] messageBytesArray = Encoding.UTF8.GetBytes(message);
                    ClientSocket.Send(messageBytesArray);

                    lb_Send.Items.Add("Вы: " + message);
                    txt_Message.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при отправке сообщения: " + ex.Message, "Ошибка");
                }
            }

        }

    }
}
