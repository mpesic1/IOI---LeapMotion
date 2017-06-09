
using UnityEngine;


namespace Leap.Unity {

public class TargetLocationScript : MonoBehaviour {

    public GameObject correctPiece;
    float minDist = 0.03f;
    //public Behaviour halo;
    private LeapRTS leapRTSScript;
    public Material highlight;
    public Material silhouette;
    public Material cannonMaterial;
    public GameObject target;
    private GameStateScript gameStateScript;
    bool isPlaced = false;


	// Use this for initialization
	void Start () {
		//halo = (Behaviour)GetComponent("Halo");
        leapRTSScript = correctPiece.GetComponent<LeapRTS>();
        gameStateScript = GameObject.Find("GameState").GetComponent<GameStateScript>();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (target.transform.position, correctPiece.transform.position);

        // highlight the correct location
        if(!isPlaced) {
            if(leapRTSScript.isPickedUp) {
                //halo.enabled = true;
                GetComponent<Renderer>().material = highlight;
            }
            else GetComponent<Renderer>().material = silhouette; //halo.enabled = false;
        }
        // put the object in place
        if(distance <= minDist) {

            // update gameStateScript
            if(!isPlaced) {
                correctPiece.SetActive(false);
                GetComponent<Renderer>().material = cannonMaterial;
                gameStateScript.numPlacedObjects--;
                GameObject.Find("GameState").GetComponent<AudioSource>().Play();
                isPlaced = true;
            }
        }
	}
}

}
