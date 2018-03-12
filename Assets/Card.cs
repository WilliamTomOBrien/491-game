using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour {

    private CardState cardState;
    private Coroutine nudger = null;
    public Color baseColor = Color.black;
    public Color highlightedColor; 

    private int i = 0;

    void Awake () {

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

	public void ActivateCard () {
        
        if(GameObject.FindWithTag("CurrentEntity").GetComponent<Entity>() is Player){
            //Get input needed, 
            //give it to function in cardState
            //repeat until finished



            StartCoroutine(DoTasks(cardState.tasks));

            //Add the card state to the current 
            //players discard pile
            GameObject j = GameObject.FindWithTag("CurrentEntity");
            Entity e = j.GetComponent<Entity>();
            Player p = (Player) e;
            p.LoseEnergy(cardState.cost);

            p.hand.Remove(gameObject);
            p.discard.Add(cardState);

            Destroy(gameObject);
            

        }
	}

    void OnMouseDown(){
        ActivateCard();
    }

    override
    public string ToString(){
        return cardState.name;
    }

    private IEnumerator DoTasks(List<Task> tasks) {
        for(int i = 0; i < tasks.Count; i++) {
            //set the appropriate task
            Debug.Log(i);
           if(cardState.tasks[i].type == Task.Input.Null){
               cardState.tasks[i].Run(null);

           } else if (cardState.tasks[i].type == Task.Input.Enemy){
                GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetSelectionType(GameController.SelectionType.SelectEntity);
                while(!(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input.GetComponent<Entity>() is Enemy)) {yield return null;}
                cardState.tasks[i].Run(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input); 

           } else if (cardState.tasks[i].type == Task.Input.Player){
                GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetSelectionType(GameController.SelectionType.SelectEntity);
                while(!(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input.GetComponent<Entity>() is Player)) {
                    
                    yield return null;
                }
                cardState.tasks[i].Run(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input);

           } else if (cardState.tasks[i].type == Task.Input.Entity){
                Debug.Log("This is run");
                GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetSelectionType(GameController.SelectionType.SelectEntity);
                Debug.Log(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input == null);
                while(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input == null) {
                    Debug.Log("Im running here");
                    yield return null;
                }
                cardState.tasks[i].Run(GameObject.FindWithTag("MainCamera").GetComponent<GameController>().input);
                GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetInputToNull();

                Debug.Log("This was run");

           }
        }

        GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetInputToNull();
        //We want to repeat using the same input when we can, but only for the same card, so we 
        //have to set it to null in between
        GameObject.FindWithTag("MainCamera").GetComponent<GameController>().SetSelectionType(GameController.SelectionType.SelectCardToPlay);

        //set the gameController back to the normal card selection
    }
}
