using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicktomoveCamforNoPlanetScenes : MonoBehaviour {
	public bool forward = false;
    public int backVal = -170;
    public int forwardVal = -90;
    public float incre = 0;
    public float custX = 0;
    public bool stuck = false;
    private float custX1 = 0;
    private float custY1 = 0;
    public GameObject multipurposeCam;
    public float custY = 0;

    public bool finishedAnimation = true;
	// Use this for initialization
	void Start () {
        incre = backVal;
        custX1 = custX;
        custY1 = custY;
        AllowFullCam(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (stuck == false)
        {

            incre += Input.GetAxis("Mouse ScrollWheel");
            incre = Mathf.Clamp(incre, backVal, forwardVal);

            if (Input.GetMouseButtonDown(0))
            {
                forward = !forward;
                finishedAnimation = false;
            }
            if (forward == true && finishedAnimation == false)
            {
                moveForward();
                //finishedAnimation = false;
            }
            if (forward == false && finishedAnimation == false)
            {
                moveBack();

            }
            transform.localPosition = new Vector3(custX1, custY1, incre);
        }
        if (stuck == true){
            transform.localPosition = new Vector3(custX1, custY1, incre);
        }
	}

    public void moveForward()
    {
        if (incre < forwardVal){
            incre += 5;
            if(custX1 > 0 ){
                custX1 -= 2;
            }
            if (custY1 < 0)
            {
                custY1 += 2;
            }
        }
        if (incre > forwardVal)
        {
            incre -= 5;
            if (custX1 > 0)
            {
                custX1 -= 2;
            }
            if (custY1 < 0)
            {
                custY1 += 2;
            }
        }
        if (incre == forwardVal){
            finishedAnimation = true;
        }
        //transform.possdition = new Vector3(0, 0, incre);
    }
    public void moveBack()
    {
        if (incre < backVal)
        {
            incre += 5;
            if (custX1 > 0)
            {
                custX1 += 2;
            }
            if (custY1 < 0)
            {
                custY1 -= 2;
            }
        }
        if (incre > backVal)
        {
            incre -= 5;
            if (custX1 < custX){
                custX1 += 2;
            }
            if (custY1 > custY)
            {
                custY1 -= 2;
            }
        }
        if (incre == backVal)
        {
            finishedAnimation = true;
        }


    }

    public void AllowFullCam(bool hasAccess){
        if (hasAccess){
            multipurposeCam.GetComponent<freelookcam1>().m_TiltMin = -90;
            multipurposeCam.GetComponent<freelookcam1>().m_TiltMax = 90;

        }
        if (!hasAccess)
        {
            multipurposeCam.GetComponent<freelookcam1>().m_TiltMin = -90;
            multipurposeCam.GetComponent<freelookcam1>().m_TiltMax = 0;

        }
    }


}
