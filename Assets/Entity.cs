using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity {

    private GameState state;

    private int maxHP;
    private int hp;
    private int cardsPerTurn;
    private int energyPerTurn;
    // If this > 10, update the player class regex
    private int handLimit = 5;
    private int currentEnergy;

    private List<Card> allCards;
    private List<Card> deck;
    private List<Card> hand;
    private List<Card> discards;
    private List<Card> trash;

    private bool alive = true;

    public Entity(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> startingCards) {
        maxHP = startingHP;
        hp = startingHP;
        this.cardsPerTurn = cardsPerTurn;
        this.energyPerTurn = energyPerTurn;
        currentEnergy = energyPerTurn;
        allCards = new List<Card>(startingCards);
        deck = new List<Card>();
        hand = new List<Card>();
        discards = new List<Card>();
        trash = new List<Card>();
    }

    public void SetState(GameState state) {
        this.state = state;
    }

    public GameState GetState() {
        return state;
    }

    public int GetMaxHP() {
        return maxHP;
    }
    public int GetEnergy() {
        return currentEnergy;
    }

    public void LoseEnergy(int n) {
        currentEnergy -= n;
    }

    public int GetHP() {
        return hp;
    }

    public void Damage(int amount) {
        if (alive) {
            SetHP(hp - amount);
        }
    }

    public void Heal(int amount) {
        if (alive) {
            SetHP(hp + amount);
        }
    }

    public void SetHP(int amount) {
        hp = amount;
        if (hp > maxHP) {
            hp = maxHP;
        }
        if (hp <= 0) {
            hp = 0;
            alive = false;
            state.Kill(this);
        }
    }

    public List<Card> GetAllCards() {
        return allCards;
    }

    public List<Card> GetDeck() {
        return deck;
    }

    public List<Card> GetHand() {
        return hand;
    }

    public List<Card> GetDiscards() {
        return discards;
    }

    public List<Card> GetTrash() {
        return trash;
    }

    public bool Discard(Card card) {
        if (hand.Remove(card)) {
            discards.Add(card);
            return true;
        }
        return false;
    }

    public void DisplayHand() {
        for (int i = 0; i < hand.Count; i++) {
            GameState.UnityOutput(i + ": " + hand[i].ToString());
        }
    }

    public bool Trash(Card card) {
        if (hand.Remove(card) || deck.Remove(card)) {
            trash.Add(card);
            return true;
        }
        return false;
    }

    public bool Destroy(Card card) {
        deck.Remove(card);
        hand.Remove(card);
        discards.Remove(card);
        trash.Remove(card);
        return allCards.Remove(card);
    }

    public bool BeginTurn() {
        currentEnergy = energyPerTurn;
        for (int i = 0; i < cardsPerTurn; i++) {
            if (!DrawCard()) {
                return false;
            }
        }
        return true;
    }

    public bool EndTurn() {
        discards.AddRange(hand);
        hand.Clear();
        return true;
    }

    public void BeginFight() {
        deck.Clear();
        discards.Clear();
        hand.Clear();
        trash.Clear();
        foreach (Card card in allCards) {
            deck.Add(card);
        }
        Shuffle(deck);
    }

    public void TakeTurn() {
        BeginTurn();
        TurnLogic();
        EndTurn();
    }
    public abstract void TurnLogic();

    public bool DrawCard() {
        if (deck.Count == 0) {
            if (discards.Count == 0) {
                return false;
            }
            foreach (Card card in discards) {
                deck.Add(card);
            }
            discards.Clear();
            Shuffle(deck);
        }
        Card drawCard = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        if (hand.Count == handLimit) {
            discards.Add(drawCard);
            return false;
        }
        hand.Add(drawCard);
        return true;
    }

    public Card GetCard(int i) {
        return hand[i];
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}