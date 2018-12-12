using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideChangeState : MonoBehaviour {
    public GameObject intro;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            intro.GetComponent<introManager>().switchSubScene(2);
        }
    }
}
