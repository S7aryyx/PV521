using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MessegerServer
{
    class ServerTcp
    {
        class ClientInfo
        {
            public Socket socket { get; set; }
            public Thread thread { get; set; }
            public string login { get; set; }
        }

        static List<ClientInfo> clients = new List<ClientInfo>();
        static object ClientLock = new object();

        static void Main()
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPEndPoint serverEp = new IPEndPoint(IPAddress.Any, 8888);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(serverEp);
            listenSocket.Listen(10);
            Console.WriteLine("Сервер запущен на порту 8888. Ожидание подключений...");

            while (true)
            {
                Socket clientSocket = listenSocket.Accept();
                Console.WriteLine("[SERVER] Новое подключение: " + clientSocket.RemoteEndPoint.ToString());

                ClientInfo client = new ClientInfo { socket = clientSocket };
                lock (ClientLock)
                {
                    clients.Add(client);
                }
                client.thread = new Thread(() => HandleClient(client));
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
                Console.WriteLine($"[SERVER] Пользователь {login} вошёл в чат.");
                SendEveryfing($"|| {login} присоединился к чату ||", null);

                while (true)
                {
                    bytesRead = socket.Receive(buffer);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string msgText = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (string.IsNullOrWhiteSpace(msgText))
                    {
                        continue;
                    }

                    Console.WriteLine($"[{client.login}]: {msgText}");

                    // Рассылаем ВСЕМ, включая автора
                    SendEveryfing($"{client.login}: {msgText}", null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Клиент {client.login} отключился: {ex.Message}");
            }
            finally
            {
                lock (ClientLock)
                {
                    clients.Remove(client);
                }
                SendEveryfing($"|| {client.login} покинул чат ||", null);
                try 
                { 
                    socket.Shutdown(SocketShutdown.Both); 
                } 
                catch { }
                try 
                { 
                    socket.Close(); 
                } 
                catch { }
            }
        }

        // Отправляем сообщение всем клиентам, кроме exceptClient (null — отправить всем)
        //exceptClient — наш клиент. (Я , который по сути и так уже знает, что я написал).
        static void SendEveryfing(string message, ClientInfo exceptClient)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            lock (ClientLock)
            {
                foreach (var client in clients)
                {
                    if (client == exceptClient) continue;
                    try
                    {
                        if (client.socket.Connected)
                            client.socket.Send(messageBytes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка отправки клиенту {client.login}: {ex.Message}");
                    }
                }
            }
        }
    }
}