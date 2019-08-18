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

        protected virtual void OnOptionSelect(OptionSelectedEventArgs e)
        {
           
            OptionSelect?.Invoke(this, e);
        }

        public delegate void OptionSelectedEventHandler(object sender, OptionSelectedEventArgs e);

        public class OptionSelectedEventArgs : EventArgs
        {
            public int option { get; set; }
        }

        public void SetOptions(params string[] options)
        {
            if (this.options != null) throw new Exception("Options have already been defined.");

            this.options = options;
        }

        public void DisplayMenu()
        {
            if (this.options == null) throw new Exception("Options cannot be null.");

            bool active = true;
            int selectedIndex = 0;

            while (active)
            {

                Console.Clear();
                Console.WriteLine("Please select an option:");


                int optionIndex = 0;
                foreach (string option in options)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    if (selectedIndex == optionIndex) Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"[{optionIndex + 1}] " + option);

                    Console.BackgroundColor = ConsoleColor.Black;

                    optionIndex++;
                }

                SelectionInfo selectionInfo = GetNextSelectionInfo(selectedIndex, options.Count());
                selectedIndex = selectionInfo.currentIndex;
                active = !selectionInfo.stopMenu;



            }
        }

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
