using System;

namespace TcpSockets.Common
{
    public static class IpConfiguration
    {
        public static ushort PortPrompt()
        {
            ushort port;
            string portString;
            do
            {
                Console.Write($"Enter a port for work ({0}-{ushort.MaxValue}): ");
                var prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                portString = Console.ReadLine();
                Console.ForegroundColor = prevColor;
            }
            while (!ushort.TryParse(portString, out port));

            return (port);               
        }
    }
}
