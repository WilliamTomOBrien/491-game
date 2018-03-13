using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public Entity currentEntity;
	public Player currentPlayer;
	public Enemy currentEnemy;

	public List<GameObject> entities;
	public GameObject input;

	public bool canCheckForEndOfTurn = true;

	int entityIndex = 0;

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
		entities = new List<GameObject>();

		entities.Add(Instantiate(Resources.Load("Player"), new Vector2(7, 3), Quaternion.identity) as GameObject);
		entities[0].tag = "CurrentEntity";

		entities.Add(Instantiate(Resources.Load("Player"), new Vector2(7, 2), Quaternion.identity) as GameObject);
		entities.Add(Instantiate(Resources.Load("Enemy"), new Vector2(-2, 3), Quaternion.identity) as GameObject);
		entities.Add(Instantiate(Resources.Load("Enemy"), new Vector2(-2, 2), Quaternion.identity) as GameObject);

	}

	// Use this for initialization
	void Start () {
		entityIndex = 0;
		currentEntity = entities[entityIndex].GetComponent<Entity>();
		currentEntity.BeginTurn();
	}

	// Update is called once per frame
	void Update () {
		if(currentEntity is Player){
			currentPlayer = (Player) currentEntity;
			if(selectionType == SelectionType.SelectCardToPlay && currentPlayer.HasCards()){
				Debug.Log("The Highlighted Card is highlighted: " + currentPlayer.CardIsHighlighted(numIndex));
				Debug.Log("Selected Card: " + numIndex);

				numObjects = currentPlayer.GetNumberOfCards();
				Debug.Log("Number of Cards: " + currentPlayer.GetNumberOfCards());
				if(currentPlayer.NoCardHighlighted() && currentPlayer.HasCards()){
					currentPlayer.HighlightCard(0);
					numIndex = 0;

				} else if(Input.GetKey(KeyCode.LeftArrow) && prevCode == KeyCode.A){
					currentPlayer.UnHighlightCard(numIndex);
					numIndex = (numIndex - 1 + numObjects) % numObjects;
					currentPlayer.HighlightCard(numIndex);
					prevCode = KeyCode.LeftArrow;

				} else if(Input.GetKey(KeyCode.RightArrow) && prevCode == KeyCode.A){
					currentPlayer.UnHighlightCard(numIndex);
					numIndex = (numIndex + 1) % numObjects;
					currentPlayer.HighlightCard(numIndex);
					prevCode = KeyCode.RightArrow;

				} else if(Input.GetKey(KeyCode.Return) && prevCode == KeyCode.A) {
					prevCode = KeyCode.Return;
					currentPlayer.ActivateCard(numIndex);

					if(currentPlayer.HasCards()){
						currentPlayer.HighlightCard(0);
						numIndex = 0;
					}
					//break out of this loop if we here and we have no cards fam

				} else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Return)){
					//Do nothing
					//Player is holding down the key
				} else {
					prevCode = KeyCode.A;
				}


				if(currentPlayer.currentEnergy <= 0 || !currentPlayer.HasCards()){
					if(canCheckForEndOfTurn){
						currentPlayer.EndTurn();

						entityIndex = (entityIndex + 1) % entities.Count;
						currentEntity = entities[entityIndex].GetComponent<Entity>();
						currentEntity.BeginTurn();

					}
				}

			
			} else if(selectionType == SelectionType.SelectEntity){
				numObjects = entities.Count;

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
					numIndex = 0;

				} else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Return)){
					//Do nothing
					//Player is holding down the key
				} else {
					Debug.Log("I am here putting prevCode to A");
					prevCode = KeyCode.A;

				}


			}
		} else if(currentEntity is Enemy){
			currentEnemy = (Enemy) currentEntity;
			currentEnemy.ActivateEffect();

			entities[entityIndex].tag = "Enemy";
			entityIndex = (entityIndex + 1) % entities.Count;
			currentEntity = entities[entityIndex].GetComponent<Entity>();
			entities[entityIndex].tag = "CurrentEntity";
			currentEntity.BeginTurn();
		}
	}

	private bool NoEntitiesHighlighted(){
		for(int i = 0; i < entities.Count; i++){
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

	public void SetBoolCheckEndOfTurn(bool b){
		canCheckForEndOfTurn = true;
	}

}
