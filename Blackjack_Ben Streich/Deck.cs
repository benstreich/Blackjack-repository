using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Ben_Streich
{
    public class Deck
    {

        
        private List<Card> cards { get; set; }

        public Deck()
        {
            this.cards = new List<Card>();
        }



        /// <summary>
        /// This method is only used to Count all cards, above all, this method is onyl used 
        /// to deal the card that has just been drawn. In the Program the Method is used like this:
        /// playerdeck.decksize() -1
        /// </summary>

        public int decksize()
        {
            return this.cards.Count;
        }

        public void crossrounddeck(Deck movedeck)
        {
            int decksize = this.cards.Count;

            for(int i = 0; i < decksize; i++)
            {
                movedeck.addCard(this.cards[i]);
            }
            for (int i = 0; i < decksize; i++)
            {
                this.removeCard(0);
            }
        }


        /// <summary>
        /// this is probably the most important method in the deck class.
        /// Here, foreach Value in the Enum Suits and foreach Value in the Enum Value a card is added to the Array Cards.
        /// Additionally the shuffle method is accessed.
        /// </summary>

        public void createDeck()
        {
            //Generate Cards here
            foreach(Suits suit in (Enum.GetValues(typeof(Suits))))
            {
                    
                   foreach(Value value in (Enum.GetValues(typeof(Value))))
                   {
                        this.cards.Add(new Card(value, suit));
                   }
            }
            shuffleDeck();
        }

       

        /// <summary>
        /// This Method is used, to display the cards
        /// </summary>

        public string deckoutput()
        {
            string cardoutput = "";
            foreach (Card card in this.cards)
            {
                cardoutput += "\n" + card.cardOutput();
            }
            return cardoutput;
            
        }

        /// <summary>
        /// This shuffle Method, shuffles the cards randomly. It's going to go
        /// though the deck and it pulls cards out of the deck and save it in 
        /// a temporary deck. That way the Deck will have a different order.
        /// In the end the original deck will be set equal to the temporary deck.
        /// tmpdeck = shuffleddeck
        /// orgdeck = shuffledeck
        /// </summary>

        public void shuffleDeck()
        {
        
            List<Card> tmpdeck = new List<Card>();
            Random rand = new Random();
            int Index = 0;
            int OrgSize = this.cards.Count();
            for(int i = 0; i < OrgSize; i++)        
            {                                        
                //Random Index = rand.next((max - min) + 1) + min
                Index = rand.Next ((this.cards.Count() - 1 - 0) + 1) + 0;   /*nextint*/
                tmpdeck.Add(this.cards[Index]);
                this.cards.Remove(this.cards[Index]);

                /*
                for loop repeats itself as long as OrgSize is
                tmpdeck will get shuffled and the random index will get removed from cards
                */
            }
            this.cards = tmpdeck;

        }

      
        public void removeCard(int r)
        {
            this.cards.Remove(this.cards[r]);
        }

        public Card getCard(int g)
        {
            return this.cards[g];
        }
        public void addCard(Card addCard)
        {
            this.cards.Add(addCard);
        }

        /// <summary>
        /// This Method is used, so that the one card is drawn
        /// In the Program the method is used twice in a row, because both the Player and Dealer need 2 cards
        /// the Methods: getCard and removeCard are accessed here
        /// </summary>
        
        public void drawCard(Deck comesfromcard)
        {
            this.cards.Add(comesfromcard.getCard(0));
            comesfromcard.removeCard(0);
        }

        /// <summary>
        /// This method calulates the total Value of the players cards.
        /// </summary>
        
        public double valueofcardstotal()
        {
            double totalvalue = 0;
            double aces = 0;

            foreach(Card card in this.cards)
            {
                switch(card.GetValue())
                {
                    case Value.TWO: totalvalue += 2; break;
                    case Value.THREE: totalvalue += 3; break;
                    case Value.FOUR: totalvalue += 4; break;
                    case Value.FIVE: totalvalue += 5; break;
                    case Value.SIX: totalvalue += 6; break;
                    case Value.SEVEN: totalvalue += 7; break;
                    case Value.EIGHT: totalvalue += 8; break;
                    case Value.NINE: totalvalue += 9; break;
                    case Value.TEN: totalvalue += 10; break;
                    case Value.JACK: totalvalue += 10; break;
                    case Value.QUEEN: totalvalue += 10; break;
                    case Value.KING: totalvalue += 10; break;
                    case Value.ACE: aces += 1; break;
                }
            }

         
            for (int i = 0; i < aces; i++)
            {
                if (totalvalue > 10)
                    totalvalue += 1;
                else 
                    totalvalue += 11;

            }

            return totalvalue;
        }


        

    }
}
