using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp1 {

    public abstract class Player : IEntity {

        private int maxHP;
        private int hp;

        private List<Card> classCards;

        public Player(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> classCards, List<Card> startingCards) : base(startingHP, cardsPerTurn, energyPerTurn, startingCards) {
            this.classCards = classCards;
            maxHP = startingHP;
            hp = startingHP;
        } 

        override
        public void TurnLogic(GameState state) {
            if (state.GetCurrentEntity() == state.getEntity(0)) {
                Console.WriteLine("Player 1:");
            } else {
                Console.WriteLine("Player 2:");
            }
            List<Card> hand = GetHand();
            string s = "";
            while (hand.Count > 0 && s != " ") {
                int cardNum = 1;
                foreach (Card card in hand) {
                    Console.WriteLine(cardNum + ") " + card);
                    cardNum++;
                }
                Console.Write("Enter the number of a card to play, or press space to end turn: ");
                String validInput = "^[0-" + GetHand().Count + " ]$";
                Regex r = new Regex(validInput);
                Match m;
                do {
                    ConsoleKeyInfo input = Console.ReadKey();
                    s = "" + input.KeyChar;
                    m = r.Match(s);
                } while (!m.Success);
                Console.WriteLine();
                if (s != " ") {
                    hand[Int32.Parse(s)- 1].Run(state);
                }
                Console.WriteLine();
            }
            if (hand.Count == 0) {
                Console.WriteLine("Player hand is empty.");
            }
            Console.WriteLine();
        }
    }

    public class Wizard : Player {

        private static int startingHP = 100;
        private static int cardsPerTurn = 5;
        private static int energyPerTurn = 3;

        public Wizard() : base(startingHP, cardsPerTurn, energyPerTurn, getClassCards(), getStartingCards()) {
        }

        private static List<Card> getClassCards() {
            return new List<Card> {
                new Attack()
            };
        }

        private static List<Card> getStartingCards() {
            return new List<Card> {
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack()
            };
        }
    }
}