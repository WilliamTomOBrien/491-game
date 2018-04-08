using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public CardState cardState;
    public Color baseColor = Color.black;
    public Color highlightedColor;
    
    void Awake () {
        //cardState = new cardState()
    }

    private SpriteRenderer GetRenderer() {
        return gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void SetSprite(SpriteRenderer r, string filePath) {
        r.sprite = Resources.Load<Sprite>(filePath);
    }

    public void Highlight(){
        SpriteRenderer r = GetRenderer();
        SetSprite(r, "Sprites/CardFrontHighlighted");
        r.sortingOrder = 2;
    }

    public void UnHighlight() {
        SpriteRenderer r = GetRenderer();
        SetSprite(r, "Sprites/CardFront");
        r.sortingOrder = 0;
    }

    public void HighlightSelect(){
        SpriteRenderer r = GetRenderer();
        SetSprite(r, "Sprites/CardFrontSelected");
        r.sortingOrder = 2;
    }


    public bool IsHighlighted() {
        SpriteRenderer r = GetRenderer();
        return r.sprite == Resources.Load<Sprite>("Sprites/CardFrontHighlighted");
    }

    public void AddState(CardState c) {
        cardState = c;
    }

	public void ActivateCard () {
        
        if(GameObject.FindWithTag("CurrentEntity").GetComponent<Entity>() is Player) {
            Renderer[] r = gameObject.GetComponents<Renderer>();
            for(int i = 0; i < r.Length; i++){
                Debug.Log("Fam I ran");
                r[i].enabled = false;
            }
            //Get input needed, 
            //give it to function in cardState
            //repeat until finished

            StartCoroutine("DoTasks");
        }
	}

    void OnMouseDown(){
        ActivateCard();
    }

    override
    public string ToString(){
        return cardState.name;
    }

    private IEnumerator DoTasks() {

        GameController controller = GameController.GetGameController();
        controller.SetBoolCheckEndOfTurn(false);

        bool flag = false;

        for(int i = 0; i < cardState.tasks.Count; i++) {
            //set the appropriate task
            Debug.Log(i);

            switch (cardState.tasks[i].type) {

                case Task.Input.Null:
                    cardState.tasks[i].Run(null);
                    break;

                case Task.Input.Enemy:
                    controller.SetSelectionType(GameController.SelectionType.SelectEntity);
                    while (!(controller.input.GetComponent<Entity>() is Enemy)) { yield return null; }
                    cardState.tasks[i].Run(controller.input);
                    break;

                case Task.Input.Player:
                    controller.SetSelectionType(GameController.SelectionType.SelectEntity);
                    while (!(controller.input.GetComponent<Entity>() is Player)) {
                        yield return null;
                    }
                    cardState.tasks[i].Run(controller.input);
                    break;

                case Task.Input.Entity:
                    Debug.Log("This is run");
                    flag = true;
                    controller.SetSelectionType(GameController.SelectionType.SelectEntity);
                    Debug.Log(controller.input == null);
                    while (controller.input == null) {
                        Debug.Log("Im running here");
                        yield return null;
                    }
                    StartCoroutine(flash(controller.input));
                    cardState.tasks[i].Run(controller.input);
                    //controller.SetInputToNull();

                    Debug.Log("This was run");
                    break;
                case Task.Input.Card:
                    controller.SetSelectionType(GameController.SelectionType.SelectCard);
                    while(controller.input == null){
                        yield return null;
                    }
                    cardState.tasks[i].Run(controller.input);
                    break;
            }
        }

        //Add the card state to the current 
        //players discard pile
        Entity.UnHighlightAll();

        GameObject j = GameObject.FindWithTag("CurrentEntity");
        Entity e = j.GetComponent<Entity>();
        Player p = (Player) e;
        p.LoseEnergy(cardState.cost);

        p.hand.Remove(gameObject);
        p.discard.Add(cardState);

        if(!flag) Destroy(gameObject);
        GameController.GetGameController().SetBoolCheckEndOfTurn(true);





        GameController.GetGameController().SetInputToNull();
        //We want to repeat using the same input when we can, but only for the same card, so we 
        //have to set it to null in between
        GameController.GetGameController().SetSelectionType(GameController.SelectionType.SelectCardToPlay);

        //set the gameController back to the normal card selection
    }

    public IEnumerator flash(GameObject g) {
        int i = 0;
        while(i < 10){
            i++;
            Debug.Log("flash");

                Renderer[] r = g.GetComponents<Renderer>();
                for(int k = 0; k < r.Length; k++){
                    r[k].enabled = !r[k].enabled;
                }
    
            yield return new WaitForSeconds(.08f);
        }
        Destroy(gameObject);
    }
}
