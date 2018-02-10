using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public abstract class Player : IEntity {

        private int maxHP;
        private int hp;

        private List<Card> classCards;

        private List<Card> allCards;
        private List<Card> deck;
        private List<Card> hand;
        private List<Card> discards;
        private List<Card> trash;

        public Player(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> classCards, List<Card> startingCards) : base(startingHP, cardsPerTurn, energyPerTurn, startingCards) {
            this.classCards = classCards;
            maxHP = startingHP;
            hp = startingHP;
            allCards = startingCards;
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
                new TestCard()
            };
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