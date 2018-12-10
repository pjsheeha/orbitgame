using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assignPlanetBasedOnCollision : MonoBehaviour {
    public GameObject camToSwitch;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
private void OnTriggerEnter(Collider other)
    {
        GetComponent<GravManager>().playerGravOn = true;
        //camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().forward = true;
        //camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().finishedAnimation = false;

        camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().AllowFullCam(false);
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<GravManager>().playerGravOn = false;
        //camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().forward = false;
        //camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().finishedAnimation = false;


        camToSwitch.GetComponent<clicktomoveCamforNoPlanetScenes>().AllowFullCam(true);

    }
}
