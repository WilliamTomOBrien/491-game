using System;
using System.Collections.Generic;
 using UnityEngine;

public abstract class Enemy : IEntity {

    public Enemy(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> cards) : base(startingHP, cardsPerTurn, energyPerTurn, cards) {
    }
}