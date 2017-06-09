using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateScript : MonoBehaviour {
    //when this is 0 the game ends. Set to number of all pieces of the cannon.
    public int numPlacedObjects = 11;
    public GameObject wholeCannon;
    public GameObject rotationCenter;

    public GameObject backFace;

    public GameObject titleText;
    public GameObject languageLabelText;
    public GameObject resetText;
    public GameObject mainCam;
    bool first = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
		if(numPlacedObjects == 0) {
            if(first) {
                first = false;
                mainCam.GetComponent<Animation>().Play();
                backFace.SetActive(true);
            }
            
            //wholeCannon.transform.Rotate(new Vector3(0, 1, 0));
            wholeCannon.transform.RotateAround(rotationCenter.transform.position, new Vector3(0, 1, 0), 1);
        } //resetScene();
	}

    public void resetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exitGame() {
        Application.Quit();
    }


    public void languageChange(int id) {
        if(id == 0) {
            //SLO
            titleText.GetComponent<Text>().text = "Sestavi top";
            languageLabelText.GetComponent<Text>().text = "Language";
            resetText.GetComponent<Text>().text = "Ponastavi";
        }
        else if(id == 1) {
            //ENG
            titleText.GetComponent<Text>().text = "Construct the cannon";
            languageLabelText.GetComponent<Text>().text = "Jezik";
            resetText.GetComponent<Text>().text = "Reset";
        }
    }
}
