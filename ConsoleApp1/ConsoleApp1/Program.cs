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

            Player p1 = new Wizard();
            Player p2 = new Wizard();

            IEntity enemy1 = new Pawn();

            GameState state = new GameState();
            state.AddEntity(p1);
            state.AddEntity(p2);
            state.AddEntity(enemy1);

            state.BeginFight();
            
            while(true) {
                state.NextTurn();
            }

            Console.ReadKey();
        }

        public static void outputList<E>(List<E> list) {
            foreach (E e in list) {
                Console.WriteLine(e);
            }
        }
    }
}