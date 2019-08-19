using System;
using ConsoleInterfaces;

namespace testUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.TICKBOX);
            optionMenu.SetOptions("First Option", "Second Option", "Third Option");

            optionMenu.DisplayMenu();

        }

        static void OnBoxsSelectEvent(object sender, OptionsMenu.BoxOptionSelectedEventArgs e)
        {
           //Event code here
        }

    }
}
