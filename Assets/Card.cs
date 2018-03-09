using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour {

    //private CardState cardState;

    void Awake () {

    }

	void OnMouseDown () {
        Destroy(gameObject);

        //Get input needed, 
        //give it to function in cardState
        //repeat until finished

        Debug.Log("You hit the card fam");
	}

    override
    public string ToString(){
        return "Card";
    }
}
