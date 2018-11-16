using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidePlanet : MonoBehaviour {

	//public GameObject currentCenter;
    //public float yScale = .1f;
    //public int countInt = 0;
    public bool gravOn = true;
    public GameObject planet;
   // public bool countBool = true;
    public bool attachedToPlayer = false;
    //public float mag = 0f;
   // public bool grav;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        /*
            if (countBool == false)
            {
                countInt -= 1;
                if (yScale > .07 / mag)
                {
                    yScale -= .003f;

                }
            }
            if (countBool == true)
            {

                if (yScale < .1f)
                {
                    yScale += .003f;

                }
            }
            if (countInt <= 0)
            {
                countBool = true;

            }
            */

            //GameObject.Find("CameraCube").transform.localScale = new Vector3(GameObject.Find("CameraCube").transform.localScale.x, yScale, GameObject.Find("CameraCube").transform.localScale.z);
            //GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();


            //transform.localScale = new Vector3(GameObject.Find("CameraCube").transform.localScale.x*10,GameObject.Find("CameraCube").transform.localScale.y*10,GameObject.Find("CameraCube").transform.localScale.z*10);
        }

	




private void OnTriggerEnter(Collider other)
{
        if (attachedToPlayer == true)
        {
            if (GameObject.Find("EventManager").GetComponent<GravManager>().playerGravOn == true)
            {





                if (other.transform.name == "Bubble")
                {
                    GameObject.Find("EventManager").GetComponent<GravManager>().playerPlanet = other.GetComponent<assignedGravPlanet>().planet;
                }
            }
        }
        else
        {
            if (gravOn == true)
            {
                if (other.transform.name == "Bubble")
                {
                    planet = other.GetComponent<assignedGravPlanet>().planet;
                }
            }
        }
    
}

    /*
    void OnCollisionEnter(Collision collide)
    {

        //if (collide.transform.name.Substring(0, 6) == "Planet" )
        // {
        //GameObject recentImpact = Instantiate(Resources.Load("impact") as GameObject, transform.position, GameObject.Find("CameraCube").transform.rotation);
        //recentImpact.transform.localScale = new Vector3(collide.relativeVelocity.magnitude / 5, collide.relativeVelocity.magnitude / 5, collide.relativeVelocity.magnitude / 5);

        // }

        if (GameObject.Find("EventManager").GetComponent<states>().state > 0)
        {
            if (name == "gravOwner")
            {
                countBool = false;

                // print("WEW");
                countInt = 10;
            }
            mag = collide.relativeVelocity.magnitude / 10;

            if (collide.transform.name.Length > 13 && transform.name.Length > 4)
            {
                if (collide.transform.name.Substring(0, 13) == "wallConnector" && transform.name.Substring(0, 4) == "grav"){
                    GameObject.Find("EventManager").GetComponent<GravManager>().planet = collide.transform.GetComponent<connectedForce>().planet;
                }

            }

            if (collide.transform.name.Length > 6 && transform.name.Length > 4){
                if (collide.transform.name.Substring(0, 6) == "Planet" && transform.name.Substring(0, 4) == "grav")
                {
                    GameObject.Find("EventManager").GetComponent<GravManager>().planet = GameObject.Find(collide.transform.name);

                }
            }

        }
    }
    */
}