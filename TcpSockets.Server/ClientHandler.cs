using System;
using System.Net.Sockets;
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
            Logger.Log($"New connection. Client endpoint: {_client.RemoteEndPoint}");
            while (true)
            {
                Respond();
            }
        }

        private void Respond()
        {
            var request = _client.RecieveString();

            Logger.Log($"Recieved new request from {_client.RemoteEndPoint}");

            if (string.IsNullOrEmpty(request))
            {
                _client.SendString("Invalid command.");
            }
            else if (request.Trim().ToLower() == timeCommand)
            {
                _client.SendString($"System date: {DateTime.Now.ToString()}");
            }
            else
            {
                _client.SendString(request.ToUpper());
            }
        }
    }
}
