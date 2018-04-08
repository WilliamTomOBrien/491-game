using System;
using System.Collections;
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

    public virtual void EndTurn(){

    }

    public virtual void ActivateCard(){
        
    }

    public void Highlight() {
        if(GameObject.FindWithTag("Arrow")){
            Destroy(GameObject.FindWithTag("Arrow"));
        }

        gameObject.tag = "HighlightedEntity";
        Vector3 currentEntityVector = GameController.GetGameController().currentEntity.transform.position - new Vector3(0, 1.8f, 0f);
        GameObject g = Instantiate(Resources.Load("Arrow"), currentEntityVector, Quaternion.identity) as GameObject;
        g.tag = "Arrow";

        StartCoroutine(UpAndDown(g, currentEntityVector, currentEntityVector - new Vector3(0,1f,0), 50));

    }

    public void UnHighlight(){
        if(gameObject.tag != "CurrentEntity") gameObject.tag = "Entity";
    }

    public static void UnHighlightAll(){
        if(GameObject.FindWithTag("Arrow")){
            Destroy(GameObject.FindWithTag("Arrow"));
        }
    }

    public bool IsHighlighted(){
        return !(gameObject.tag == "HighlightedEntity");
    }

    private static IEnumerator UpAndDown(GameObject g, Vector3 start, Vector3 finish, int loops){
        while(true){
           for(int i = 0; i < loops; i++){
                if(g == null) break;

                float f = 1/ (float) loops;
                Debug.Log("Coroutine going");
                g.transform.position = Vector3.Lerp(start, finish, i*f);
                yield return null;
            }
        
            for(int i = 0; i < loops; i++){
                if(g == null) break;

                float f = 1/ (float) loops;
                Debug.Log("Coroutine going");
                g.transform.position = Vector3.Lerp(finish, start, i*f);
                yield return null;
            }

        }

    }


    
}