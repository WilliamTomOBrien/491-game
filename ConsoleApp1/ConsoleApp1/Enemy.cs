using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public abstract class Enemy : IEntity {

        public Enemy(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> cards) : base(startingHP, cardsPerTurn, energyPerTurn, cards) {
        }
    }
}