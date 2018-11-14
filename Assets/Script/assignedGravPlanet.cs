using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assignedGravPlanet : MonoBehaviour {
    public GameObject planet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerCube" || other.tag == "playerSphere"){
            GameObject.Find("EventManager").GetComponent<GravManager>().playerPlanet = planet;
        }
    }
}
