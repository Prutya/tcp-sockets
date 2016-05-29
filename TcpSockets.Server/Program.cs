using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpSockets.Common;

namespace WebSocketLab
{
    public class Program
    {
        private const string serverIpAddress = "127.0.0.1";
        private static int serverPort = 3000;
        private const string timeCommand = "time";
        private const string response = "Welcome to my server <3";

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
                    HandleClient(client);
                });
            }

            Console.Read();
        }

        private static void HandleClient(Socket client)
        {
            Logger.Log($"Connection accepted. Client ip: {client.RemoteEndPoint}");

            var request = ReadRequest(client);

            WriteResponse(client, "[Welcome to my server :3] ");

            if (request.Trim().ToLower() == timeCommand)
            {
                WriteResponse(client, $"System date: {DateTime.Now.ToString()}");
            }
            else
            {
                WriteResponse(client, request.ToUpper());
            }
        }

        private static string ReadRequest(Socket client)
        {
            string data;
            byte[] buffer = new byte[1500];

            int size = client.Receive(buffer);
            data = Encoding.UTF8.GetString(buffer, 0, size);

            return data;
        }

        private static void WriteResponse(Socket client, string response)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(response);
            try
            {
                client.Send(encodedMessage);
            }
            catch (SocketException ex)
            {
                Logger.Log(ex.Message);
            }
        }
    }
}
