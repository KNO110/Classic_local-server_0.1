using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {
                ////// Подключаемся
            TcpClient client = new TcpClient("localhost", 20042);
            NetworkStream stream = client.GetStream();

            Console.Write("Enter u'r msg: ");
            string message = Console.ReadLine();

                ///// кидаем инфу на серв
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);

                //// Ждём крч инфу от сервака
            data = new byte[256];
            string responseData = string.Empty;
            int bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Recivied from server: {0}", responseData);

                // закрываем соеденине с сервом
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }

        Console.WriteLine("\nEnter any key to close console.");
        Console.ReadLine();
    }
}
