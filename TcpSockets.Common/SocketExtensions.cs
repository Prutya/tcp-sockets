using System;
using System.Net.Sockets;
using System.Text;

namespace TcpSockets.Common
{
    public static class SocketExtensions
    {
        public static string RecieveString(this Socket client)
        {
            string data = null;
            byte[] buffer = new byte[1500];

            try
            {
                int size = client.Receive(buffer);
                data = Encoding.UTF8.GetString(buffer, 0, size);
            }
            catch (SocketException ex)
            {
                Logger.Log(ex.Message);
            }

            return data;
        }

        public static void SendString(this Socket client, string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);
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
