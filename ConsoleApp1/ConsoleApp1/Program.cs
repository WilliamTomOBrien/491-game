using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public class Program {

        public enum Rarity {
            Common = 0,
            Uncommon = 1,
            Rare = 2
        }

        static void Main(string[] args) {
            
            List<Card> enemyCards = new List<Card> {
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard(),
                new TestCard()
            };

            Player p1 = new Wizard();
            Player p2 = new Wizard();

            IEntity enemy1 = new Pawn();

            GameState state = new GameState();
            state.AddEntity(p1);
            state.AddEntity(p2);
            state.AddEntity(enemy1);
            
            // Test code (unfinished) -------------------------------
            int i = 1;
            foreach (Player player in state.GetPlayers()) {
                Console.WriteLine("Player " + i + "'s turn.");
                int cardNum = 1;
                foreach (Card card in player.GetHand()) {
                    Console.WriteLine(cardNum + ") " + card);
                    cardNum++;
                }
                i++;
            }
            // ------------------------------------------------------

            Console.ReadKey();
        }
    }
}