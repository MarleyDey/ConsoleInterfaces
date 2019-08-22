using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleInterfaces
{
    public class OptionsMenu
    {
        private int selectedIndex;
        private string title;
        private OptionsMenu parentMenu;
        private Dictionary<Option, bool> options;
        private MenuType menuType;

        public event EventHandler<OptionSelectedEventArgs> OptionSelect;
        public event EventHandler<BoxOptionSelectedEventArgs> BoxOptionSelect;


        public enum MenuType
        {
            OPTIONS,
            TICKBOX
        }

        public OptionsMenu(string title, MenuType menuType)
        {
            this.title = title;
            this.menuType = menuType;
        }

        /** 
         * Event method called when the box selection option 
         * is called
         * */
        protected virtual void OnBoxOptionSelect(BoxOptionSelectedEventArgs e)
        {

            BoxOptionSelect?.Invoke(this, e);
        }

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
            public Option option { get; set; }
        }

        /**
         * This is the custom Event args that the EventHandler will
         * use to parse information for the options
         * 
         * */
        public class BoxOptionSelectedEventArgs : EventArgs
        {
            public List<Option> options { get; set; }
        }

        public void SetParentMenu(OptionsMenu parentMenu)
        {
            this.parentMenu = parentMenu;
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

            var index = 0;
            this.options = new Dictionary<Option, bool>();

            foreach (var optionName in options)
            {
                var op = new Option
                {
                    index = index,
                    name = optionName
                };

                this.options.Add(op, false);
                index++;
                    
            }
        }

        /**
         * Set a linked menu to an option from its ID
         * 
         * int optionNum The options ID
         * OptionMenu The optionMenu to be linked
         * */
        public void SetOptionLink(int optionNum, OptionsMenu optionsMenu)
        {
            if (menuType == MenuType.TICKBOX) throw new Exception("You cannot link a menu to a check box option.");
            var option = GetOptionFromIndex(optionNum - 1);
            option.linkedOptionMenu = optionsMenu;
            optionsMenu.SetParentMenu(this);
        }

        /**
         * This starts a display loop of an option menu with the
         * highlighted selected option with a custom title.
         * 
         * string title This is the custom title the option menu will have
         * 
         * @return null
         * */
        public void DisplayMenu()
        {
            if (this.options == null) throw new Exception("Options cannot be null.");

            var active = true;
          

            //Loop display the option menu*
            while (active)
            {

                Console.Clear();
                if (!(title == null || title.Equals(""))) {
                    Console.Write(title);

                    if (parentMenu != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" << Return\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
               
               
               
                foreach (var option in options)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    //Displays option menu
                    if (menuType == MenuType.OPTIONS)
                    {

                        if (selectedIndex == option.Key.index) Console.BackgroundColor = ConsoleColor.DarkBlue;

                        Console.Write($"[{option.Key.index + 1}] " + option.Key.name);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((option.Key.linkedOptionMenu != null ? " >>" : "") + "\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    //Displays tick box menu
                    else
                    {

                        /*
                         * This handles the colours of the star tick boxs and
                         * the selected option
                         */
                        Console.BackgroundColor = ConsoleColor.Black;
                        if (selectedIndex == option.Key.index) Console.BackgroundColor = ConsoleColor.DarkBlue;

                        Console.Write("[");
                        if (option.Value) Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((option.Value ? "*" : " "));

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("] " + option.Key.name + "\n");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(" ");
                if (menuType == MenuType.TICKBOX) Console.WriteLine("Press [Space] to select.");
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

            var selectionInfo = new SelectionInfo();
            var keyInfo = Console.ReadKey();

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
                case (ConsoleKey.RightArrow):
                    if (GetOptionFromIndex(currentIndex).linkedOptionMenu != null)
                    {
                        selectionInfo.stopMenu = true; //Close current menu
                        GetOptionFromIndex(currentIndex).linkedOptionMenu.DisplayMenu();
                    }
                    break;
                case (ConsoleKey.Escape):
                case (ConsoleKey.LeftArrow):
                    if (parentMenu != null)
                    {
                        selectionInfo.stopMenu = true;
                        parentMenu.DisplayMenu();
                    }
                    break;
                case (ConsoleKey.Spacebar):
                    if (menuType == MenuType.TICKBOX)
                    {
                        options[GetOptionFromIndex(currentIndex)] = (options[GetOptionFromIndex(currentIndex)] ? false : true);
                    }
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
                    if (menuType == MenuType.OPTIONS)
                    {
                        if (GetOptionFromIndex(currentIndex).linkedOptionMenu != null)
                        {
                            selectionInfo.stopMenu = true; //Close current menu
                            GetOptionFromIndex(currentIndex).linkedOptionMenu.DisplayMenu();
                            break;
                        }
                        
                        SendSelectionEvent(currentIndex);
                    } else
                    {
                        SendBoxTickSelectionEvent();
                    }
                    selectionInfo.stopMenu = true;
                    break;
            }
            selectionInfo.currentIndex = currentIndex;
            return selectionInfo;
        }

        /**
         * Sends the selection event when a user selects an
         * option from the menu
         * 
         * int currentIndex The current index of the option selected
         * 
         * @return null
         * */
        private void SendSelectionEvent(int currentIndex)
        {
            OptionSelectedEventArgs eventArgs = new OptionSelectedEventArgs
            {
                option = GetOptionFromIndex(currentIndex)
        };
            OnOptionSelect(eventArgs);
        }

        private void SendBoxTickSelectionEvent()
        {
            var tickedOptions = new List<Option>();

            int index = 0;
            foreach (bool ticked in options.Values)
            {
                if (ticked)
                {
                    tickedOptions.Add(GetOptionFromIndex(index));
                }
                index++;
            }

            BoxOptionSelectedEventArgs eventArgs = new BoxOptionSelectedEventArgs
            {
                options = tickedOptions
            };
            OnBoxOptionSelect(eventArgs);
        }

        /**
         * Gets the option from the index of the option
         * that is being searched for
         * 
         * int index The index of the option
         * 
         * @return The option instance
         * */
        public Option GetOptionFromIndex(int index)
        {
            foreach (var option in options)
            {
                if (option.Key.index == index) return option.Key;
            }
            return new Option
            {
                index = -1, name = "NULL", linkedOptionMenu = null
               
            };
        }

        private class SelectionInfo
        {
            public int currentIndex;
            public bool stopMenu = false;
        }

        public class Option
        {
            public string name;
            public int index;
            public OptionsMenu linkedOptionMenu;
        }

    }
}
