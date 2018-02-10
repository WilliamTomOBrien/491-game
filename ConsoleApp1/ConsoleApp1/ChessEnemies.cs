using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public class Pawn : Enemy {

        private static int startingHP = 16;
        private static int cardsPerTurn = 5;
        private static int energyPerTurn = 3;

        public Pawn() : base(startingHP, cardsPerTurn, energyPerTurn, getStartingCards()) {
        }

        private static List<Card> getStartingCards() {
            return new List<Card> {
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
        }
    }
}