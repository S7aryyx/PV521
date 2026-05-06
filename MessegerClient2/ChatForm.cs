using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MessegerClient2
{
    public partial class ChatForm : Form
    {
        private Socket clientSocket;
        private string clientLogin;
        private Thread listenThread;
        private bool isListening = true;
        public ChatForm(Socket userSocket, string userLogin)
        {
            InitializeComponent();
            clientSocket = userSocket;
            clientLogin = userLogin;

            lbl_loggedAs.Text = clientLogin;
            lbl_ConnectionStatus.Text = "Подключено";
            lbl_ConnectionStatus.ForeColor = System.Drawing.Color.Green;
            listenThread = new Thread(ListenForMessages);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void ListenForMessages()
        {
            byte[] buffer = new byte[4096];
            while (isListening && clientSocket.Connected)
            {
                try
                {
                    int bytesRead = clientSocket.Receive(buffer);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (lb_Send.InvokeRequired)
                    {
                        lb_Send.Invoke(() => lb_Send.Items.Add(message));
                    }
                    else
                    {
                        lb_Send.Items.Add(message);
                    }
                }
                catch (SocketException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка приёма: " + ex.Message);
                    break;
                }
            }
            if (lbl_ConnectionStatus.InvokeRequired)
            {
                lbl_ConnectionStatus.Invoke(() =>
                {
                    lbl_ConnectionStatus.Text = "Отключено";
                    lbl_ConnectionStatus.ForeColor = System.Drawing.Color.Red;
                });
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = txt_Message.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Введите сообщение", "Ошибка");
                return;
            }
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(data);
                //lb_Send.Items.Add("Вы: " + message);
                txt_Message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отправки: " + ex.Message, "Ошибка");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isListening = false;
            try
            {
                if (clientSocket.Connected)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                }
                clientSocket.Close();
            }
            catch { }
        }
    }
}