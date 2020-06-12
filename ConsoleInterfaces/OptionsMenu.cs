using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleInterfaces
{
    public class OptionsMenu
    {
        private int _selectedIndex;
        private readonly string _title;
        private OptionsMenu _parentMenu;
        private readonly MenuType _menuType;
        private Dictionary<Option, bool> _options;

        public event EventHandler<OptionSelectedEventArgs> OptionSelect;
        public event EventHandler<BoxOptionSelectedEventArgs> BoxOptionSelect;


        public enum MenuType
        {
            OPTIONS,
            TICKBOX
        }

        public OptionsMenu(string title, MenuType menuType)
        {
            this._title = title;
            this._menuType = menuType;
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
            public Option Option { get; set; }
        }

        /**
         * This is the custom Event args that the EventHandler will
         * use to parse information for the options
         * 
         * */
        public class BoxOptionSelectedEventArgs : EventArgs
        {
            public List<Option> Options { get; set; }
        }

        public void SetParentMenu(OptionsMenu parentMenu)
        {
            _parentMenu = parentMenu;
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
            if (_options != null) throw new Exception("Cannot override initial set options!");

            var index = 0;
            _options = new Dictionary<Option, bool>();

            foreach (var optionName in options)
            {
                var op = new Option
                {
                    Index = index,
                    Name = optionName
                };

                _options.Add(op, false);
                index++;
                    
            }
        }

        /**
         * Set a linked menu to an option from its ID
         * 
         * int optionNum The options ID
         * OptionMenu The optionMenu to be linked
         * */
        public void SetOptionLink(int optionIndex, OptionsMenu optionsMenu)
        {
            if (_menuType == MenuType.TICKBOX) throw new Exception("Menu cannot be linked to a option of type Tick Box!");
            var option = GetOptionFromIndex(optionIndex);
            option.LinkedOptionMenu = optionsMenu;
            optionsMenu.SetParentMenu(this);
        }

        /**
         * This starts a display loop of an option menu with the
         * highlighted selected option with a custom title.
         * 
         * string title This is the custom title the option menu will have
         * 
         * @returns null
         * */
        public void DisplayMenu()
        {
            if (_options == null) throw new Exception($"Menu {this._title} does not contain any options, and therefore cannot be displayed!");

            var active = true;

            //Loop display the option menu*
            while (active)
            {

                Console.Clear();
                if (!(_title == null || _title.Equals(""))) {
                    Console.Write(_title);

                    if (_parentMenu != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" << Return\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
               
               
               
                foreach (var option in _options)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    //Displays option menu
                    if (_menuType == MenuType.OPTIONS)
                    {

                        if (_selectedIndex == option.Key.Index) Console.BackgroundColor = ConsoleColor.DarkBlue;

                        Console.Write($"[{option.Key.Index + 1}] " + option.Key.Name);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((option.Key.LinkedOptionMenu != null ? " >>" : "") + "\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    //Displays tick box menu
                    else
                    {

                        /*
                         * This handles the colors of the star tick box's and
                         * the selected option
                         */
                        Console.BackgroundColor = ConsoleColor.Black;
                        if (_selectedIndex == option.Key.Index) Console.BackgroundColor = ConsoleColor.DarkBlue;

                        Console.Write("[");
                        if (option.Value) Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((option.Value ? "*" : " "));

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("] " + option.Key.Name + "\n");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(" ");
                if (_menuType == MenuType.TICKBOX)
                {
                    Console.WriteLine("Press [Space] to select option.");
                    Console.WriteLine("Press [Enter] to continue.");
                }
                else
                {
                    Console.WriteLine("Press [Enter] to select.");
                }

                var selectionInfo = GetNextSelectionInfo(_selectedIndex, _options.Count);
                _selectedIndex = selectionInfo.CurrentIndex;
                active = !selectionInfo.StopMenu;
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
                    if (GetOptionFromIndex(currentIndex).LinkedOptionMenu != null)
                    {
                        selectionInfo.StopMenu = true; //Close current menu
                        GetOptionFromIndex(currentIndex).LinkedOptionMenu.DisplayMenu();
                    }
                    break;
                case (ConsoleKey.Escape):
                case (ConsoleKey.LeftArrow):
                    if (_parentMenu != null)
                    {
                        selectionInfo.StopMenu = true;
                        _parentMenu.DisplayMenu();
                    }
                    break;
                case (ConsoleKey.Spacebar):
                    if (_menuType == MenuType.TICKBOX)
                    {
                        _options[GetOptionFromIndex(currentIndex)] = (!_options[GetOptionFromIndex(currentIndex)]);
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
                    if (_menuType == MenuType.OPTIONS)
                    {
                        if (GetOptionFromIndex(currentIndex).LinkedOptionMenu != null)
                        {
                            selectionInfo.StopMenu = true; //Close current menu
                            GetOptionFromIndex(currentIndex).LinkedOptionMenu.DisplayMenu();
                            break;
                        }
                        
                        SendSelectionEvent(currentIndex);
                    } else
                    {
                        SendBoxTickSelectionEvent();
                    }
                    selectionInfo.StopMenu = true;
                    break;
            }
            selectionInfo.CurrentIndex = currentIndex;
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
            var eventArgs = new OptionSelectedEventArgs
            {
                Option = GetOptionFromIndex(currentIndex)
        };
            OnOptionSelect(eventArgs);
        }

        private void SendBoxTickSelectionEvent()
        {
            var tickedOptions = new List<Option>();

            var index = 0;
            foreach (var ticked in _options.Values)
            {
                if (ticked)
                {
                    tickedOptions.Add(GetOptionFromIndex(index));
                }
                index++;
            }

            var eventArgs = new BoxOptionSelectedEventArgs
            {
                Options = tickedOptions
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
            foreach (var option in _options.Where(option => option.Key.Index == index))
            {
                return option.Key;
            }

            return new Option
            {
                Index = -1, Name = "NULL", LinkedOptionMenu = null
               
            };
        }

        private class SelectionInfo
        {
            public int CurrentIndex;
            public bool StopMenu = false;
        }

        public class Option
        {
            public string Name;
            public int Index;
            public OptionsMenu LinkedOptionMenu;
        }

    }
}
