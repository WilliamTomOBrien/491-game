using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

public Entity currentEntity;
public Player currentPlayer;
public Enemy currentEnemy;

public List<GameObject> entities;
public GameObject input;

public CardPile cardPile;

//used for health and energy graphics
public Slider p1HealthBar;
public Slider p2HealthBar;
public Slider e1HealthBar;
public Slider e2HealthBar;
public List<Slider> healthBars;

public Text p1HealthText;
public Text p2HealthText;
public Text e1HealthText;
public Text e2HealthText;
public List<Text> healthTexts;

public GameObject energy;
public List<GameObject> p1Energy;
public List<GameObject> p2Energy;

public bool canCheckForEndOfTurn = true;
private int numCardsHighlighted = 0;

int entityIndex = 0;

private System.Random rand;

int numObjects;
int numIndex;

private const KeyCode unusedKey = KeyCode.A;
public KeyCode prevCode = unusedKey;
List<CardStateBuilder> AllCardStates;

public enum SelectionType {
        SelectCardToPlay,
        SelectCard,
        CardPileSelect,
        SelectEntity
};

public enum GameType {
    Battle,
    BattleSetup,
    PickACard
};

public GameType gameType;

public SelectionType selectionType;

void Awake () {
        AllCardStates = new List<CardStateBuilder>();
        rand = new System.Random();

        selectionType = SelectionType.SelectCardToPlay;
        entities = new List<GameObject> {
                Instantiate(Resources.Load("Player"), new Vector2(-3, 0), Quaternion.identity) as GameObject
        };
        entities[0].tag = "CurrentEntity";

        entities.Add(Instantiate(Resources.Load("Player"), new Vector2(-5, 0), Quaternion.identity) as GameObject);
        entities.Add(Instantiate(Resources.Load("EnemyMagician"), new Vector2(5, 0), Quaternion.identity) as GameObject);
        entities.Add(Instantiate(Resources.Load("EnemyMagician"), new Vector2(3, 0), Quaternion.identity) as GameObject);

        gameType = GameType.Battle;
}

// Use this for initialization
void Start () {
        healthBars = new List<Slider> {
                p1HealthBar,
                p2HealthBar,
                e1HealthBar,
                e2HealthBar
        };

        healthTexts = new List<Text> {
                p1HealthText,
                p2HealthText,
                e1HealthText,
                e2HealthText
        };

        Task ex = new Damage(10);
        List<Task> li = new List<Task> {
            ex
        };

        CardState baseCard = new CardState("example", 1, li, "Sounds/PunchSound");

        for(int i = 0; i < 6; i++){
          List<float> means = new List<float>();
          means.Add(9f + i/6f);
          means.Add(10f + i/6f);
          means.Add(11f + i/6f);
          List<float> stdDev = new List<float>();
          stdDev.Add(2.5f + i/20f);
          stdDev.Add(3.75f + i/20f);
          stdDev.Add(1f + i/20f);
          TaskBuilder strike = new TaskBuilder(new Damage(0), means, stdDev);
          List<TaskBuilder> tasks = new List<TaskBuilder>();
          tasks.Add(strike);
          CardStateBuilder stateBuilder = new CardStateBuilder(new StrikeState(), tasks);
          CardPile.AddToAll(stateBuilder);
          AllCardStates.Add(stateBuilder);
        }



        entityIndex = 0;
        currentEntity = entities[entityIndex].GetComponent<Entity>();
        currentEntity.BeginTurn();
}

