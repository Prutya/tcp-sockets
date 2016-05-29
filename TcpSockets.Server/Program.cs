using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpSockets.Common;

namespace TcpSockets.Server
{
    public class Program
    {
        private const string serverIpAddress = "127.0.0.1";
        private static int serverPort = 3000;

        public static void Main(string[] args)
        {
            Logger.Log("Welcome to .NET TCP sockets server.");

            serverPort = IpConfiguration.PortPrompt();

            TcpListener listener = new TcpListener(IPAddress.Parse(serverIpAddress), serverPort);
            listener.Start();
            Logger.Log($"Server started. Listening on {serverIpAddress}:{serverPort}.");

            while (true)
            {
                var client = listener.AcceptSocket();

                Task.Run(() =>
                {
                    new ClientHandler(client).Handle();
                });
            }

            Console.Read();
        }
    }
}
