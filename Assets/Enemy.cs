using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Enemy : Entity {

    public List<CardState> hand;

    public List<CardState> deck;
    public List<CardState> discard;


    void Awake(){
        allCards = new List<CardState>();

        hand = new List<CardState>();

        deck = new List<CardState>();
        discard = new List<CardState>();

        Task ex = new Example();
        List<Task> li = new List<Task>();

        li.Add(ex);

        for(int i = 0; i < 50; i++){
            deck.Add(new CardState("example", 1, li));
        }
    }

    override public void BeginTurn(){
        // for(int i = 0; i < 5; i++) {
        //   hand.Add(Instantiate(Resources.Load("Card"), new Vector2(i*2, 0), Quaternion.identity) as GameObject);
		//   hand[i].GetComponent<Card>().AddState(deck[0]);
        //   deck.Remove(deck[0]);
		//   Debug.Log("Made Card: " + i);
        // }
    }

    public void ActivateCard(int i){
        //hand[i].ActivateCard();
    }


}