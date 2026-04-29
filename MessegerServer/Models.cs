using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;


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
        public Thread thread { get; set; } //Поток для общения с клиентом
        public string login { get; set; }
    }

    static List<ClientInfo> clients = new List<ClientInfo>();
    static List<MessageInfo> AllMessages = new List<MessageInfo>();
    static string txtMessages = "messages.txt";

    static void Main()
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Server Project");

        IPEndPoint server = new IPEndPoint(IPAddress.Any, 8888);
        Socket MessagerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        MessagerSocket.Bind(server);
        MessagerSocket.Listen(50);

        Console.WriteLine("Сервер запущен. Ожидание подключений...");

        while (true)
        {
            Socket currentClient = MessagerSocket.Accept();
            Console.WriteLine($"Подключился новый клиент: {currentClient.RemoteEndPoint}");

            ClientInfo clientInfo = new ClientInfo()
            {
                socket = currentClient
            };
            clientInfo.thread = new Thread(() => AcceptedClientConnection(clientInfo));
            clientInfo.thread.Start();
            clients.Add(clientInfo);
        }
    }

    static void AcceptedClientConnection(ClientInfo client)
    {
        Socket currentClientSocket = client.socket;

        try
        {
            byte[] GettedBytes = new byte[1024];
            int BytesCount = currentClientSocket.Receive(GettedBytes);
            string clientLogin = Encoding.UTF8.GetString(GettedBytes, 0, BytesCount);

            while (true)
            {
                BytesCount = currentClientSocket.Receive(GettedBytes);
                string clientMessage = Encoding.UTF8.GetString(GettedBytes, 0, BytesCount);


                var messageInfo = new MessageInfo()
                {
                    login = clientLogin,
                    text = clientMessage,
                    sendTime = DateTime.Now
                };

                Console.WriteLine(messageInfo.ToString());
                AllMessages.Add(messageInfo);
                MessageLog(messageInfo);
            }
        }
        catch (SocketException) //Любой разрыв подключения с клиентом (причина не важна)
        {
            Console.WriteLine($"Клиент {client.login} отключился");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке клиента");
        }
        finally
        {
            currentClientSocket.Close();
        }
    }

    static void MessageLog(MessageInfo msg)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(txtMessages, true))
            {
                writer.WriteLine("====");
                writer.WriteLine(msg.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи сообщения в лог: {ex.Message}");
        }
    }
}
}