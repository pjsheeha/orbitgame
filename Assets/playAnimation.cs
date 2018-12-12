using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimation : MonoBehaviour {
    public GameObject intro;
	// Use this for initialization
	void Start () {
        intro.GetComponent<Animator>().Play("introcutscene2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
