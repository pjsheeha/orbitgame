using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOrbit : MonoBehaviour {
    public float rotateSpeed = .1f;
    public string directionOfRot = "z";
    //float offset = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //offset += rotateSpeedZ;
        if (directionOfRot == "z")
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotateSpeed);
        }
        if (directionOfRot == "y")
        {
            transform.Rotate(Vector3.right * rotateSpeed);
        }
	}
}
