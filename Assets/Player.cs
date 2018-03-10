using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Player : Entity {

    private List<GameObject> hand;

    private List<CardState> deck;
    private List<CardState> discards;
    private List<CardState> trash;

    public int cardsPerTurn = 5;
    public int handLimit = 5;
    public int energyPerTurn = 4;
    public int currentEnergy = 4;



    void Awake(){
        allCards = new List<CardState>();

        hand = new List<GameObject>();

        deck = new List<CardState>();
        discards = new List<CardState>();
        trash = new List<CardState>();

        energyPerTurn = 4;
        currentEnergy = energyPerTurn;

        Task ex = new Example();
        List<Task> li = new List<Task>();

        li.Add(ex);

        for(int i = 0; i < 50; i++){
            deck.Add(new CardState("example", 0, li));
        }
    }

    override public void BeginTurn(){
        for(int i = 0; i < 5; i++) {
          hand.Add(Instantiate(Resources.Load("Card"), new Vector2(i*2, 0), Quaternion.identity) as GameObject);
		  hand[i].GetComponent<Card>().AddState(deck[0]);
          deck.Remove(deck[0]);
		  Debug.Log("Made Card: " + i);
        }
    }

}