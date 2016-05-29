using System;
using System.Net.Sockets;
using System.Text;
using TcpSockets.Common;

namespace TcpSockets.Server
{
    public class ClientHandler
    {
        private const string timeCommand = "time";

        private readonly Socket _client;

        public ClientHandler(Socket client)
        {
            _client = client;
        }

        public void Handle()
        {
            Logger.Log($"Connection accepted. Client ip: {_client.RemoteEndPoint}");

            var request = ReadRequest(_client);

            WriteResponse(_client, "[Welcome to my server :3] ");

            if (request.Trim().ToLower() == timeCommand)
            {
                WriteResponse(_client, $"System date: {DateTime.Now.ToString()}");
            }
            else
            {
                WriteResponse(_client, request.ToUpper());
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
