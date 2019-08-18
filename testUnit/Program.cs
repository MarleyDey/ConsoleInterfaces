using System;
using ConsoleInterfaces;

namespace testUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            OptionsMenu options = new OptionsMenu();

            options.SetOptions("Test", "Another test", "boi" );

            var anotherOption = new OptionsMenu();

            anotherOption.SetOptions("Ya", "boi");

            options.SetOptionLinkMenu(2, anotherOption);

            options.DisplayMenu("Try boi:");
        }
    }
}
