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
    public int numOfCards = 5;

    public float r = 4f;

    void Awake() {
        allCards = new List<CardState>();

        hand = new List<GameObject>();

        deck = new List<CardState>();
        discard = new List<CardState>();
        trash = new List<CardState>();

        energyPerTurn = 4;
        currentEnergy = energyPerTurn;

        Task ex = new Damage(10);
        List<Task> li = new List<Task> {
            ex
        };

        CardState baseCard = new CardState("example", 1, li, "Sounds/PunchSound");
        List<float> means = new List<float>();
        means.Add(9f);
        means.Add(10f);
        means.Add(11f);
        List<float> stdDev = new List<float>();
        stdDev.Add(2.5f);
        stdDev.Add(3.75f);
        stdDev.Add(1f);
        TaskBuilder strike = new TaskBuilder(new Damage(0), means, stdDev);
        List<TaskBuilder> tasks = new List<TaskBuilder>();
        tasks.Add(strike);
        CardStateBuilder stateBuilder = new CardStateBuilder(new StrikeState(), tasks);

        //CardPile.Add(stateBuilder);


        for (int i = 0; i < 50; i++){
            deck.Add(stateBuilder.GetCardState(1));
        }
    }


    override public void BeginTurn() {
        numOfCards = cardsPerTurn;
        currentEnergy = energyPerTurn;
        for(int i = 0; i < numOfCards; i++) {
          hand.Add(Instantiate(Resources.Load("Card"), new Vector2((float) (r*Math.Sin((Math.PI/180)*i*120/(numOfCards - 1) - (Math.PI/180)*60)),
          (float) (-6f + r*Math.Cos((Math.PI/180)*i*120/(numOfCards - 1) - (Math.PI/180)*60))), Quaternion.identity) as GameObject);
		  hand[i].GetComponent<Card>().SetState(deck[0]);
          deck.Remove(deck[0]);
		  Debug.Log("Made Card: " + i);
        }
        gameObject.tag = "CurrentEntity";
    }

    public void OrganizeCards(){
        numOfCards = hand.Count;
        float freedom = numOfCards*90f/5f;
        Debug.Log("FREEDOM:" + freedom + "");
        float bound = (180f - freedom)/2;
        if(numOfCards > 1){
            for(int i = 0; i < numOfCards; i++) {
                hand[i].transform.position = new Vector3((float) (r*Math.Cos((Math.PI/180)*(numOfCards - 1 - i)*freedom/(numOfCards - 1) + (Math.PI/180)*bound)),
                (float) (-6f + r*Math.Sin((Math.PI/180)*(numOfCards - 1 - i)*freedom/(numOfCards - 1) + (Math.PI/180)*bound)),0);
           }
        } else if(numOfCards == 1) {
            hand[0].transform.position = new Vector3(0, -1 ,0);
        }
    }


    public void AddCardState(CardState c){
        deck.Add(c);
    }

    override public void EndTurn() {
        int size = hand.Count;
        while(hand.Count != 0){
            discard.Add(hand[0].GetComponent<Card>().GetState());
            //Destroy(hand[i]);
            Destroy(hand[0]);
            hand.Remove(hand[0]);
        }
        //gameObject.tag = "Player";
    }

    public void ActivateCard(int i) {
        hand[i].GetComponent<Card>().ActivateCard();
    }

    public void HighlightCard(int i) {
        hand[i].GetComponent<Card>().Highlight();
    }

    public void UnHighlightCard(int i) {
        hand[i].GetComponent<Card>().UnHighlight();
    }

    public bool NoCardHighlighted() {
        for(int i = 0; i < hand.Count; i++){
            if(hand[i].GetComponent<Card>().IsHighlighted()){
                return false;
            }
        }
        return true;
    }

    public void UnSelectAll(){
        for(int i = 0; i < hand.Count; i++) {
            hand[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/FistStatic");
        }
    }

    public void HighlightCardSelect(int i) {
        hand[i].GetComponent<Card>().HighlightSelect();
    }

    public GameObject GetCard(int i){
        return hand[i];
    }

    public List<CardState> GetDiscard(){
        return discard;
    }

    public void LoseEnergy(int n) {
        currentEnergy -= n;
    }

    public bool CardIsHighlighted(int i) {
        return hand[i].GetComponent<Card>().IsHighlighted();
    }

    public bool HasCards() {
        return hand.Count != 0;
    }

    public int GetHandSize() {
        return hand.Count;
    }


}
