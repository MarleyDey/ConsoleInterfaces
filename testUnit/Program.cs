using System;
using ConsoleInterfaces;
using System.Collections.Generic;

namespace testUnit
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.White;
            string test = Log.ReplacePlaceHolders("Test {boi} a test", new Dictionary<string, string>()
            {
                { "{boi}", "is" }
            });

            Log.Info(test);
        }
    }
}
