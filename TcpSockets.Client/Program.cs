using System;
using System.Net;
using System.Net.Sockets;
using TcpSockets.Common;

namespace TcpSockets.Client
{
    public class Program
    {
        private static readonly IPAddress serverIpAddress = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 3000;

        public static void Main(string[] args)
        {
            Logger.Log("Welcome to .NET TCP sockets client.");

            while (true)
            {
                serverPort = Input.PortPrompt();

                while (true)
                {
                    TcpClient client = new TcpClient();

                    try
                    {
                        ProcessCommand(client);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message);
                        break;
                    }
                }
            }
        }

        private static void ProcessCommand(TcpClient client)
        {
            string message = Input.CommandPrompt("for server");

            Logger.Log($"Connecting to {serverIpAddress}:{serverPort}...");

            try
            {
                client.Connect(serverIpAddress, serverPort);
                Socket clientSocket = client.Client;

                Logger.Log("Connection established.");
                clientSocket.SendString(message);

                string response = clientSocket.RecieveString();

                Logger.Log($"Server response is: \"{response}\"");
            }
            finally
            {
                client.Close();
            }
        }
    }
}
