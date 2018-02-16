using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

	public enum battleState {beginFight, beginTurn, chooseCard, chooseTarget, endTurn, endFight};
	battleState curState;
	IEntity turn;
	Card activatedCard;

	GameState state;

	int t = 0;

	// Use this for initialization
	void Start () {
		    Player p1 = new Wizard();
            Player p2 = new Wizard();

            IEntity enemy1 = new Pawn();

            state = new GameState();
            state.AddEntity(p1);
            state.AddEntity(p2);
            state.AddEntity(enemy1);

            state.BeginFight();
			turn = state.getEntity(0);
			state.setCurrentEntity(turn);
			curState = battleState.beginTurn;
	}
	
	// Update is called once per frame
	void Update () {
		if(curState == battleState.beginFight){
			state.BeginFight();
			turn = state.getEntity(t);
			state.setCurrentEntity(turn);

			turn.evaluateBegin();
			turn.DrawHand();
		}
		else if(curState == battleState.beginTurn){
			turn = state.getEntity(t);
			turn.DrawHand();
			turn.displayHand();
			curState = battleState.chooseCard;
			Debug.Log("Turn Begins");
		}
		else if(curState == battleState.chooseCard){
			//if a card a picked, do use effect (discard, trash, ect.)
			//activate the card / pick its target
			//then stay in the same state
			if(turn is Player){
				
				if(Input.GetKeyDown(KeyCode.Alpha1)) {
					turn.loseEnergy(turn.getCard(0).GetCost());
					turn.getCard(0).Run(state);
					Debug.Log("You now have " + turn.getEnergy() + " energy!");
				}
				if(Input.GetKeyDown(KeyCode.Alpha2)) turn.getCard(1).Run(state);
				if(Input.GetKeyDown(KeyCode.Alpha3)) turn.getCard(2).Run(state);
				if(Input.GetKeyDown(KeyCode.Alpha4)) turn.getCard(3).Run(state);
				if(Input.GetKeyDown(KeyCode.Alpha5)) turn.getCard(4).Run(state);

				if(turn.getEnergy() <= 0) curState = battleState.endTurn;
			}

			else if(turn is Enemy){

			}

			//if they hit the end turn button, go to end turn state
		}
		else if(curState == battleState.endTurn){
			turn.DiscardHand();
			Debug.Log("Your turn is done!");
			t++;
			curState = battleState.beginTurn;
		}
	}
}
