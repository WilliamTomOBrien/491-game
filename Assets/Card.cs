using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour {

    private CardState cardState;

    void Awake () {

    }

    public void AddState(CardState c) {
        cardState = c;
    }

	void OnMouseDown () {
        Destroy(gameObject);

        //Get input needed, 
        //give it to function in cardState
        //repeat until finished

        for(int i = 0; i < cardState.tasks.Count; i++){
            if(cardState.tasks[i].type == Task.Input.Null) cardState.tasks[i].Run(null);
        }

        //Add the card state to the current 
        //players discard pile

        Debug.Log("You hit the card fam");
	}

    override
    public string ToString(){
        return cardState.name;
    }
}