// Update is called once per frame
void Update () {
        //update every entity's health bar
        if(gameType == GameType.Battle) {

                int numPlayers = 0;
                int numEnemies = 0;
                int i = 0;
                while(i < entities.Count) {
                        bool removedFlag = false;
                        string message;
                        Entity e = entities[i].GetComponent<Entity>();
                        if (e != null) {
                                if (e is Player) {
                                        numPlayers++;
                                        message = "Player " + numPlayers + ": ";
                                        if(e.GetHP() <= 0){
                                          Debug.Log("player died");

                                          //You lost, we should figure out what to do here
                                        }
                                } else {
                                        numEnemies++;
                                        Debug.Log("enemy exists " + numEnemies);
                                        message = "Enemy " + numEnemies + ": ";
                                        if(e.GetHP() <= 0){
                                          Debug.Log("enemy died");
                                          removedFlag = true;
                                          Destroy(entities[i]);
                                          entities.Remove(entities[i]);
                                        }
                                }
                                message += e.GetHP() + "/" + e.GetMaxHP();
                                healthTexts[i].text = message;
                                healthBars[i].value = (float)e.GetHP() / e.GetMaxHP();
                        }

                        if(!removedFlag) i++;

                }

                if(numEnemies == 0){
                  gameType = GameType.PickACard;
                  cardPile = CardPile.MakeCardPile(3);
                }


                DeleteAllEnergy();

                if (currentEntity is Player) {
                        currentPlayer = (Player) currentEntity;
                        currentPlayer.OrganizeCards();

                        //Used to check for p1 and p2, then creates energy for the gui
                        if(currentEntity.Equals(entities[0].GetComponent<Entity>()))
                        {
                                //is p1
                                DeleteEnergy1();
                                CreateEnergy1();
                        } else
                        {
                                //is p2
                                DeleteEnergy2();
                                CreateEnergy2();
                        }

                        if(selectionType == SelectionType.SelectCardToPlay && currentPlayer.HasCards()) {


                                numObjects = currentPlayer.GetHandSize();
                                if (numCardsHighlighted == 0 && currentPlayer.HasCards()) {
                                        currentPlayer.HighlightCard(0);
                                        numCardsHighlighted++;
                                        numIndex = 0;
                                }

                                if(currentPlayer.currentEnergy <= 0 || !currentPlayer.HasCards()) {
                                        if(canCheckForEndOfTurn) {
                                                currentPlayer.EndTurn();

                                                entityIndex = (entityIndex + 1) % entities.Count;
                                                currentEntity = entities[entityIndex].GetComponent<Entity>();
                                                currentEntity.BeginTurn();

                                        }
                                }



                                KeyCode key = GetKey();
                                if (prevCode == unusedKey) {
                                        switch (key) {
                                        case KeyCode.LeftArrow:
                                                if(numObjects != numIndex) currentPlayer.UnHighlightCard(numIndex);
                                                numIndex = (numIndex - 1 + numObjects) % (numObjects + 1);
                                                currentPlayer.HighlightCard(numIndex);
                                                break;

                                        case KeyCode.RightArrow:
                                                currentPlayer.UnHighlightCard(numIndex);
                                                numIndex = (numIndex + 1) % (numObjects + 1);
                                                if(numObjects != numIndex) currentPlayer.HighlightCard(numIndex);
                                                break;

                                        case KeyCode.Return:
                                                if(numObjects != numIndex) currentPlayer.ActivateCard(numIndex);
                                                else{
                                                        //if(canCheckForEndOfTurn) {
                                                        currentPlayer.EndTurn();

                                                        entityIndex = (entityIndex + 1) % entities.Count;
                                                        currentEntity = entities[entityIndex].GetComponent<Entity>();
                                                        currentEntity.BeginTurn();
                                                        //}

                                                }
                                                numCardsHighlighted--;
                                                break;
                                        }
                                }
                                prevCode = key;


                        } else if(selectionType == SelectionType.SelectEntity) {
                                numObjects = entities.Count;

                                if (NoEntitiesHighlighted()) {
                                        HighlightEntity(0);
                                        numIndex = 0;
                                }

                                KeyCode key = GetKey();
                                if (prevCode == unusedKey) {
                                        switch (key) {
                                        case KeyCode.LeftArrow:
                                                UnHighlightEntity(numIndex);
                                                numIndex = (numIndex - 1 + numObjects) % (numObjects);
                                                HighlightEntity(numIndex);
                                                break;

                                        case KeyCode.RightArrow:
                                                UnHighlightEntity(numIndex);
                                                numIndex = (numIndex + 1) % (numObjects);
                                                if(numIndex != numObjects) HighlightEntity(numIndex);
                                                break;

                                        case KeyCode.Return:
                                                input = entities[numIndex];
                                                UnHighlightEntity(numIndex);
                                                Entity.UnHighlightAll();
                                                numIndex = 0;
                                                break;
                                        }

                                }
                                prevCode = key;
                        } else if(selectionType == SelectionType.SelectCard && currentPlayer.HasCards()) {

                                numObjects = currentPlayer.GetHandSize();
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
                                                currentPlayer.UnSelectAll();
                                                break;
                                        }
                                }
                                prevCode = key;

                        } else if(selectionType == SelectionType.CardPileSelect) {
                                cardPile = GameObject.FindWithTag("CardPile").GetComponent<CardPile>();

                                KeyCode key = GetKey();
                                if (prevCode == unusedKey) {
                                        switch (key) {
                                        case KeyCode.LeftArrow:
                                                cardPile.Left();
                                                break;

                                        case KeyCode.RightArrow:
                                                cardPile.Right();
                                                break;

                                        case KeyCode.Return:
                                                input = cardPile.GetSelected();
                                                break;
                                        }
                                }
                                prevCode = key;

                        }
                } else if(currentEntity is Enemy) {
                        currentEnemy = (Enemy) currentEntity;
                        currentEnemy.ActivateEffect();

                        entities[entityIndex].tag = "Enemy";
                        entityIndex = (entityIndex + 1) % entities.Count;
                        currentEntity = entities[entityIndex].GetComponent<Entity>();
                        entities[entityIndex].tag = "CurrentEntity";
                        currentEntity.BeginTurn();
                }
        } else if(gameType == GameType.PickACard) {
          cardPile = GameObject.FindWithTag("CardPile").GetComponent<CardPile>();

          KeyCode key = GetKey();
          if (prevCode == unusedKey) {
                  switch (key) {
                  case KeyCode.LeftArrow:
                          cardPile.Left();
                          break;

                  case KeyCode.RightArrow:
                          cardPile.Right();
                          break;

                  case KeyCode.Return:
                          (entities[rand.Next(2)].GetComponent<Player>()).AddCardState(cardPile.GetSelected().GetComponent<Card>().GetState());
                          cardPile.DestroyAll();
                          gameType = GameType.BattleSetup;
                          break;
                  }
          }
          prevCode = key;
        } else if(gameType == GameType.BattleSetup) {
          entities.Add(Instantiate(Resources.Load("Enemy"), new Vector3(5, 0, -1), Quaternion.identity) as GameObject);
          entities.Add(Instantiate(Resources.Load("Enemy"), new Vector3(3, 0, -1), Quaternion.identity) as GameObject);

          for(int i = 2; i < entities.Count; i++){
            List<CardStateBuilder> c = new List<CardStateBuilder>();
            for(int j = 0; j < 6; j++){
              c.Add(AllCardStates[rand.Next(AllCardStates.Count)]);
            }
            EnemyBuilder e = new EnemyBuilder(1, null, c);
            e.SetEnemy(entities[i].GetComponent<Enemy>(), 0);
          }
          gameType = GameType.Battle;
            Debug.Log("In Battle Setup");
        }
}

