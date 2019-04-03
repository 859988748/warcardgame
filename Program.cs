using System;
using System.Collections.Generic;

namespace Card_Game {
    class Program {
        static void Main (string[] args) {
           
            Console.WriteLine("Welcome to the Big Bellies War Game!") ;
            Console.WriteLine("HOW TO PLAY:") ;
            Console.WriteLine("Each of the two players is dealt one half of a shuffled deck of cards.");
            Console.WriteLine("Each turn, each player draws one card from their decks. ");
            Console.WriteLine("The player that drew the card with higher value gets both cards.");
            Console.WriteLine("If there is a draw, they place another 3 cards and compare the 4th card.  If a draw happens again, the cards are put back into each players deck.");
            Console.WriteLine("HOW TO WIN:") ;
            Console.WriteLine("The player that gets all of cards first, wins.") ;
            Console.WriteLine("Press enter in order to draw a new card.");
            string name1; 
            string name2; 
            Console.WriteLine("Player1, Enter your name: ");
            name1 = Console.ReadLine();
            Console.WriteLine("Player2, Enter your name: ");
            name2 = Console.ReadLine();
            Console.WriteLine("Press enter, to begin the game!");

            Player player1=new Player(name1);
            Player player2=new Player(name2);
            
            // Game loop
            while (true) {
                int totalMoves = 0;

                // Generate deck
                WarDeck mainDeck = new WarDeck ();
                mainDeck.generateDeck ();
                //mainDeck.shuffleDeck ();

                // Split deck into 2 (1 for each player)
                WarDeck player1Deck = new WarDeck ();
                WarDeck player2Deck = new WarDeck ();

                bool toggle = false;

                foreach (WarCard card in mainDeck.stack) {
                    if (toggle) {
                        player1Deck.stack.Add (card);
                    } else {
                        player2Deck.stack.Add (card);
                    }
                    toggle = !toggle;
                }

                // Draw loop
                while (!player1Deck.isEmpty () && !player2Deck.isEmpty ()) {
                    Console.ReadLine ();

                    // Each player draws a card
                    WarCard player1Draw = (WarCard) player1Deck.drawCard ();
                    WarCard player2Draw = (WarCard) player2Deck.drawCard ();
                    totalMoves++;

                    Console.WriteLine (player1.name+" has drawn: {0} of {1}.\n", player1Draw.face, player1Draw.suite);
                    Console.WriteLine (player2.name+ " has drawn: {0} of {1}.\n\n", player2Draw.face, player2Draw.suite);

                    if ((int) player1Draw.face > (int) player2Draw.face) {
                        Console.WriteLine ("The "+player1.name+" has won the cards.\nThe cards have been placed in your deck.\n\n");
                        player1Deck.placeInDeck (player1Draw, player2Draw);
                    } else if ((int) player1Draw.face < (int) player2Draw.face) {
                        Console.WriteLine ("The "+player2.name+" has won the cards.\nThe cards have been placed in the player2's deck.\n\n");
                        player2Deck.placeInDeck (player1Draw, player2Draw);
                    } else {
                        Console.WriteLine ("It's a draw!\nLet's play War. You need to place 3 cards at one time and compare the 4th card.");
                        WarCard player1Draw1 = (WarCard) player1Deck.drawCard ();
                        WarCard player1Draw2 = (WarCard) player1Deck.drawCard ();
                        WarCard player1Draw3 = (WarCard) player1Deck.drawCard ();
                        WarCard player1Draw4 = (WarCard) player1Deck.drawCard ();
                        WarCard player2Draw1 = (WarCard) player2Deck.drawCard ();
                        WarCard player2Draw2 = (WarCard) player2Deck.drawCard ();
                        WarCard player2Draw3 = (WarCard) player2Deck.drawCard ();
                        WarCard player2Draw4 = (WarCard) player2Deck.drawCard ();
                        if((int) player1Draw4.face > (int) player2Draw4.face) {
                          Console.WriteLine ("The "+player1.name+" has won the cards.\nThe 8 cards have been placed in your deck.\n\n");
                          player1Deck.place8InDeck (player1Draw4, player1Draw2, player1Draw3, player1Draw4, player2Draw4, player2Draw2, player2Draw3, player2Draw4);
                        } else if ((int) player1Draw4.face < (int) player2Draw4.face){
                           Console.WriteLine ("The "+player2.name+" has won the cards.\nThe 8 cards have been placed in your deck.\n\n"); 
                           player2Deck.place8InDeck (player1Draw4, player1Draw2, player1Draw3, player1Draw4, player2Draw4, player2Draw2, player2Draw3, player2Draw4);
                       
                        } else {
                          Console.WriteLine ("It's a draw again!\nLet's throw the cards and restart ");
                        }
                    }

                    Console.WriteLine ("================================================================================" +
                                       "================================================================================");
                }

                if (player1Deck.isEmpty ()) {
                    Console.WriteLine ("After a total of {0} moves, the "+player2.name+" has won!\n\n", totalMoves);
                } else {
                    Console.WriteLine ("After a total of {0} moves, the "+player1.name+" won!\n\n", totalMoves);
                }

                string line;
                do {
                    Console.WriteLine ("Would you like to play again?\nIf yes, type 'y'. If not, type 'n'.\n");
                    line = Console.ReadLine ();
                } while (line != "n" && line != "y");

                if (line == "n") {
                    break;
                }
            }
        }
    }

