using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Ben_Streich
{
    public class Card
    {
        Value value = new Value();
        Suits suit = new Suits();

        

        public Card(Value value, Suits suit)
        {
            this.value = value;
            this.suit = suit;
        } 

        public string cardOutput()
        {
            return this.value.ToString() + "-" + this.suit.ToString();
        }
         
        /// <summary>
        /// returns Value
        /// </summary>
        public Value GetValue()
        {
            return this.value;
        }
         
    }
}
