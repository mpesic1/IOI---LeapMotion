using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementBoundaries : MonoBehaviour {

    //keeps picked up objects in range of the sensor and the camera

    float xLeftBound = -4;
    float xRightBound = 0;
    float yLowBound = 3.1f;
    float zFrontBound = -2.4f;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x > xRightBound)
            transform.position.Set(xRightBound, transform.position.y, transform.position.z);
        if(transform.position.x < xLeftBound)
            transform.position.Set(xLeftBound, transform.position.y, transform.position.z);
        if(transform.position.y < yLowBound) {
            print("too low");
            transform.SetParent(null, true);
            transform.position.Set(transform.position.x, yLowBound+1, transform.position.z);
        }
        if(transform.position.z < zFrontBound)
            transform.position.Set(transform.position.x, transform.position.y, zFrontBound);
	}
}
