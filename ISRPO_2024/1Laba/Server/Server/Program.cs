using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Server
{ 
class ClientHandler
{
    public TcpClient clientSocket;

    public void RunClient()
    {
        StreamReader readerStream = new StreamReader(clientSocket.GetStream());
        NetworkStream writerStream = clientSocket.GetStream();

        string returnData = readerStream.ReadLine();
        string name = returnData;
        Console.WriteLine("Welcome " + name + " to the server");

        while (true)
        {
            returnData = readerStream.ReadLine();

            if (returnData.IndexOf("QUIT") > -1)
            {
                Console.WriteLine("Goodbye " + name);
                break;
            }

            Console.WriteLine(name + ": " + returnData);

            returnData += "\r\n";
            byte[] dataWrite = Encoding.UTF8.GetBytes(returnData);
            writerStream.Write(dataWrite, 0, dataWrite.Length);
        }

        clientSocket.Close();
    }
}

class Program
{
    const int ECHO_PORT = 8081;
    public static int nClients = 0;

    static void Main(string[] args)
    {
        try
        {
            TcpListener clientListener = new TcpListener(IPAddress.Any, ECHO_PORT);

            clientListener.Start();
            Console.WriteLine("Waiting for connections...");

            while (nClients < 3)
            {
                TcpClient client = clientListener.AcceptTcpClient();
                ClientHandler cHandler = new ClientHandler();
                cHandler.clientSocket = client;
                Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                clientThread.Start();
                nClients++;
            }

            clientListener.Stop();
        }
        catch (Exception exp)
        {
            Console.WriteLine("Exception: " + exp.Message);
        }
    }
}
}