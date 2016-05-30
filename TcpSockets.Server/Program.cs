using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpSockets.Common;

namespace TcpSockets.Server
{
    public class Program
    {
        private static readonly IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 3000;

        public static void Main(string[] args)
        {
            Logger.Log("Welcome to .NET TCP sockets server.");

            serverPort = Input.PortPrompt();

            var listener = new TcpListener(serverIpAddress, serverPort);
            listener.Start();

            Logger.Log($"Server started. Listening at {listener.LocalEndpoint}.");

            while (true)
            {
                var client = listener.AcceptSocket();

                Task.Run(() =>
                {
                    try
                    {
                        new ClientHandler(client).Handle();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message);
                        client.Close();
                    }
                });
            }
        }
    }
}
