using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setControlsTo : MonoBehaviour {
    bool canSee;
    public bool onSee;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P) && onSee == false){
            canSee = !canSee;
        }
        if (canSee == true){
            GetComponent<Animator>().Play("demointro");
            canSee = false;
        }
        if (onSee == true){
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetComponent<Animator>().Play("demointroend");
            }

        }
	}
    public void ActiveOnSee(){
        onSee = true;
    }
    public void DeactiveOnSee()
    {
        onSee = false;
    }
}
