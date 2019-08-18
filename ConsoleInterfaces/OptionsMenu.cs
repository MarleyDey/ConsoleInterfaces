using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaces
{
    public class OptionsMenu
    {

        private string[] options;
        public event EventHandler<OptionSelectedEventArgs> OptionSelect;

        /** 
         * Event method called when the selection option 
         * is called
         * */
        protected virtual void OnOptionSelect(OptionSelectedEventArgs e)
        {
           
            OptionSelect?.Invoke(this, e);
        }

        public delegate void OptionSelectedEventHandler(object sender, OptionSelectedEventArgs e);

        /**
         * This is the custom Event args that the EventHandler will
         * use to parse information for the option
         * 
         * */
        public class OptionSelectedEventArgs : EventArgs
        {
            public int option { get; set; }
        }

        /**
         * This allows you to set the options of the option
         * menu with multiple string parameters
         * 
         * string[] options These are the option string array
         * 
         * @return null
         * */
        public void SetOptions(params string[] options)
        {
            if (this.options != null) throw new Exception("Options have already been defined.");

            this.options = options;
        }

        /**
         * This starts a display loop of an option menu with the
         * highlighted selected option with a custom title.
         * 
         * string title This is the custom title the option menu will have
         * 
         * @return null
         * */
        public void DisplayMenu(string title)
        {
            if (this.options == null) throw new Exception("Options cannot be null.");

            bool active = true;
            int selectedIndex = 0;

            //Loop display the option menu*
            while (active)
            {

                Console.Clear();
                if (!(title == null || title.Equals(""))) {
                    Console.WriteLine(title);
                }
               
                int optionIndex = 0;
                foreach (string option in options)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    if (selectedIndex == optionIndex) Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"[{optionIndex + 1}] " + option);

                    Console.BackgroundColor = ConsoleColor.Black;

                    optionIndex++;
                }

                Console.WriteLine(" ");
                Console.WriteLine("Press [Enter] to select.");

                SelectionInfo selectionInfo = GetNextSelectionInfo(selectedIndex, options.Count());
                selectedIndex = selectionInfo.currentIndex;
                active = !selectionInfo.stopMenu;
            }
        }

        /**
         * This gets the selection information for the console
         * key inputs of the user.
         * 
         * int currentIndex This is the index that the selector is currently on
         * int optionSize This is the number of options there are
         * 
         * @return The selection info class 
         **/
        private SelectionInfo GetNextSelectionInfo(int currentIndex, int optionSize)
        {

            SelectionInfo selectionInfo = new SelectionInfo();
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case (ConsoleKey.UpArrow):
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = (optionSize - 1);
                    break;
                case (ConsoleKey.DownArrow):
                    currentIndex++;
                    if (currentIndex > (optionSize - 1)) currentIndex = 0;
                    break;
                case (ConsoleKey.D1):
                    if (1 > optionSize) break;
                    currentIndex = 0;
                    break;
                case (ConsoleKey.D2):
                    if (2 > optionSize) break;
                    currentIndex = 1;
                    break;
                case (ConsoleKey.D3):
                    if (3 > optionSize) break;
                    currentIndex = 2;
                    break;
                case (ConsoleKey.D4):
                    if (4 > optionSize) break;
                    currentIndex = 3;
                    break;
                case (ConsoleKey.D5):
                    if (5 > optionSize) break;
                    currentIndex = 4;
                    break;
                case (ConsoleKey.D6):
                    if (6 > optionSize) break;
                    currentIndex = 5;
                    break;
                case (ConsoleKey.D7):
                    if (7 > optionSize) break;
                    currentIndex = 6;
                    break;
                case (ConsoleKey.D8):
                    if (8 > optionSize) break;
                    currentIndex = 7;
                    break;
                case (ConsoleKey.D9):
                    if (9 > optionSize) break;
                    currentIndex = 8;
                    break;
                case (ConsoleKey.Enter):
                    OptionSelectedEventArgs eventArgs = new OptionSelectedEventArgs
                    {
                        option = currentIndex + 1
                    };
                    OnOptionSelect(eventArgs);

                    selectionInfo.stopMenu = true;
                    break;
            }
            selectionInfo.currentIndex = currentIndex;
            return selectionInfo;
        }

        private class SelectionInfo
        {
            public int currentIndex;
            public bool stopMenu = false;
        }

    }
}
