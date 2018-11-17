using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractorWorking : MonoBehaviour {
    public GameObject playerPlanet;
    public float grav = 9;
    public bool playerGravOn = false;
    public float backUpGrav;
    public GameObject cameraCube;
    public GameObject grava;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        grav = playerPlanet.GetComponent<gravPlanetInfo>().gravHere;
        Vector3 downVec = playerPlanet.transform.position - transform.position;

        GetComponent<Rigidbody>().AddForce(downVec / grav);
	}
}
