using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    Player currentEntity;

	public GameObject[] entities;
	public GameObject input;

	int numObjects;
	int numIndex;

	public KeyCode prevCode = KeyCode.A;

	public enum SelectionType {
		SelectCardToPlay,
		SelectCard,
		SelectEntity
	};

	public SelectionType selectionType;

	void Awake () {
		selectionType = SelectionType.SelectCardToPlay;
		entities = new GameObject[4];

		entities[0] = Instantiate(Resources.Load("Player"), new Vector2(7, 3), Quaternion.identity) as GameObject;
		entities[0].tag = "CurrentEntity";

		entities[1] = Instantiate(Resources.Load("Player"), new Vector2(7, 2), Quaternion.identity) as GameObject;
		entities[2] = Instantiate(Resources.Load("Enemy"), new Vector2(-2, 3), Quaternion.identity) as GameObject;
		entities[3] = Instantiate(Resources.Load("Enemy"), new Vector2(-2, 2), Quaternion.identity) as GameObject;

	}

	// Use this for initialization
	void Start () {


		currentEntity = entities[0].GetComponent<Player>();

		currentEntity.BeginTurn();
	}

	// Update is called once per frame
	void Update () {
		if(selectionType == SelectionType.SelectCardToPlay && currentEntity.HasCards()){
			Debug.Log("The Highlighted Card is highlighted: " + currentEntity.CardIsHighlighted(numIndex));
			Debug.Log("Selected Card: " + numIndex);

			numObjects = currentEntity.GetNumberOfCards();
			if(currentEntity.NoCardHighlighted() && currentEntity.HasCards()){
				currentEntity.HighlightCard(0);
				numIndex = 0;

			} else if(Input.GetKey(KeyCode.LeftArrow) && prevCode == KeyCode.A){
				currentEntity.UnHighlightCard(numIndex);
				numIndex = (numIndex - 1 + numObjects) % numObjects;
				currentEntity.HighlightCard(numIndex);
				prevCode = KeyCode.LeftArrow;

			} else if(Input.GetKey(KeyCode.RightArrow) && prevCode == KeyCode.A){
				currentEntity.UnHighlightCard(numIndex);
				numIndex = (numIndex + 1) % numObjects;
				currentEntity.HighlightCard(numIndex);
				prevCode = KeyCode.RightArrow;

			} else if(Input.GetKey(KeyCode.Return) && prevCode == KeyCode.A) {
				prevCode = KeyCode.Return;
				currentEntity.ActivateCard(numIndex);

				if(currentEntity.HasCards()){
					currentEntity.HighlightCard(0);
					numIndex = 0;
				}
				//break out of this loop if we here and we have no cards fam

			} else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Return)){
				//Do nothing
				//Player is holding down the key
			} else {
				prevCode = KeyCode.A;
			}

			
		} else if(selectionType == SelectionType.SelectEntity){
			numObjects = entities.Length;
			Debug.Log("numIndex: " + numIndex);

			for(int i = 0; i < 4; i++) {
				if(entities[i].GetComponent<Entity>().isHighlighted()) Debug.Log("Entity " + i + " is highlighted!");
			}

			if(NoEntitiesHighlighted()){
				Debug.Log("No Entities Highlighted Now!");
				entities[0].GetComponent<Entity>().Highlight();
				numIndex = 0;

			} else if(Input.GetKey(KeyCode.LeftArrow) && prevCode == KeyCode.A){
				entities[numIndex].GetComponent<Entity>().UnHighlight();
				numIndex = (numIndex - 1 + numObjects) % numObjects;
				entities[numIndex].GetComponent<Entity>().Highlight();
				prevCode = KeyCode.LeftArrow;

			} else if(Input.GetKey(KeyCode.RightArrow) && prevCode == KeyCode.A){
				entities[numIndex].GetComponent<Entity>().UnHighlight();
				numIndex = (numIndex + 1) % numObjects;
				entities[numIndex].GetComponent<Entity>().Highlight();
				prevCode = KeyCode.RightArrow;

			} else if(Input.GetKey(KeyCode.Return) && prevCode == KeyCode.A) {
				prevCode = KeyCode.Return;
				input = entities[numIndex];
				entities[numIndex].GetComponent<Entity>().UnHighlight();


			} else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Return)){
				//Do nothing
				//Player is holding down the key
			} else {
				Debug.Log("I am here putting prevCode to A");
				prevCode = KeyCode.A;

			}


		}
	}

	private bool NoEntitiesHighlighted(){
		for(int i = 0; i < entities.Length; i++){
			if(entities[i].GetComponent<Entity>().isHighlighted()) return false;
		}
		return true;
	}

	public void SetSelectionType(SelectionType s){
		selectionType = s;
	}

	public void SetInputToNull() {
		input = null;
	}

}
