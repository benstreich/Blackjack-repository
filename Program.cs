using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Blackjack_Ben_Streich
{
    
    class Program
    {
        static void Main(string[] args)
        {
            startmenu();

            

            /// <summary>
            /// Main Function of the Program, here everything falls into place.
            /// Here the whole Blackjack Logic is programmed out
            /// As well as the Options.
            /// </summary>
          

            void startmenu()
            {

                bool supportmode = false;
                string[] optionsmenu = { "Play", "Rules", "Tutorial", "Options", "Exit" };
                double playerasset = 100.00;


                Menu menu = new Menu(optionsmenu);
                int selectedindex = menu.Runmenu();

                switch (selectedindex)
                {
                    case 0:
                        Blackjack();
                        break;
                    case 1:
                        Rules();
                        break;
                    case 2:
                        tutorial();
                        break;
                    case 3:
                        options();
                        break;
                    case 4:
                        System.Environment.Exit(0);
                        break;
                }

                /// <summary>
                /// Options menu
                /// </summary>

                void options()
                {
                    string[] optionsmenu = { "Support Mode ON", "Support Mode OFF", "Color Menu", "Back to the Startmenu" };
                   
                    Menu menu = new Menu(optionsmenu);
                    int selectedindex = menu.Runmenu();

                    switch (selectedindex)
                    {
                        case 0:
                            supportmode = true;
                            WriteLine("\n");
                            WriteLine("Support Mode has been turned on");
                            Thread.Sleep(1500);
                            Blackjack();
                            break;
                        case 1:
                            supportmode = false;
                            WriteLine("\n");
                            WriteLine("Support Mode has been turned off");
                            Thread.Sleep(1500);
                            startmenu();
                            break;
                        case 2:
                            optionscolormenu();
                            break;
                        case 3:
                            startmenu();
                            break;
                    }

                }

                /// <summary>
                /// This is the Main Method of this Game, everything from here on out is the Logic of the Gane,
                /// this method remains as long as the player has money (playerasset > 0)
                /// </summary>

                void Blackjack()
                {
                    do
                    {
                        bool endofround = false;
                        //Deck for playing
                        Deck deck = new Deck();
                        //Deck for player 
                        Deck playerdeck = new Deck();
                        //Deck for Dealer
                        Deck dealerdeck = new Deck();

                        Console.Clear();

                            Console.WriteLine("\nYou currently have: {0}$", playerasset);
                            Console.WriteLine("How much do you want to bet?\n");
                            string input = Console.ReadLine();

                            double playerbet;

                            ///<summary>
                            ///Here is checked if the Player types in the right data type
                            ///with Tryparse
                            /// </summary>

                            bool success = double.TryParse(input, out playerbet);
                            while (!success)
                            {
                                Console.Clear();
                                Console.WriteLine("\nInvalid Input. Please type in a valid input");
                                Thread.Sleep(3000);
                                Blackjack();
                            }



                            ///<summary>
                            ///If player bets more than he effectifly has
                            /// </summary>
                           
                            if (playerbet > playerasset)
                            {
                                Console.WriteLine("\nYou cannot bet more that you have\nPlease try again\n");
                                Thread.Sleep(2000);
                                Blackjack();
                            }
                            if (playerbet <= 0)
                            {
                                Console.WriteLine("\nYou cannot bet zero\nPlease try again");
                                Thread.Sleep(2000);
                                Blackjack();
                            }

                        Console.WriteLine("\n");
                        Console.WriteLine("Very well, let the game begin");
                        Console.WriteLine("The dealer is dealing out Cards");
                        for (int i = 0; i < 1; i++)
                        {
                            Console.Write("."); Thread.Sleep(500);
                            Console.Write("."); Thread.Sleep(500);
                            Console.Write("."); Thread.Sleep(500);
                        }
                        Console.Clear();

                        //Playing deck
                        deck.createDeck();

                        //This will be executed twice because the player
                        //gets 2 cards at the start of the game
                        playerdeck.drawCard(deck);
                        playerdeck.drawCard(deck);

                        //Same goes for the dealer
                        dealerdeck.drawCard(deck);
                        dealerdeck.drawCard(deck);

                        while (true)
                        {
                            if (supportmode == true)
                            {
                                Console.Write("\t\t\t\t\t\t\t\t\tSupport Mode ON");
                            }

                            ///<summary>
                            ///Display of Cards: Dealer and Player
                            /// </summary>

                            Console.WriteLine("\n\nDealer Hand:");
                            Console.WriteLine(dealerdeck.getCard(0).cardOutput());
                            Console.WriteLine("[hidden card]\n\n");

                            Console.Write("Your Hand:");
                            Console.Write(playerdeck.deckoutput() + "\n");
                            Console.WriteLine("Current Value: +{0}", playerdeck.valueofcardstotal() + "\n\n");

                            if (playerdeck.valueofcardstotal() == 21)
                            {
                                Console.Clear();
                                playerdeck.deckoutput(); 
                                Console.WriteLine("\n\nYou scored a Blackjack");
                                playerasset = playerasset + (1.5 * playerbet);
                                Console.WriteLine("You won +{0}$ this round", (1.5 * playerbet));
                                Console.WriteLine("\nYou now currently have: {0}$", playerasset);
                                Console.WriteLine("End of Hand");
                                Thread.Sleep(5000);
                                endofround = true;
                                break;
                            }

                            Thread.Sleep(1500);

                            Console.WriteLine("\nHit(1), Stand(2) or Double Down(3) (Startmenu(4) Exit(5))\n");

                            if (supportmode == true)
                            {

                                if(dealerdeck.getCard(0).GetValue() == Value.TWO && playerdeck.valueofcardstotal() <= 13)
                                {
                                    WriteLine("Dealer has a fair Card.\nYou should keep hitting until you have a total of 13 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.TWO && playerdeck.valueofcardstotal() > 13)
                                {
                                    WriteLine("Dealer has a fair Card.\nYou should keep hitting until you have a total of 13 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.THREE && playerdeck.valueofcardstotal() <= 13)
                                {
                                    WriteLine("Dealer has a fair Card.\nYou should keep hitting until you have a total of 13 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.THREE && playerdeck.valueofcardstotal() > 13)
                                {
                                    WriteLine("Dealer has a fair Card.\nYou should keep hitting until you have a total of 13 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.FOUR && playerdeck.valueofcardstotal() <= 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.FOUR && playerdeck.valueofcardstotal() > 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.FIVE && playerdeck.valueofcardstotal() <= 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.FIVE && playerdeck.valueofcardstotal() > 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.SIX && playerdeck.valueofcardstotal() <= 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.SIX && playerdeck.valueofcardstotal() > 12)
                                {
                                    WriteLine("Dealer has a poor Card.\nYou should keep hitting until you have a total of 12 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.SEVEN && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.SEVEN && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.EIGHT && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.EIGHT && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.NINE && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.NINE && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.TEN && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.TEN && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.JACK && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.JACK && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.QUEEN && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.QUEEN && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.KING && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.KING && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }

                                if (dealerdeck.getCard(0).GetValue() == Value.ACE && playerdeck.valueofcardstotal() <= 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Hit");
                                    WriteLine("\n");
                                }
                                else if (dealerdeck.getCard(0).GetValue() == Value.ACE && playerdeck.valueofcardstotal() > 17)
                                {
                                    WriteLine("Dealer has a Good Card.\nYou should keep hitting until you have a total of 17 or higher");
                                    WriteLine("Suggestion: Stand");
                                    WriteLine("\n");
                                }


                            }


                            string Input = Console.ReadLine();

                            int playerchoise;
                            success = int.TryParse(Input, out playerchoise);
                            while(!success)
                            {
                                Console.WriteLine("Invalid Input. Please type in a number");
                                Thread.Sleep(750);
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                ClearCurrentConsoleLine();

                                input = Console.ReadLine();
                                success = int.TryParse(input, out playerchoise);
                            }

                            Console.WriteLine("\n");
                            Console.Clear();

                            ///<summary>
                            ///Hit new Dealing out of the Deck
                            /// </summary>

                            if (playerchoise == 1)
                            {
                                playerdeck.drawCard(deck);
                                Console.WriteLine("Dealer deals you a new Card");
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("."); Thread.Sleep(500);
                                    Console.Write("."); Thread.Sleep(500);
                                    Console.Write("."); Thread.Sleep(500);
                                }
                                Console.WriteLine("\n\n" + playerdeck.getCard(playerdeck.decksize() - 1).cardOutput());
                                Thread.Sleep(2500);
                                Console.Clear();

                            }

                            if (playerdeck.valueofcardstotal() > 21)
                            {

                                Console.WriteLine("\nYour Hand:");
                                Console.WriteLine(playerdeck.deckoutput());
                                Console.WriteLine("\n\nBust");
                                Console.WriteLine("Your Value is over 21 (+{0})", playerdeck.valueofcardstotal());
                                playerasset -= playerbet;
                                Console.WriteLine("You lost {0}$", playerbet);
                                Console.WriteLine("End of Hand");
                                Thread.Sleep(5000);
                                Console.Clear();
                                endofround = true;
                                break;
                            }


                            else if (playerchoise == 2)
                                break;
                            else if (playerchoise == 3)
                            {

                                if (playerbet * 2 <= playerasset)
                                {

                                    playerbet += playerbet;
                                    Console.WriteLine("\n\nYour current bet is now: {0}\n", playerbet);
                                    Thread.Sleep(4000);
                                    Console.WriteLine("As you Double Down you will only hit one more time.\n\n");
                                    Thread.Sleep(2000);
                                    Console.WriteLine("Dealer deals you a new Card");
                                    for (int i = 0; i < 1; i++)
                                    {
                                        Console.Write("."); Thread.Sleep(500);
                                        Console.Write("."); Thread.Sleep(500);
                                        Console.Write("."); Thread.Sleep(500);
                                    }
                                    playerdeck.drawCard(deck);
                                    Console.WriteLine("\n\n" + playerdeck.getCard(playerdeck.decksize() - 1).cardOutput());
                                    Thread.Sleep(2500);
                                    Console.Clear();
                                    Console.WriteLine("\nYour Hand:");
                                    Console.WriteLine(playerdeck.deckoutput());
                                    Console.WriteLine("Current Value: +" + playerdeck.valueofcardstotal());
                                    Thread.Sleep(1500);
                                }

                                if (playerdeck.valueofcardstotal() > 21)
                                {

                                    Console.WriteLine("\nYour Hand:");
                                    Console.WriteLine(playerdeck.deckoutput());
                                    Console.WriteLine("\n\nBust");
                                    Console.WriteLine("Your Value is over 21 (+{0})", playerdeck.valueofcardstotal());
                                    playerasset -= playerbet;
                                    Console.WriteLine("You lost {0}$", playerbet);
                                    Console.WriteLine("End of Hand");
                                    Thread.Sleep(6000);
                                    Console.Clear();
                                    endofround = true;
                                    break;
                                }

                                
                                else  Console.WriteLine("You cannot bet Double Down, if you dont have double your money"); 
                                break;
                            }
                            else if (playerchoise == 5) System.Environment.Exit(0);
                            else if (playerchoise == 4) startmenu();
                            else { Console.WriteLine("Please enter one of the given numbers, to continue"); }

                        }

                        Console.WriteLine("\n\nDealer reveals his cards:");
                        for (int i = 0; i < 1; i++)
                        {
                            Console.Write("."); Thread.Sleep(500);
                            Console.Write("."); Thread.Sleep(500);
                            Console.Write("."); Thread.Sleep(500);
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine(dealerdeck.deckoutput());
                        Console.WriteLine("Dealers Value: +" + dealerdeck.valueofcardstotal() + "\n");
                        Thread.Sleep(3000);


                        //Important to know is that the Dealer draws Cards aswell: Dealer draws up to and including 16

                        while ((dealerdeck.valueofcardstotal() < 17) && endofround == false)
                        {
                            dealerdeck.drawCard(deck);
                            Console.WriteLine("\nDealer Draws");
                            for (int i = 0; i < 1; i++)
                            {
                                Console.Write("."); Thread.Sleep(500);
                                Console.Write("."); Thread.Sleep(500);
                                Console.Write("."); Thread.Sleep(500);
                            }
                            Console.WriteLine("\n\n\n" + dealerdeck.getCard(dealerdeck.decksize() - 1).cardOutput());
                            Console.WriteLine("Dealers Value: +" + dealerdeck.valueofcardstotal() + "\n");
                            Thread.Sleep(3000);
                        }

                       ///<summary>
                       ///Different ending of a round: Dealer busts, push, Dealer wins, you win
                       /// </summary>

                        if ((dealerdeck.valueofcardstotal() > 21) && endofround == false)
                        {
                            Console.WriteLine("\nDealer busts");
                            Console.WriteLine("\n\nDealer Hand\n" + dealerdeck.deckoutput());
                            playerasset = playerasset + playerbet;
                            Console.WriteLine("You won +{0}$ this round", playerbet);
                            Console.WriteLine("End of Hand");
                            Thread.Sleep(5000);
                            endofround = true;
                        }

                        if ((playerdeck.valueofcardstotal() == dealerdeck.valueofcardstotal()) && endofround == false)
                        {
                            Console.WriteLine("\nPush");
                            Console.WriteLine("You and the Dealer have the same value");
                            Console.WriteLine(playerdeck.valueofcardstotal() + "" + "-" + "" + dealerdeck.valueofcardstotal());
                            Console.WriteLine("End of Hand");
                            Thread.Sleep(5000);
                            endofround = true;
                        }
                        if ((dealerdeck.valueofcardstotal() > playerdeck.valueofcardstotal()) && endofround == false)
                        {
                            Console.WriteLine("\nDealer wins...Better luck next time :) ");
                            Console.WriteLine("End of Hand");
                            Thread.Sleep(5000);
                            playerasset -= playerbet;
                            endofround = true;
                        }
                        if ((dealerdeck.valueofcardstotal() < playerdeck.valueofcardstotal()) && endofround == false)
                        {
                            Console.WriteLine("\nWinner, winner chicken dinner");
                            playerasset += playerbet;
                            Console.WriteLine("You won +{0}$ this round", playerbet);
                            Console.WriteLine("End of Hand");
                            Thread.Sleep(5000);
                            endofround = true;
                        }

                        playerdeck.crossrounddeck(deck);
                        dealerdeck.crossrounddeck(deck);
                        Console.Clear();
                    }
                    while (playerasset > 0);

                   

                    ///<summary>
                    ///End of the game, when the player breaks out of the while loop
                    /// Here a option is to restart the Game
                    /// </summary>

                    string[] endofgame = { "Restart", "Exit" };
                    Menu menu = new Menu(endofgame);
                    int selectedindex = menu.Runmenu();

                    switch (selectedindex)
                    {
                        case 0:
                            startmenu();
                            break;
                        case 1:
                            System.Environment.Exit(0);
                            break;
                    }

                }

                void optionscolormenu()
                {

                    string[] optionsmenu = {"Font Color", "Console Color", "Back to Options", "Exit"};
                    
                    Menu menu = new Menu(optionsmenu);
                    int selectedindex = menu.Runmenu();

                    switch (selectedindex)
                    {
                        case 0:
                            Console.Clear();
                            string[] fontcolor = {"Red", "Green", "Magenta" , "Black", "Blue", "Cyan", "Gray", "Dark Blue", "Dark Cyan", "Dark Gray", "Dark Green", "Dark Magenta", "Dark Red" };

                            Menu fontmenu = new Menu(fontcolor);
                            int selectedindexfont = fontmenu.Runmenu();

                            switch (selectedindexfont)
                            {
                                case 0:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case 5:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case 6:
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case 7:
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                case 8:
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    break;
                                case 9:
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    break;
                                case 10:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    break;
                                case 11:
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case 12:
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    break;
                                case 13:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;

                            }

                            break;
                        case 1:
                            Console.Clear();
                            string[] consolecolor = { "Red", "Green", "Magenta", "Black", "Blue", "Cyan", "Gray", "Dark Blue", "Dark Cyan", "Dark Gray", "Dark Green", "Dark Magenta", "Dark Red" };

                            Menu consolecolormenu = new Menu(consolecolor);
                            int selectedindexconsolecolor = consolecolormenu.Runmenu();

                            switch (selectedindexconsolecolor)
                            {
                                case 0:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    break;
                                case 1:
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    break;
                                case 2:
                                    Console.BackgroundColor = ConsoleColor.Magenta;
                                    break;
                                case 3:
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    break;
                                case 4:
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    break;
                                case 5:
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    break;
                                case 6:
                                    Console.BackgroundColor = ConsoleColor.Cyan;
                                    break;
                                case 7:
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    break;
                                case 8:
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    break;
                                case 9:
                                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                                    break;
                                case 10:
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    break;
                                case 11:
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case 12:
                                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                    break;
                                case 13:
                                    Console.BackgroundColor = ConsoleColor.DarkRed;
                                    break;

                            }
                            break;
                        case 2:
                            options();
                            break;
                        case 3:
                            System.Environment.Exit(0);
                            break;
                    }


                    options();



                }

            }



            /// <summary>
            /// Here the Rules are explained, I tried to explain the game as simple as possible, which wasn't
            /// as easy because it's Blackjack. At the End you have the Option to Either, start Tutorial, go back to the Menu or Exit
            /// </summary>

            void Rules()
            {
                Console.Clear();
                titleartrules(); 
                WriteLine("\n\n");
                Thread.Sleep(2000);

                Console.WriteLine("The Goal of Blackjack is to have a hand that totals higher than the dealers,");
                Thread.Sleep(200);
                Console.WriteLine("but doesn't total to higher than 21. If your Hand totals higher than 21");
                Thread.Sleep(200);
                Console.WriteLine("It's called a bust, it means that you are out of the game.");
                Thread.Sleep(200);
                Console.WriteLine("Game Start: Everyone is placing a bet, than the Dealer deals everyone\n(including himself) two cards which are face up exepct");
                Thread.Sleep(200);
                Console.WriteLine("the second card of the Dealer that is face down so that you cannot see it.");
                Thread.Sleep(200);
                Console.WriteLine("The cards 2-10 are scored using their face value, Jacks, Queens and Kings each score 10");
                Thread.Sleep(200);
                Console.WriteLine("Aces can be either 1 or 11 depending what the player chooses thoughout the round.");
                Thread.Sleep(200);
                Console.WriteLine("Îf your cards are: An ace and a 10 value card, you score a natural or a Blackjack");
                Thread.Sleep(200);
                Console.WriteLine("That means that you automatically win 1.5 times your bet and you're done with that round.");
                Thread.Sleep(200);
                Console.WriteLine("Otherwise you have the option to either hit or stand");
                Thread.Sleep(200);
                Console.WriteLine("Hit means that you want another Card from the Dealers deck, stand means you want to\nstay with your current hand");
                Thread.Sleep(200);
                Console.WriteLine("Once everyone says stand, the dealer flips up his hidden card and reveals his value");
                Thread.Sleep(200);
                Console.WriteLine("If the dealers Value is 16 or under the dealer has to take another card.\nOnce its 17 or higher");
                Thread.Sleep(200);
                Console.WriteLine("The dealer stays with his hand, if the dealer busts, every player wins twice their bet");
                Thread.Sleep(200);
                Console.WriteLine("If the dealer doesn't bust, only the players which have a higher total value\nthan the dealer");
                Thread.Sleep(200);
                Console.WriteLine("get twice their bet, otherwise you lose your money.\nNow the round is over and it starts again...");
                Thread.Sleep(200);
                Console.WriteLine("There is another options possible to say instead of hit or stand,\nthere is the option to Double Down");
                Thread.Sleep(200);
                Console.WriteLine("If you decide you want to Double Down, you bet twice your bet but you're only allowed\nto draw 1 more card");
                Thread.Sleep(200);
                Console.WriteLine("Another Option is to say Split, this is only possible if you have two cards that\nare the same number");
                Thread.Sleep(200);
                Console.WriteLine("like two Jacks, you can split them apart and play each one\nlike two seperate hands instead of one.");
                Thread.Sleep(200);
                Console.WriteLine("Your inistial bet will be split into two (split hand).\nThe Hand on your right will be played first");
                Thread.Sleep(200);
                Console.WriteLine("You can now play this card like a normal Hand, hit, stand etc.");
                Thread.Sleep(200);
                Console.WriteLine("When you decide you want to split a pair of aces, you will only get 1 card per Hand\n\n");
                Thread.Sleep(200);

                Console.Write("When you are done reading, please press the Enter button.\n");
                Console.ReadKey(true);

                string[] optionsmenurules = { "Back the Startmenu", "Tutorial", "Exit"};


                Menu menu = new Menu(optionsmenurules);
                int selectedindex = menu.Runmenu();

                switch (selectedindex)
                {
                    case 0:
                        startmenu();
                        break;
                    case 1:
                        tutorial();
                        break;
                    case 2:
                        System.Environment.Exit(0);
                        break;
                }

            }


            /// <summary>
            /// Here the Tutorial is programmed out.
            /// The Tutorial is a help for players, who do not understand, how the game works, and progressses. 
            /// The Help is displayed in a fast Typing bot, so there is no confusion
            /// </summary>

            void tutorial()
            {
                Console.Clear();

                string[] optionsmenu = { "Start Tutorial", "Rules", "Back to the Startmenu", "Exit"};

                Menu menu = new Menu(optionsmenu);
                int selectedindex = menu.Runmenu();

                switch (selectedindex)
                {
                    case 0:
                        break;
                    case 1:
                        Rules();
                        break;
                    case 2:
                        startmenu();
                        break;
                    case 3:
                        System.Environment.Exit(0);
                        break;
                }

                Console.Clear();

                WriteLine("\nPlease read the rules first, before you play the Tutorial\n\n");
                WriteLine("Did you read them? (Y/N)");

                

                string input = Console.ReadLine();

                char answer;
                bool success = char.TryParse(input, out answer);

                while (!success)
                {
                    Console.WriteLine("Invalid Input. Please type in Y/N");
                    Thread.Sleep(750);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    ClearCurrentConsoleLine();

                    input = Console.ReadLine();
                    success = char.TryParse(input, out answer);
                }


                if (answer == 'n' || answer == 'N')
                    Rules();






                Console.Clear();
                string botline0 = "\n\nIn Tutorial Mode a bot is typing for you and you just have to press enter\nwhen you are ready.\n\nUnderstood?\n";

                foreach(char b in botline0)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);

                Console.WriteLine("\n\nLet the Tutorial begin");
                for (int i = 0; i < 1; i++)
                {
                    Console.Write("."); Thread.Sleep(500);
                    Console.Write("."); Thread.Sleep(500);
                    Console.Write("."); Thread.Sleep(500);
                }

                Console.Clear();

                 Console.WriteLine("\nYou currently have 100$");
                 Console.WriteLine("How much do you want to bet?\n");
                 Thread.Sleep(1500);

                Console.Write("9"); Thread.Sleep(400); Console.Write("0\n\n"); Thread.Sleep(1500);
                string botline1 = ("The Player chooses how much he wants to bet and presses Enter\n");

                foreach(char b in botline1)
                {
                    Console.Write(b); Thread.Sleep(20);
                }


                Console.ReadKey(true);
                Console.Clear();


                Console.WriteLine("\n\nDealer Hand:\nQUEEN-DIAMONDS\n[hidden card]\n");

                string botline2 = "The Dealer got his Hand, one fliped up and the other fliped down\nYou can see the Dealers Hand on the top left corner\n";

                foreach (char b in botline2)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine("\n\n\n\n\n\nYour Hand:\nNINE-HEARTS\nNINE-CLUBS\nCurrent Value: +18");

                string botline3 = "\n\nYour Hand is displayed underneath the Dealers hand.\nVisable are 2 Cards and the Value\n";
                foreach (char b in botline3)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);
                Console.Clear();

                string botline4 = "These are your options for continiuing the game\n\n";


                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\nHit(1), Stand(2) or Double Down(3)\n\n");

                foreach (char b in botline4)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);
                Thread.Sleep(1000);
                Console.WriteLine("1"); Thread.Sleep(1500);


                Console.Clear();
                string botline5 = "\n\nThe Bot hit, so the Dealer deals you a new Card";

                foreach(char b in botline5)
                {
                    Console.Write(b); Thread.Sleep(20);
                }
                Thread.Sleep(1000);

                Console.WriteLine("\n\nDealer deals you a new Card");
                for (int i = 0; i < 1; i++)
                {
                    Console.Write("."); Thread.Sleep(500);
                    Console.Write("."); Thread.Sleep(500);
                    Console.Write("."); Thread.Sleep(500);
                }
                Console.WriteLine("\n\nTHREE-CLUBS\n");


                String botline6 = "Your new Card\n";
                foreach (char b in botline6)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine("\n\nDealer Hand:\nQUEEN-DIAMONDS\n[hidden card]\n\n");
                Console.WriteLine("Your Hand:\nNINE-HEARTS\nNINE-CLUBS\nTHREE-CLUBS\nCurrent Value: +21\n\n");

                string botline7 = "You Scored a Blackjack!\nYou won twice you initial bet. Congratulation\n\nDo you think you are ready for a real game?\nif you dont feel safe enough you can replay the Tutorial\nor play with the Support mode ON: Starting Menu -> Options -> Support Mode ON\n";
                foreach (char b in botline7)
                {
                    Console.Write(b); Thread.Sleep(20);
                }

                Console.ReadKey(true);

                startmenu();


            }

            void titleartrules()
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("\t\t\t.------..------..------..------..------.");
                Console.WriteLine("\t\t\t|R.--. ||U.--. ||L.--. ||E.--. ||S.--. |");
                Console.WriteLine("\t\t\t| :(): || (\x5c/) || :/\x5c: || (\x5c/) || :/\x5c: |");
                Console.WriteLine("\t\t\t| ()() || :\x5c/: || (__) || :\x5c/: || :\x5c/: |");
                Console.WriteLine("\t\t\t| '--'R|| '--'U|| '--'L|| '--'E|| '--'S|");
                Console.WriteLine("\t\t\t`------'`------'`------'`------'`------'");
            }


            /// <summary>
            /// This Method is used to Clear the current line, so
            /// that a flawless invalid input can be put out, without clearing the whole page
            /// with Console.Clear()
            /// </summary>


            void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine(new String(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

        }
    }

}