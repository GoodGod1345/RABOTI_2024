using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System;

namespace Client2
{

    class Program
    {
        const int ECHO_PORT = 8081;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Your Name:");
                string name = Console.ReadLine();
                Console.WriteLine("---Logged In---");

                TcpClient eClient = new TcpClient("172.20.35.5", ECHO_PORT);
                StreamReader readerStream = new StreamReader(eClient.GetStream());
                NetworkStream writerStream = eClient.GetStream();

                string dataToSend;
                dataToSend = name + "\r\n";
                byte[] data = Encoding.UTF8.GetBytes(dataToSend); writerStream.Write(data, 0, data.Length);
                while (true)
                {
                    Console.Write(name + ":");
                    dataToSend = Console.ReadLine(); dataToSend += "\r\n";
                    data = Encoding.UTF8.GetBytes(dataToSend); writerStream.Write(data, 0, data.Length);
                    if (dataToSend.IndexOf("QUIT") > -1)
                        break;
                    string returnData = readerStream.ReadLine(); Console.WriteLine("Server: " + returnData);
                }
                eClient.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
        }
    }
}