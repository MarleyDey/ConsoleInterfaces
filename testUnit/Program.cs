using System;
using ConsoleInterfaces;
using System.Collections.Generic;

namespace testUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayTickBox();   
        }

        static void OpenNormalDisplayMenu()
        {
            var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.OPTIONS);
            optionMenu.SetOptions("First Option", "Second Option", "Third Option");

            optionMenu.OptionSelect += OnOptionSelectEvent;

            optionMenu.DisplayMenu();
        }

        static void OnOptionSelectEvent(object sender, OptionsMenu.OptionSelectedEventArgs e)
        {
            Console.WriteLine($"You selected {e.Option.Index}");
            Console.WriteLine($"You selected {e.Option.Name}");
        }

        private static void OptionLinkingTest()
        {
            var optionMenu = new OptionsMenu("Menu 1:", OptionsMenu.MenuType.OPTIONS);
            optionMenu.SetOptions("Option 1", "Option 2", "option 3");

            var secondOptionMenu = new OptionsMenu("Menu 2", OptionsMenu.MenuType.OPTIONS);
            secondOptionMenu.SetOptions("Option 1", "Option 2", "Option 3");

            optionMenu.SetOptionLink(1, secondOptionMenu);

            optionMenu.DisplayMenu();
        }

        static void DisplayTickBox()
        {
            var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.OPTIONS);
            optionMenu.SetOptions("First Option", "Second Option", "Third Option");

            var secondOptionMenu = new OptionsMenu("Menu 2", OptionsMenu.MenuType.TICKBOX);
            secondOptionMenu.SetOptions("Option 1", "Option 2", "Option 3");

            optionMenu.SetOptionLink(1, secondOptionMenu);

            optionMenu.BoxOptionSelect += OnBoxsSelectEvent;

            optionMenu.DisplayMenu();
        }

        static void OnBoxsSelectEvent(object sender, OptionsMenu.BoxOptionSelectedEventArgs e)
        {
            Console.WriteLine($"Boxes selected: {e.Options.Count}");
        }
    }
}
