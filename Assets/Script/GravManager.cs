using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GravManager : MonoBehaviour
    {
    public GameObject playerPlanet;
        public float grav = 9;
    public bool playerGravOn = false;
        public float backUpGrav;
    public GameObject cameraCube;
    public List<GameObject> hasGravity = new List<GameObject>();

    public GameObject grava;
    //public bool gravOn = false;
        // Use this for initialization
        void Start()
        {
            backUpGrav = grav;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("hasGrav")){
            hasGravity.Add(obj.gameObject);
        }

        }

    // Update is called once per frame
    void FixedUpdate()
    {



        // GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();





        print("OK");





        if (playerGravOn == true)
        {


            for (int i = 0; i < hasGravity.Count; i++)
            {

               
                    //grav = 1;
                Vector3 downVec = playerPlanet.transform.position - hasGravity[i].transform.position;
                Vector3 bodyUp = hasGravity[i].transform.up;


                Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, downVec / grav) * hasGravity[i].transform.rotation;
                hasGravity[i].transform.rotation = Quaternion.Slerp(hasGravity[i].transform.rotation, targetRotation, 2 * Time.deltaTime);


                if (hasGravity[i].name != "CameraCube")
                {

                    hasGravity[i].GetComponent<Rigidbody>().AddForce(downVec / grav);
                }
            }
        }

                


            }
        }
    


   
