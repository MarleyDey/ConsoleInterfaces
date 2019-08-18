using System;
using ConsoleInterfaces;

namespace ConsoleMenuTestUit
{
    class Program
    {
        static void Main(string[] args)
        {

            TickBoxMenu tickBoxMenu = new TickBoxMenu();

            tickBoxMenu.BoxOptionSelect += OnBoxOptionSelect;

            tickBoxMenu.SetBoxOptions("Bob", "Tony", "Marley");

            tickBoxMenu.DisplayMenu("Select A big boi:");

        }

        static void OnBoxOptionSelect(object sender, TickBoxMenu.BoxOptionSelectedEventArgs e)
        {
            Console.WriteLine("Options selected:");
            foreach (int option in e.options)
            {
                Console.WriteLine("- " + option);
            }
        }
    }
}
