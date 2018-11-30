using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPosition : MonoBehaviour {
    public GameObject leader;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = leader.transform.position;
        transform.rotation = leader.transform.rotation;

	}
}
