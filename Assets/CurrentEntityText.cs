using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentEntityText : MonoBehaviour {
	public Text text;
	Entity currentEntity;
	Player currentPlayer;
	Enemy currentEnemy;
	string previousText;
	string s;

	// Use this for initialization
	void Start () {
		previousText = "";
		s = "";
		currentEntity = null;
		currentPlayer = null;
		currentEnemy = null;

		text.text = "hello fame";
		
	}
	
	// Update is called once per frame
	void Update () {
		currentEntity = GameObject.FindWithTag("CurrentEntity").GetComponent<Entity>();
		if(currentEntity != null && currentEntity is Player) {
			currentPlayer = (Player) currentEntity;
			s = "Player Information: \n";
			s += "HP: " + currentPlayer.hp + "/" + currentPlayer.maxHP +"\n";
			s += "Energy: " + currentPlayer.currentEnergy + "/" + currentPlayer.energyPerTurn;
			if(text.text != s) text.text = s;
		} else if(currentEntity != null && currentEntity is Enemy) {
			currentEnemy = (Enemy) currentEntity;
			s = "Enemy Information: \n";
			s += "HP: " + currentEnemy.hp + "/" + currentEnemy.maxHP +"\n";
			if(text.text != s) text.text = s;
		} else {
			text.text = "";
		}
	}
}
