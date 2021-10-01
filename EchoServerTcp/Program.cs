using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServerTcp
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("This is the Echo server");
            IPAddress localAddr = IPAddress.Parse("192.168.104.145");
            TcpListener listener = new TcpListener(localAddr, 7);
            //TcpListener listener = new TcpListener(IPAddress.Loopback, 7);


            listener.Start();
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();

                Task.Run(() =>
                {
                    HandleClient(socket);
                }
               );
            }
        }

        public static void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            Console.WriteLine("Hurra, incoming client");
            while (true)
            {

           string message = reader.ReadLine().Trim() ;
           Console.WriteLine("Request: " + message);

                if (message.Contains("Fuck"))
                {
                    writer.WriteLine(message.Replace("Fuck", "No"));
                }

               else if (message.StartsWith("x1")) 
                {
                    writer.WriteLine(message.ToUpper().Substring(2).Trim());
                }
                  
              else if(message == "farvel" )
               {
                  break;
               }

                else
                {
                    writer.WriteLine(message);
                }

                writer.Flush();
                //socket.Close();
            }

        }
    }
    }

