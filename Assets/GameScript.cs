using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    public enum BattleState {
        beginFight,
        beginTurn,
        chooseCard,
        chooseTarget,
        endTurn,
        endFight
    };
    BattleState curState;
    Entity turn;
    Card activatedCard;

    GameState state;

    // Use this for initialization
    void Start() {

        state = new GameState();

        Player p1 = new Wizard();
        Player p2 = new Wizard();
        Entity enemy1 = new Pawn();
        
        state.AddEntity(p1);
        state.AddEntity(p2);
        state.AddEntity(enemy1);

        state.BeginFight();
        turn = state.GetCurrentEntity();
        curState = BattleState.beginTurn;
    }

    // Update is called once per frame
    void Update() {

        if (curState == BattleState.beginFight) {
            state.BeginFight();
        } else if (curState == BattleState.beginTurn) {
            state.BeginTurn();
            turn.DisplayHand();
            curState = BattleState.chooseCard;
            GameState.UnityOutput("Turn Begins");
        } else if (curState == BattleState.chooseCard) {
            // if a card a picked, do use effect (discard, trash, ect.)
            // activate the card / pick its target
            // then stay in the same state
            if (turn is Player) {

                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    turn.LoseEnergy(turn.GetCard(0).GetCost());
                    turn.GetCard(0).Run(state);
                    GameState.UnityOutput("You now have " + turn.GetEnergy() + " energy!");
                }
                if (Input.GetKeyDown(KeyCode.Alpha2)) turn.GetCard(1).Run(state);
                if (Input.GetKeyDown(KeyCode.Alpha3)) turn.GetCard(2).Run(state);
                if (Input.GetKeyDown(KeyCode.Alpha4)) turn.GetCard(3).Run(state);
                if (Input.GetKeyDown(KeyCode.Alpha5)) turn.GetCard(4).Run(state);

                if (turn.GetEnergy() <= 0) curState = BattleState.endTurn;

            } else if (turn is Enemy) {

                turn.TakeTurn();
                curState = BattleState.endTurn;

            }

            // If they hit the end turn button, go to end turn state
        } else if (curState == BattleState.endTurn) {
            turn.EndTurn();
            GameState.UnityOutput("Your turn is done!");
            turn = state.NextTurn();
            curState = BattleState.beginTurn;
        }
    }
}
