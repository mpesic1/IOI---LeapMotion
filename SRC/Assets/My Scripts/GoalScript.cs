using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour {

    GameObject gameState;
    GameStateScript stateScript;
    public int timeToChange;
    bool isAchieved = false;
    public Sprite filledSprite;

	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameState");
	    stateScript = gameState.GetComponent<GameStateScript>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToChange == stateScript.numPlacedObjects && !isAchieved) {
            isAchieved = true;
            GetComponent<Image>().sprite = filledSprite;
        }
	}
}
