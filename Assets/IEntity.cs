using System;
using System.Collections.Generic;
using UnityEngine;


    public abstract class IEntity {

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

        public IEntity(int startingHP, int cardsPerTurn, int energyPerTurn, List<Card> startingCards) {
            maxHP = startingHP;
            hp = startingHP;
            this.cardsPerTurn = cardsPerTurn;
            this.energyPerTurn = energyPerTurn;
            allCards = new List<Card>(startingCards);
            deck = new List<Card>();
            hand = new List<Card>();
            discards = new List<Card>();
            trash = new List<Card>();

            currentEnergy = energyPerTurn;
        }

        public int getMaxHP() {
            return maxHP;
        }
        public int getEnergy(){
            return currentEnergy;
        }


        public void loseEnergy(int n){
            currentEnergy -= n;
        }


        public int GetHP() {
            return hp;
        }
        public void SetHP(int amount) {
            hp = amount;
            if (hp > maxHP) {
                hp = maxHP;
            }
            if (hp <= 0) {
                hp = 0;
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

    	public void displayHand(){
	        for(int i = 0; i < hand.Count; i++) {
                Debug.Log(i + ": " + hand[i].ToString());
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

        public bool DrawHand() {
            for (int i = 0; i < cardsPerTurn; i++) {
                if (!DrawCard()) {
                    return false;
                }
            }
            return true;
        }

        public bool DiscardHand() {
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

        public void TakeTurn(GameState state) {
            DrawHand();
            TurnLogic(state);
            DiscardHand();
        }
        public abstract void TurnLogic(GameState state);

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

        public Card getCard(int i){
            return hand[i];
        }

        private static System.Random rng = new System.Random();  

        public static void Shuffle<T>(IList<T> list)  
        {  
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