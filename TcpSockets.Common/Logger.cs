using System;

namespace TcpSockets.Common
{
    public static class Logger
    {
        public static void Log(string message)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"[{DateTime.UtcNow} UTC] ");
            Console.ForegroundColor = prevColor;
            Console.WriteLine($"{message}");
        }
    }
}
