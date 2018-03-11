using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    Player currentEntity;
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
	}

	// Use this for initialization
	void Start () {
		GameObject g = Instantiate(Resources.Load("Player"), new Vector2(7, 3), Quaternion.identity) as GameObject;
		currentEntity = g.GetComponent<Player>();
		g.tag = "CurrentEntity";

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

			
		}
	}

}
