using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        TcpListener server = null;
        try
        {
            int port = 20042; ///чекаем порт
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);

            server.Start();

                //// Ждём подключения клиента
            TcpClient client = server.AcceptTcpClient();

                ///// ждём поток для читания
            NetworkStream stream = client.GetStream();

                //// переводим в байты
            byte[] data = new byte[256];

                // основа база фундамент
            while (true)
            {
                    // читаем что там кинули
                int bytesRead = stream.Read(data, 0, data.Length);
                string responseData = Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Recieved: {0}", responseData);

                if (responseData == "Bye:(")
                    break;

                    //// кидаем ответку 
                byte[] msg = Encoding.ASCII.GetBytes("I have got u'r msg");
                stream.Write(msg, 0, msg.Length);
            }

                // закрываем соеденине
            client.Close();
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
                //// остановка, за вардо
            server.Stop();
        }

        Console.WriteLine("\n Server stoppped.");
        Console.ReadLine();
    }
}
