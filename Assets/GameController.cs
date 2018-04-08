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
    private int numCardsHighlighted = 0;

	int entityIndex = 0;

	int numObjects;
	int numIndex;

    private const KeyCode unusedKey = KeyCode.A;
	public KeyCode prevCode = unusedKey;

	public enum SelectionType {
		SelectCardToPlay,
		SelectCard,
		SelectEntity
	};

	public SelectionType selectionType;

	void Awake () {
		selectionType = SelectionType.SelectCardToPlay;
        entities = new List<GameObject> {
            Instantiate(Resources.Load("Player"), new Vector2(-3, 0), Quaternion.identity) as GameObject
        };
        entities[0].tag = "CurrentEntity";

		entities.Add(Instantiate(Resources.Load("Player"), new Vector2(-5, 0), Quaternion.identity) as GameObject);
		entities.Add(Instantiate(Resources.Load("EnemyMagician"), new Vector2(5, 0), Quaternion.identity) as GameObject);
		entities.Add(Instantiate(Resources.Load("EnemyMagician"), new Vector2(3, 0), Quaternion.identity) as GameObject);

	}

	// Use this for initialization
	void Start () {
		entityIndex = 0;
		currentEntity = entities[entityIndex].GetComponent<Entity>();
		currentEntity.BeginTurn();
	}

	// Update is called once per frame
	void Update () {
		if(currentEntity is Player) {
			currentPlayer = (Player) currentEntity;
			if(selectionType == SelectionType.SelectCardToPlay && currentPlayer.HasCards()){
				Debug.Log("The Highlighted Card is highlighted: " + currentPlayer.CardIsHighlighted(numIndex));
				Debug.Log("Selected Card: " + numIndex);

				numObjects = currentPlayer.GetHandSize();
				Debug.Log("Number of Cards: " + currentPlayer.GetHandSize());
                if (numCardsHighlighted == 0 && currentPlayer.HasCards()) {
                    currentPlayer.HighlightCard(0);
                    numCardsHighlighted++;
                    numIndex = 0;
                }

                KeyCode key = GetKey();
                if (prevCode == unusedKey) {
                    switch (key) {
                        case KeyCode.LeftArrow:
                            currentPlayer.UnHighlightCard(numIndex);
                            numIndex = (numIndex - 1 + numObjects) % numObjects;
                            currentPlayer.HighlightCard(numIndex);
                            break;

                        case KeyCode.RightArrow:
                            currentPlayer.UnHighlightCard(numIndex);
                            numIndex = (numIndex + 1) % numObjects;
                            currentPlayer.HighlightCard(numIndex);
                            break;

                        case KeyCode.Return:
                            currentPlayer.ActivateCard(numIndex);
                            numCardsHighlighted--;
                            break;
                    }
                }
                prevCode = key;

				if(currentPlayer.currentEnergy <= 0 || !currentPlayer.HasCards()){
					if(canCheckForEndOfTurn) {
						currentPlayer.EndTurn();

						entityIndex = (entityIndex + 1) % entities.Count;
						currentEntity = entities[entityIndex].GetComponent<Entity>();
						currentEntity.BeginTurn();

					}
				}

			
			} else if(selectionType == SelectionType.SelectEntity){
				numObjects = entities.Count;

                if (NoEntitiesHighlighted()) {
                    Debug.Log("No Entities Highlighted Now!");
                    HighlightEntity(0);
                    numIndex = 0;
                }

                KeyCode key = GetKey();
                if (prevCode == unusedKey) {
                    switch (key) {
                        case KeyCode.LeftArrow:
                            UnHighlightEntity(numIndex);
                            numIndex = (numIndex - 1 + numObjects) % numObjects;
                            HighlightEntity(numIndex);
                            break;

                        case KeyCode.RightArrow:
                            UnHighlightEntity(numIndex);
                            numIndex = (numIndex + 1) % numObjects;
                            HighlightEntity(numIndex);
                            break;

                        case KeyCode.Return:
                            input = entities[numIndex];
                            UnHighlightEntity(numIndex);
                            numIndex = 0;
                            break;
                    }
                }
                prevCode = key;
			} else if(selectionType == SelectionType.SelectCard && currentPlayer.HasCards()){
                Debug.Log("The Highlighted Card is highlighted: " + currentPlayer.CardIsHighlighted(numIndex));
				Debug.Log("Selected Card: " + numIndex);

				numObjects = currentPlayer.GetHandSize();
				Debug.Log("Number of Cards: " + currentPlayer.GetHandSize());
                if (numCardsHighlighted == 0 && currentPlayer.HasCards()) {
                    currentPlayer.HighlightCard(0);
                    numCardsHighlighted++;
                    numIndex = 0;
                }

                KeyCode key = GetKey();
                if (prevCode == unusedKey) {
                    switch (key) {
                        case KeyCode.LeftArrow:
                            currentPlayer.UnHighlightCard(numIndex);
                            numIndex = (numIndex - 1 + numObjects) % numObjects;
                            currentPlayer.HighlightCardSelect(numIndex);
                            break;

                        case KeyCode.RightArrow:
                            currentPlayer.UnHighlightCard(numIndex);
                            numIndex = (numIndex + 1) % numObjects;
                            currentPlayer.HighlightCardSelect(numIndex);
                            break;

                        case KeyCode.Return:
                            input = currentPlayer.GetCard(numIndex);
                            break;
                    }
                }
                prevCode = key;
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

    private void HighlightEntity(int n) {
        entities[n].GetComponent<Entity>().Highlight();
    }

    private void UnHighlightEntity(int n) {
        entities[n].GetComponent<Entity>().UnHighlight();
    }

	private bool NoEntitiesHighlighted(){
		for(int i = 0; i < entities.Count; i++){
			if(entities[i].GetComponent<Entity>().IsHighlighted()) return false;
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

    public static GameController GetGameController() {
        return GameObject.FindWithTag("MainCamera").GetComponent<GameController>();
    }

    public static KeyCode GetKey() {
        if (Input.GetKey(KeyCode.Return)) return KeyCode.Return;
        if (Input.GetKey(KeyCode.LeftArrow)) return KeyCode.LeftArrow;
        if (Input.GetKey(KeyCode.RightArrow)) return KeyCode.RightArrow;
        return unusedKey;
    }
}
