using System;
using System.Collections.Generic;

namespace ConsoleInterfaces
{
    public class Log
    {
        public static void Info(string t)
        {
            Console.WriteLine(t);
        }

        public static void Info(string t, ConsoleColor color)
        {
            ConsoleColor previousColour = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(t);
            Console.ForegroundColor = previousColour;
        }

        public static void Error(string t)
        {
            Info("[ERROR] " + t, ConsoleColor.Red);
        }
    }
}