    public class Player{
    public string name {get;set;}
    public Player(string nm){
      name=nm;
    }
    }
   
    /// <summary>
    /// A standard playing card for the game of War.
    /// </summary>
    public class WarCard : Card {
        public WarCard (Suite s, Face f) : base (s, f) {
        }

        public override void printCard () {
            throw new NotImplementedException ();
        }
        public override int getFaceValue () {
            return ((int) suite);
        }
    }

    /// <summary>
    /// A standard deck for the game of War.
    /// </summary>
    public class WarDeck : Deck {
        public override void generateDeck () {
            // Creation of each card suite deck chunk
            for (int k = 0; k < 4; k++) {
                // Creation of the individual card
                for (int i = 1; i < 14; i++) {
                    stack.Add (new WarCard ((Card.Suite) k, (Card.Face) i));
                }
            }
        }
        public override void shuffleDeck () {
            Random rng = new Random ();

            for (int i = stack.Count - 1; i > 1; i--) {
                int k = rng.Next (i);

                WarCard v = (WarCard) stack [k];
                stack [k] = stack [i];
                stack [i] = v;

            }
        }
        public override Card drawCard () {
            WarCard popCard = (WarCard) stack [0];
            stack.Remove (popCard);

            return (popCard);
        }
        public override void placeInDeck (Card c1, Card c2) {
            stack.Add (c1);
            stack.Add (c2);
            Console.WriteLine("The winner this round has "+stack.Count+" cards");
            int losercardnum=52-stack.Count;
            Console.WriteLine("The loser this round has "+losercardnum+" cards");
            shuffleDeck ();
        }

        public override void place8InDeck (Card c1, Card c2, Card c3, Card c4, Card c5, Card c6, Card c7, Card c8) {
            stack.Add (c1);
            stack.Add (c2);
            stack.Add (c3);
            stack.Add (c4);
            stack.Add (c5);
            stack.Add (c6);
            stack.Add (c7);
            stack.Add (c8);
            Console.WriteLine("The winner this round has "+stack.Count+" cards");
            int losercardnum=52-stack.Count;
            Console.WriteLine("The loser this round has "+losercardnum+" cards");
            shuffleDeck ();
        }
        public override bool isEmpty () {
            return (stack.Count <= 0);
        }
    }

    /// <summary>
    /// An abstract playing card.
    /// </summary>
    public abstract class Card {
        public enum Suite {Spades=0, Hearts, Clubs, Diamonds};
        public enum Face {Ace=1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King}

        public Suite suite;
        public Face face;

        public Card (Suite s, Face f) {
            this.suite = s;
            this.face = f;
        }

        public abstract void printCard ();
        public abstract int getFaceValue ();
    }

    /// <summary>
    /// An abstract card deck.
    /// </summary>
    public abstract class Deck {
        public List <Card> stack;

        public Deck () {
            this.stack = new List <Card> ();
        }

        public abstract void generateDeck ();
        public abstract void shuffleDeck ();
        public abstract Card drawCard ();
        public abstract void placeInDeck (Card c1, Card c2);
//add the bottom line
        public abstract void place8InDeck (Card c1, Card c2, Card c3, Card c4, Card c5, Card c6, Card c7, Card c8);
        public abstract bool isEmpty ();
    }
}