private void HighlightEntity(int n) {
        entities[n].GetComponent<Entity>().Highlight();
}

private void UnHighlightEntity(int n) {
        entities[n].GetComponent<Entity>().UnHighlight();
}

private bool NoEntitiesHighlighted(){
        for(int i = 0; i < entities.Count; i++) {
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

//Used to create energy tokens for each player respectively
public void CreateEnergy1()
{
        for (int k = 0; k < currentPlayer.currentEnergy; k++) {
                p1Energy.Add(Instantiate(energy, new Vector2(-6.66f + (k * 0.75f), -1.86f), Quaternion.identity));
        }
}

public void CreateEnergy2()
{
        for (int k = 0; k < currentPlayer.currentEnergy; k++) {
                p2Energy.Add(Instantiate(energy, new Vector2(-6.66f + (k * 0.75f), -3.14f), Quaternion.identity));
        }
}

//remove all energy tokens for a given player
public void DeleteEnergy1()
{
        for (int k = 0; k < p1Energy.Count; k++) {
                Destroy(p1Energy[k]);
        }
        p1Energy.Clear();
}

public void DeleteEnergy2()
{
        for (int k = 0; k < p2Energy.Count; k++) {
                Destroy(p2Energy[k]);
        }
        p2Energy.Clear();
}

public void DeleteAllEnergy()
{
        for (int k = 0; k < p1Energy.Count; k++) {
                Destroy(p1Energy[k]);
        }
        p1Energy.Clear();

        for (int k = 0; k < p2Energy.Count; k++) {
                Destroy(p2Energy[k]);
        }
        p2Energy.Clear();
}

public Entity GetHighlightedEntity(){
        for(int i = 0; i < entities.Count; i++) {
                if(entities[i].GetComponent<Entity>().IsHighlighted()) return entities[i].GetComponent<Entity>();
        }
        return null;
}
}
