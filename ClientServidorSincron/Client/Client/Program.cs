using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        private static IPAddress ServerIP;
        private static int ClientPort;
        private static IPEndPoint serverEndPoint;
        private static string text = "";
        private static string message = "";
        private static TcpClient Client;
        private static NetworkStream ClientNS;


        static void Main(string[] args)
        {
            ClientPort = 50000;
            ServerIP = IPAddress.Parse("127.0.0.1");
            serverEndPoint = new IPEndPoint(ServerIP, ClientPort);

            TcpClient Client = new TcpClient();

            Client.Connect(ServerIP, ClientPort);

            if (Client.Connected)
                Console.WriteLine("Connectat al servidor");
            do
            {
                ClientNS = Client.GetStream();
                Console.WriteLine("Escriu una frase (q per sortir):");
                text = Console.ReadLine();

                //Pasem el text String a bytes
                byte[] textBytes = Encoding.UTF8.GetBytes(text);

                //Enviar text al server
                ClientNS.Write(textBytes, 0, textBytes.Length);

                byte[] BufferLocal = new byte[256];
                message = "";

                do
                {
                    int BytesRebuts = ClientNS.Read(BufferLocal, 0, BufferLocal.Length);
                    message = message + Encoding.UTF8.GetString(BufferLocal, 0, BytesRebuts);
                } while (ClientNS.DataAvailable);

                Console.WriteLine("{0}", message);

            } while (!message.Equals("q"));

            Console.WriteLine("final");

            ClientNS.Close();
            Client.Close();

            Console.ReadLine();
        }
    }
}
