using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour {

    private CardState cardState;
    GameObject input = null;

    public Color baseColor;
    public Color highlightedColor; 

    void Awake () {
        baseColor = GetComponent<Renderer>().material.color;
        highlightedColor = UnityEngine.Color.green;
    }

    public void Highlight(){
        gameObject.GetComponent<Renderer>().material.color = highlightedColor;

    }
    public void UnHighlight(){
        gameObject.GetComponent<Renderer>().material.color = baseColor;
    }

    public bool isHighlighted(){
        return !(gameObject.GetComponent<Renderer>().material.color == baseColor);
    }

    public void AddState(CardState c) {
        cardState = c;
    }

	void OnMouseDown () {
        

        //Get input needed, 
        //give it to function in cardState
        //repeat until finished

        for(int i = 0; i < cardState.tasks.Count; i++){
            if(cardState.tasks[i].type == Task.Input.Null) cardState.tasks[i].Run(null);
        }

        //Add the card state to the current 
        //players discard pile
        GameObject j = GameObject.FindWithTag("CurrentEntity");
        Entity e = j.GetComponent<Entity>();
        if(e is Player){
            Player p = (Player) e;
            p.hand.Remove(gameObject);
            p.discard.Add(cardState);

            Destroy(gameObject);
            Debug.Log("You hit the card fam");
        }
	}

    override
    public string ToString(){
        return cardState.name;
    }

    private IEnumerator DoTasks(List<Task> tasks) {
        for(int i = 0; i < tasks.Count; i++) {
            //set the appropriate task
            while(input == null){
               yield return null;
            }
            cardState.tasks[i].Run(input);
            input = null;
        }
        //set the gameController back to the normal card selection
    }
}
