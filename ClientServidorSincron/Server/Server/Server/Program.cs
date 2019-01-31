using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static IPAddress ServerIP;
        private static int ServerPort;
        private static IPEndPoint ServerEndPoint;
        private static TcpClient Client;
        private static TcpListener Server;
        private static Boolean breakLoop = false;

        static void Main(string[] args)
        {
            int ServerPort = 50000;
            ServerIP = IPAddress.Parse("127.0.0.1");
            ServerEndPoint = new IPEndPoint(ServerIP, ServerPort);

            Server = new TcpListener(ServerIP, ServerPort);
            Console.WriteLine("Servidor creat");

            Server.Start();
            Console.WriteLine("Servidor iniciat");

            //Configurem connexio del client

            while (true)
            {
                Thread.Sleep(1000);
                Client = Server.AcceptTcpClient();
                Console.WriteLine("Client connectat");

                //Rebem dades del client
                NetworkStream ServerNS = Client.GetStream();

                Thread nouClient = new Thread(() => serverConnect(ServerNS));
                nouClient.Start();
            }

            Console.WriteLine("Server finalitzat");

            Server.Stop();
            Console.ReadLine();
        }

        static string ReverseString(string intString)
        {
            char[] charArray = intString.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static void serverConnect(NetworkStream ServerNS)
        {
            
            do
            {
                try
                {
                    String mess = "";
                    byte[] BufferLocal = new byte[256];
                    do
                    {
                        
                        int BytesRebuts = ServerNS.Read(BufferLocal, 0, BufferLocal.Length);
                        //Passem de bytes a string
                        mess = mess + Encoding.UTF8.GetString(BufferLocal, 0, BytesRebuts);
                    } while (ServerNS.DataAvailable);

                    string reverseString = ReverseString(mess);
                    //Passem de string a bytes
                    byte[] fraseBytes = Encoding.UTF8.GetBytes(reverseString);

                    //Enviem resposta al client
                    ServerNS.Write(fraseBytes, 0, fraseBytes.Length);

                }
                catch (System.IO.IOException e)
                {
                    closeConnexion();
                }
                catch (System.ObjectDisposedException e)
                {
                    closeConnexion();
                }
            } while (!breakLoop);

            closeConnexion();
        }

        public static void closeConnexion()
        {
            breakLoop = true;
            Console.WriteLine("Usuari finalitzat");
            Client.Close();
        }

    }
}



   
        

       

