using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Laba_isrpo_2024_Client
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpClient client = new TcpClient("172.20.35.6", 8081);

            StreamReader reader = new StreamReader(client.GetStream());

            NetworkStream writer = client.GetStream();

            string dataToSend = Console.ReadLine();
            dataToSend += "\r\n";
            byte[] data = Encoding.ASCII.GetBytes(dataToSend);

            writer.Write(data, 0, data.Length);

            client.Close();
        }
    }
}
