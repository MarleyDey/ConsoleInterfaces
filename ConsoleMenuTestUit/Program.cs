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

            tickBoxMenu.SetBoxOptions("This is the first tick box", "The second tick box", "And the third");
            tickBoxMenu.DisplayMenu("This is a tick box title:");
        }

        static void OnOptionSelect(object sender, OptionsMenu.OptionSelectedEventArgs e)
        {
            Console.WriteLine("You have selected option " + e.option);
        }

        static void OnBoxOptionSelect(object sender, TickBoxMenu.BoxOptionSelectedEventArgs e)
        {
            Console.WriteLine("Options selected:");
            foreach (int option in e.options)
            {
                Console.WriteLine("- " + option);
            }
        }

        /*
         TickBoxMenu tickBoxMenu = new TickBoxMenu();

            tickBoxMenu.BoxOptionSelect += OnBoxOptionSelect;

            tickBoxMenu.SetBoxOptions("Bob", "Tony", "Marley");

            tickBoxMenu.DisplayMenu("Select A big boi:");
    */
    }
}
