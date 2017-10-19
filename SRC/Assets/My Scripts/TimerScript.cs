using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimerScript : MonoBehaviour {

    /*
     * This script measures time starting from when none of the hands are detected in the scene.
     * If there is no activity for maxSeconds seconds, the scene is reset.
     * If hand is detected, the timer is reset and stopped until no hands are detected.
     */

    public GameObject leftHand;
    public GameObject rightHand;

    //reset after this much time
    int maxSeconds = 120;

    float currentTime;
    float startTime;
    bool startTimer = false;
    public bool allowReset;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if((leftHand.activeInHierarchy || rightHand.activeInHierarchy) && allowReset) {
            startTimer = true;
            startTime = Time.time;

        }

        if(!startTimer) startTime = Time.time;

        currentTime = Time.time;
        
        if(currentTime - startTime > maxSeconds) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
