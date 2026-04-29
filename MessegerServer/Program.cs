namespace MessegerServer
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class ClientTcp
    {
        static Socket userSocket = null;
        static string userLogin = null;

        static void Main()
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Client Project");

            userSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverConnection = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            try
            {
                userSocket.Connect(serverConnection);

                Console.WriteLine("\nПодключение к серверу успешно!\n\n\n");

                Console.WriteLine("Введите логин:");
                userLogin = Console.ReadLine();

                byte[] loginBytes = Encoding.UTF8.GetBytes(userLogin);
                userSocket.Send(loginBytes);

                while (true)
                {
                    Console.WriteLine("Введите сообщение для отправки на сервер:");
                    string message = Console.ReadLine();
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    userSocket.Send(messageBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                userSocket.Shutdown(SocketShutdown.Both);
                userSocket.Close();
                Console.WriteLine("Подключение закрыто.");
                Console.ReadKey();
            }

        }
    }
}
