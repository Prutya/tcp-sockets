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

            var request = _client.RecieveString();

            if (request.Trim().ToLower() == timeCommand)
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
