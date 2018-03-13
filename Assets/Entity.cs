using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Entity : MonoBehaviour {
    public int maxHP = 100;
    public int hp = 100;

    public Color baseColor = Color.black;
    public Color highlightedColor;

    public List<CardState> allCards;

    void Awake () {
        allCards = new List<CardState>();
        gameObject.GetComponent<Renderer>().material.color = baseColor;
        //highlightedColor = Color.green;
	}

    public virtual void BeginTurn(){
        
    }

    public virtual void EndTurn(){

    }

    public virtual void ActivateCard(){
        
    }

    public void Highlight(){
        gameObject.GetComponent<Renderer>().material.color = highlightedColor;
        Debug.Log("This is highlighting: " + (gameObject.GetComponent<Renderer>().material.color == highlightedColor));
        Debug.Log("Right: " + (highlightedColor == baseColor));

    }
    public void UnHighlight(){
        gameObject.GetComponent<Renderer>().material.color = baseColor;
    }

    public bool isHighlighted(){
        return !(gameObject.GetComponent<Renderer>().material.color == baseColor);
    }

}