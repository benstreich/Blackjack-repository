using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Ben_Streich
{
    public class Menu
    {
        private int SelectedIndex = 0;
        private string[] Optionsmenu;

        public Menu(string[] options)
        {
            Optionsmenu = options;
        }

        /// <summary>
        /// responsible for upward and downward control
        /// </summary>
        private void DisplayOptions()
        {
            for (int i = 0; i < Optionsmenu.Length; i++)
            {
                string currentoption = Optionsmenu[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                    Console.WriteLine($"{prefix}<<" + currentoption + ">>");
            }
            Console.ResetColor();
        }
        private static void Titleart()
        {
            Console.SetWindowSize(90, 30);

            Console.WriteLine("\n\n");
            Console.WriteLine("\t.------..------..------..------..------..------..------..------..------.");
            Console.WriteLine("\t|B.--. ||L.--. ||A.--. ||C.--. ||K.--. ||J.--. ||A.--. ||C.--. ||K.--. |");
            Console.WriteLine("\t| :(): || :/\x5c: || (\x5c/) || :/\x5c: || :/\x5c: || :(): || (\x5c/) || :/\x5c: || :/\x5c: |");
            Console.WriteLine("\t| ()() || (__) || :\x5c/: || :\x5c/: || :\x5c/: || ()() || :\x5c/: || :\x5c/: || :\x5c/: |");
            Console.WriteLine("\t| '--'B|| '--'L|| '--'A|| '--'C|| '--'K|| '--'J|| '--'A|| '--'C|| '--'K|");
            Console.WriteLine("\t`------'`------'`------'`------'`------'`------'`------'`------'`------'\n\n");

        }

        /// <summary>
        /// This Method determines the Selected Index and controlls the  Menu
        /// In addition Runmenu returns the selectedIndex to the Main Program
        /// </summary>
        public int Runmenu() 
        {
            ConsoleKey Keypressed;

            do
            {
                Console.Clear();
                Titleart();
                DisplayOptions();

                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                Keypressed = keyinfo.Key;

                if (Keypressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;

                    if (SelectedIndex == -1) SelectedIndex = Optionsmenu.Length - 1;

                }
                else if (Keypressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Optionsmenu.Length) SelectedIndex = 0;
                }

            } while (Keypressed != ConsoleKey.Enter);

            return SelectedIndex;
        }

               
}
}
