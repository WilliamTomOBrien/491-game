using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Entity : MonoBehaviour {
    private int maxHP = 100;
    private int hp = 100;
    private int cardsPerTurn = 5;
    private int handLimit = 5;
    private int energyPerTurn = 4;
    private int currentEnergy = 4;

    private List<GameObject> hand;

    private List<Card> allCards;
    private List<Card> deck;
    private List<Card> discards;
    private List<Card> trash;


    void Awake () {
		// hand = new List<GameObject>();
        // allCards = new List<CardState>();
        // deck = new List<CardState>();
        // discards = new List<CardState>();
        // trash = new List<CardState>();
	}




}