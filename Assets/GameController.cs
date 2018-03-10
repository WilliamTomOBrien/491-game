using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    Entity currentEntity;

	void Awake () {

	}

	// Use this for initialization
	void Start () {
		GameObject g = Instantiate(Resources.Load("Player"), new Vector2(7, 3), Quaternion.identity) as GameObject;
		currentEntity = g.GetComponent<Player>();

		currentEntity.BeginTurn();
	}

	// Update is called once per frame
	void Update () {

	}

}
