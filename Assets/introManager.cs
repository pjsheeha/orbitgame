using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introManager : MonoBehaviour {
    public List<GameObject> myObjs = new List<GameObject>();
    public NPCBehavior nPCBehavior;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
        public void switchSubScene(int a )
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[a])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }

        }
    public void substatePlay(string a)
    {
        nPCBehavior.ChangeSubState(a);
    }
}
