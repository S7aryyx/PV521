using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Xml;


namespace MessegerServer
{

    class ServerTcp
    {
        class MessageInfo
        {
            public string login { get; set; } //Логин отправителя сообщения
            public string text { get; set; } //Текст сообщения
            public DateTime sendTime { get; set; }

            public override string ToString()
            {
                return $"{this.login} :|: {this.text} Время: {this.sendTime} ";
            }
        }
        class ClientInfo
        {
            public Socket socket { get; set; }
            public Thread thread { get; set; }
            public string login { get; set; }
        }

        static List<ClientInfo> clients = new List<ClientInfo>();
        static object ClientLock = new object();
        //Заглушка для потока , чтобы он в процессе работы не принял новые (чужие) данные
        static string txtMessages = "messages.txt";

        static void Main()
        {
            IPEndPoint serverEp = new IPEndPoint(IPAddress.Any, 8888);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(serverEp);
            listenSocket.Listen(10);

            while (true)
            {
                Socket clientSocket = listenSocket.Accept();
                Console.WriteLine("[SERVER]Новое подключение: " + clientSocket.RemoteEndPoint.ToString());

                ClientInfo client = new ClientInfo { socket = clientSocket };
                client.thread = new Thread(() => HandleClient(client));
                //При подлючении юзера , сразу же запускается его обработка и обработчик его сообщений.
                //Чтобы не блокировать работу севрера
                //- эта обработка выносится в отдельный поток
                //(где будут запущены РАЗНЫЕ обработчики разных пользователей)
                client.thread.Start();
            }
        }
        static void HandleClient(ClientInfo client)
        {
            Socket socket = client.socket;
            byte[] buffer = new byte[4096];

            try
            {
                int bytesRead = socket.Receive(buffer);
                string login = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                client.login = login;

                while (true)
                {
                    bytesRead = socket.Receive(buffer);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        var msgInfo = new MessageInfo
                        {
                            login = client.login,
                            text = msg,
                            sendTime = DateTime.Now
                        };

                        SendEveryfing(msg, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        static void SendEveryfing(string message, ClientInfo currentClient)
        {
            byte[] buffer = new byte[4096];
            buffer = Encoding.UTF8.GetBytes(message);
            lock (ClientLock)
            {
                foreach (var client in clients)
                {
                    if (client != currentClient)
                    {
                        try
                        {
                            client.socket.Send(buffer);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при отправке сообщения клиенту {client.login}: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}