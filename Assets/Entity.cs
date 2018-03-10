using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Entity : MonoBehaviour {
    public int maxHP = 100;
    public int hp = 100;

    public List<CardState> allCards;

    void Awake () {
        allCards = new List<CardState>();
	}

    public virtual void BeginTurn(){

    }




}