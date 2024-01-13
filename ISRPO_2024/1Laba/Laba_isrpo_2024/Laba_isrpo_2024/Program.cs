using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Laba_isrpo_2024
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener tcpListener = new TcpListener(8081);
            tcpListener.Start();

            TcpClient client = tcpListener.AcceptTcpClient(); //получение клиента подключённого

            StreamReader reader = new StreamReader(client.GetStream());
            NetworkStream writer = client.GetStream();
            

            string dataFromClient = reader.ReadLine();
            Console.WriteLine(dataFromClient);
            client.Close();
            tcpListener.Stop();
            Console.ReadKey();



        }
    }
}