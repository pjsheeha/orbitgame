using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPosition : MonoBehaviour {
    public Vector3 leader;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = leader;
	}
}
