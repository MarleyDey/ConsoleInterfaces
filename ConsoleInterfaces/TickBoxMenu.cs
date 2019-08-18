using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterfaces
{
    public class TickBoxMenu
    {

        private string[] boxOptions;
        private bool[] boxTicked;

        public event EventHandler<BoxOptionSelectedEventArgs> BoxOptionSelect;

        /** 
         * Event method called when the box selection option 
         * is called
         * */
        protected virtual void OnBoxOptionSelect(BoxOptionSelectedEventArgs e)
        {

            BoxOptionSelect?.Invoke(this, e);
        }

        public delegate void BoxOptionSelectedEventHandler(object sender, BoxOptionSelectedEventArgs e);

        /**
         * This is the custom Event args that the EventHandler will
         * use to parse information for the options
         * 
         * */
        public class BoxOptionSelectedEventArgs : EventArgs
        {
            public List<int> options { get; set; }
        }

        /**
         * This allows you to set the box options of the box option
         * menu with multiple string parameters
         * 
         * string[] boxOptions These are the option string array
         * 
         * @return null
         * */
        public void SetBoxOptions(params string[] boxOptions)
        {
            if (this.boxOptions != null) throw new Exception("Box options have already been defined.");

            this.boxOptions = boxOptions;

            boxTicked = new bool[boxOptions.Count()];

            for (int i = 0; i < boxOptions.Count(); i++)
            {
                boxTicked[i] = false;
            }
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
            if (this.boxOptions == null) throw new Exception("Box options cannot be null.");

            bool active = true;
            int selectedIndex = 0;

            //Loop display the option menu*
            while (active)
            {

                Console.Clear();
                if (!(title == null || title.Equals("")))
                {
                    Console.WriteLine(title);
                }

                int boxOptionIndex = 0;
                foreach (string boxOption in boxOptions)
                {

                    /*
                     * This handles the colours of the star tick boxs and
                     * the selected option
                     */
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (selectedIndex == boxOptionIndex) Console.BackgroundColor = ConsoleColor.DarkBlue;

                    Console.Write("[");
                    if (boxTicked[boxOptionIndex]) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((boxTicked[boxOptionIndex] ? "*" : " "));

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("] " + boxOption + "\n");

                    Console.BackgroundColor = ConsoleColor.Black;

                    boxOptionIndex++;
                }

                Console.WriteLine(" ");
                Console.WriteLine("Press [Space] to select.");
                Console.WriteLine("Press [Enter] to continue.");

                SelectionInfo selectionInfo = GetNextSelectionInfo(selectedIndex, boxOptions.Count());
                selectedIndex = selectionInfo.currentIndex;
                active = !selectionInfo.stopMenu;
            }
        }

        private SelectionInfo GetNextSelectionInfo(int currentIndex, int boxOptionSize)
        {

            SelectionInfo selectionInfo = new SelectionInfo();
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case (ConsoleKey.UpArrow):
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = (boxOptionSize - 1);
                    break;
                case (ConsoleKey.DownArrow):
                    currentIndex++;
                    if (currentIndex > (boxOptionSize - 1)) currentIndex = 0;
                    break;
                case (ConsoleKey.D1):
                    if (1 > boxOptionSize) break;
                    currentIndex = 0;
                    break;
                case (ConsoleKey.D2):
                    if (2 > boxOptionSize) break;
                    currentIndex = 1;
                    break;
                case (ConsoleKey.D3):
                    if (3 > boxOptionSize) break;
                    currentIndex = 2;
                    break;
                case (ConsoleKey.D4):
                    if (4 > boxOptionSize) break;
                    currentIndex = 3;
                    break;
                case (ConsoleKey.D5):
                    if (5 > boxOptionSize) break;
                    currentIndex = 4;
                    break;
                case (ConsoleKey.D6):
                    if (6 > boxOptionSize) break;
                    currentIndex = 5;
                    break;
                case (ConsoleKey.D7):
                    if (7 > boxOptionSize) break;
                    currentIndex = 6;
                    break;
                case (ConsoleKey.D8):
                    if (8 > boxOptionSize) break;
                    currentIndex = 7;
                    break;
                case (ConsoleKey.D9):
                    if (9 > boxOptionSize) break;
                    currentIndex = 8;
                    break;
                case (ConsoleKey.Spacebar):
                    if (boxTicked[currentIndex] ? boxTicked[currentIndex] = false : boxTicked[currentIndex] = true) ;

                    break;
                case (ConsoleKey.Enter):

                    List<int> tickedOptions = new List<int>();

                    int index = 0;
                    foreach (bool ticked in boxTicked)
                    {
                        if (ticked) {
                            tickedOptions.Add(index + 1);
                        }
                        index++;
                    }

                    BoxOptionSelectedEventArgs eventArgs = new BoxOptionSelectedEventArgs
                    {
                        options = tickedOptions
                    };
                    OnBoxOptionSelect(eventArgs);

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
