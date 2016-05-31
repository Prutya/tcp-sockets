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
                try
                {
                    Respond();
                }
                catch (Exception)
                {
                    Logger.Log("Client disconnected.");
                    _client.Close();
                    break;
                }
            }
        }

        private void Respond()
        {
            var request = _client.RecieveString();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Client closed the connection.");
            }

            Logger.Log($"Recieved new request from {_client.RemoteEndPoint}");

            if (request.Trim() == string.Empty)
            {
                _client.SendString("Invalid command.");
            }
            else if (request.Trim().ToLower() == timeCommand)
            {
                _client.SendString($"UTC System date: {DateTime.Now.ToString()}");
            }
            else
            {
                _client.SendString(request.ToUpper());
            }
        }
    }
}
