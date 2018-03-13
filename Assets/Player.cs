using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity {

    public List<GameObject> hand;

    public List<CardState> deck;
    public List<CardState> discard;
    public List<CardState> trash;

    public int cardsPerTurn = 5;
    public int handLimit = 5;
    public int energyPerTurn = 4;
    public int currentEnergy = 4;



    void Awake(){
        allCards = new List<CardState>();

        hand = new List<GameObject>();

        deck = new List<CardState>();
        discard = new List<CardState>();
        trash = new List<CardState>();

        energyPerTurn = 4;
        currentEnergy = energyPerTurn;

        Task ex = new Damage(10);
        List<Task> li = new List<Task>();

        li.Add(ex);

        for(int i = 0; i < 50; i++){
            deck.Add(new CardState("example", 1, li));
        }
    }


    

    override public void BeginTurn(){
        currentEnergy = energyPerTurn;
        for(int i = 0; i < 5; i++) {
          hand.Add(Instantiate(Resources.Load("Card"), new Vector2(i*2, 0), Quaternion.identity) as GameObject);
		  hand[i].GetComponent<Card>().AddState(deck[0]);
          deck.Remove(deck[0]);
		  Debug.Log("Made Card: " + i);
        }
        gameObject.tag = "CurrentEntity";
    }

    override public void EndTurn(){
        for(int i = 0; i < hand.Count; i++){
            discard.Add(hand[i].GetComponent<Card>().cardState);
            Destroy(hand[i]);
            hand.Remove(hand[i]);
        }
        gameObject.tag = "Player";
    }

    public void ActivateCard(int i){
        hand[i].GetComponent<Card>().ActivateCard();
    }

    public void HighlightCard(int i){
        Debug.Log("Highlighting Card: " + i);
        hand[i].GetComponent<Card>().Highlight();
    }

    public void UnHighlightCard(int i){
        hand[i].GetComponent<Card>().UnHighlight();
    }

    public bool NoCardHighlighted(){
        for(int i = 0; i < hand.Count; i++){
            if(hand[i].GetComponent<Card>().isHighlighted()){
                return false;
            }
        }
        return true;
    }

    public void LoseEnergy(int n){
        currentEnergy -= n;
    }

    public bool CardIsHighlighted(int i){
        return hand[i].GetComponent<Card>().isHighlighted();
    }

    public bool HasCards(){
        return !(hand.Count == 0);
    }

    public int GetNumberOfCards(){
        return hand.Count;
    }


}