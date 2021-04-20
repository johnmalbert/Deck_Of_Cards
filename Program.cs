using System;
using System.Collections.Generic;

namespace Deck_of_Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#################################################################");
            Console.WriteLine("Welcome. This console app is pretty cool. A deck of 52 cards is created. \nNext, Five cards are dealt from the deck. The deck is then reset with 52 new cards. \nThe deck can also be shuffled, using a random array of 52 numbers. \nA player is created, then draws five cards.\nPlayers can also discard a card from their hand, by index number.");
            Deck d = new Deck();
            Console.WriteLine();
            Console.WriteLine("New deck created!");
            Console.WriteLine($"Dealing five cards from the deck:");
            Console.WriteLine();
            Card dealt = d.Deal();
            Card d2 = d.Deal();
            Card d3 = d.Deal();
            Card d4 = d.Deal();
            Card d5 = d.Deal();
            Console.WriteLine($"Dealt cards: {dealt}, {d2}, {d3}, {d4}, {d5}.");
            Console.WriteLine();
            Console.WriteLine("Resetting the deck");
            d.Reset();
            Console.WriteLine();
            d.ShuffleDeck();
            Console.WriteLine();
            Console.WriteLine("New player created, named Johnny.");
            Player player1 = new Player("Johnny");
            player1.Draw(d);
            player1.Draw(d);
            player1.Draw(d);
            player1.Draw(d);
            player1.Draw(d);
            player1.Draw(d);
            player1.PrintHand();
            player1.Discard(2);
        }
    }
        // Create a class called "Card"
    class Card
    {
        public string stringVal;
        public string suit;
        public int val;

        public Card(string s, string Suit, int value)
        {
            stringVal = s;
            suit = Suit;
            val = value;
        }        
        public override string ToString()
        {
            return $"{stringVal} of {suit}";
        }
    }
    class Deck 
    {
        public List<Card> Cards;

        public Deck()
        {
            List<string> Suits = new List<string>()
            {
                "Spades","Diamonds","Hearts","Clubs"
            };


            List<int> Vals = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13
            };

            List<string> Faces = new List<string>()
            {
                "Ace","2","3","4","5","6","7","8","9","10","Jack","Queen","King"
            };

            Cards = new List<Card>();
            for (int i = 0; i < Suits.Count; i++)
            {
                for (int j = 0; j < Vals.Count; j++)
                {
                    Cards.Add(new Card(Faces[j],Suits[i],Vals[j]));
                }
            }
        }

        public Card Deal()
        {
            //Give the Deck a deal method that selects the "top-most" card, removes it from the list of cards, and returns the Card.
            if(this.Cards.Count > 0)
            {
                Card card_to_deal = this.Cards[0];
                this.Cards.RemoveAt(0);
                // Console.WriteLine($"Dealt the {card_to_deal}");
                return card_to_deal;                
            }
            else 
            {
                Console.WriteLine("No cards left in deck");
                return null;
            }
        }
        public void Reset()
        {
            //remove all the cards from deck
            this.Cards = null;
            //create a new deck
            Cards = new List<Card>();
            List<string> Suits = new List<string>()
            {
                "Spades","Diamonds","Hearts","Clubs"
            };

            List<int> Vals = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13
            };

            List<string> Faces = new List<string>()
            {
                "Ace","2","3","4","5","6","7","8","9","10","Jack","Queen","King"
            };
            for (int i = 0; i < Suits.Count; i++)
            {
                for (int j = 0; j < Vals.Count; j++)
                {
                    Cards.Add(new Card(Faces[j],Suits[i],Vals[j]));
                }
            }
        }

        public void ShuffleDeck()
        {
            int[] randomArray = Shuffle(this.Cards.Count);
            Deck Shuffled = new Deck();
            Shuffled.Cards.RemoveRange(0,52); // clear the deck to make room for the shuffled deck.
            foreach(int number in randomArray)
            {
                if(number == -1)
                {
                    Shuffled.Cards.Add(this.Cards[0]); //for some reason Shuffle returns a -1 intead of a 0...
                }
                else
                {
                    Shuffled.Cards.Add(this.Cards[number]);
                }
            }
            this.Cards = null;
            this.Cards = Shuffled.Cards;
            Console.WriteLine("Shuffled the deck.");
        }
        public static int[] Shuffle(int num)
        //this function returns an array of shuffled numbers
        {
            Random rand = new Random();
            int[] arr = new int[num];
            arr[0] = rand.Next(1,num + 1);
            // populate each value of the array:
            for(int i = 0; i < num; i++)
            {
                // if the random number already exists in the array, get a new random number
                int check = rand.Next(0,num + 1) - 1;
                for(int j = 0; j < num; j++){
                    // Console.WriteLine($"Checking {arr[j]} and {check}");
                    if(arr[j] == check){
                        check = rand.Next(0,num + 1) - 1;
                        // Console.WriteLine("Changed check");
                        j = -1;
                    }
                }
                arr[i] = check;
                // if it doesn't exist, assign it to the next position in the array
            }
            return arr;
        }
    }
    class Player
    {
        // Give the Player class a name property.
        public string name;

        // Give the Player a hand property that is a List of type Card.
        public List<Card> hand = new List<Card>();
 
        //constructor
        public Player(string player_name)
        {
            name = player_name;
        }

        // Give the Player a draw method of which draws a card from a deck, adds it to the player's hand and returns the Card. Note this method will require reference to a deck object
        public Card Draw(Deck d)
        {
            //get the top card
            Card dealtCard = d.Deal();
            this.hand.Add(dealtCard);
            return dealtCard;
        }
        // Give the Player a discard method which discards the Card at the specified index from the player's hand and returns this Card or null if the index does not exist.
        public Card Discard(int index)
        {
            //get the card at the index, if possible.
            try
            {
                Card discardCard = this.hand[index];
                this.hand.Remove(discardCard);
                Console.WriteLine($"{this.name} is discarding the {discardCard}");
                return discardCard;
            }
            catch
            {
                Console.WriteLine("No such card at that index in your hand.");
                return null;
            }
        }

        public void PrintHand()
        {   
            Console.WriteLine($"{this.name}'s hand:");
            foreach(var card in this.hand)
            {
                Console.WriteLine(card);
            }
        }
    }
// 
}