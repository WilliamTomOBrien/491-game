using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    List<GameObject> Hand;

	void Awake () {
		Hand = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
      for(int i = 0; i < 5; i++) {
          Hand.Add(Instantiate(Resources.Load("Card"), new Vector2(i*2, 0), Quaternion.identity) as GameObject);
		  Card c = Hand[i].GetComponent<Card>();
		  Debug.Log("Made: " + c);
      }
	}

	// Update is called once per frame
	void Update () {

	}

}
