using System;
using ConsoleInterfaces;

namespace ConsoleMenuTestUit
{
    class Program
    {
        static void Main(string[] args)
        {

            OptionsMenu optionsMenu = new OptionsMenu();

            optionsMenu.OptionSelect += OnOptionSelected;
       

            optionsMenu.SetOptions("Option 1", "Option 22", "Or my fav option 3");

            optionsMenu.DisplayMenu();

        }

        static void OnOptionSelected(object sender, OptionsMenu.OptionSelectedEventArgs e)
        {
            Console.WriteLine("The option selected was in fact: " + e.option);
        }
    }
}
