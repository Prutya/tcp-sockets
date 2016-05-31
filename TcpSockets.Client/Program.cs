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
                TcpClient client = new TcpClient();
                try
                {
                    client.Connect(serverIpAddress, serverPort);
                    Logger.Log($"Connected to {client.Client.RemoteEndPoint}...");

                    while (true)
                    {
                        try
                        {
                            ProcessCommand(client.Client);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex.Message);
                            client.Close();
                            break;
                        }
                    }

                }
                catch (SocketException ex)
                {
                    Logger.Log(ex.Message);
                    client.Close();
                }
            }
        }

        private static void ProcessCommand(Socket client)
        {
            string message = Input.CommandPrompt("for server");

            Logger.Log($"Sending message to {client.RemoteEndPoint}.");
            client.SendString(message);

            string response = client.RecieveString();
            Logger.Log($"Server response is: \"{response}\"");
        }
    }
}
