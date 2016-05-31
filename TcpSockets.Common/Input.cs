using System;

namespace TcpSockets.Common
{
    public static class Input
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

            return port;
        }

        public static string CommandPrompt(string optional = null)
        {
            string command;

            do
            {
                Console.Write($"Enter a command{" " + optional}: ");
                var prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                command = Console.ReadLine();
                Console.ForegroundColor = prevColor;
            }
            while (string.IsNullOrWhiteSpace(command));

            return command;
        }
    }
}
