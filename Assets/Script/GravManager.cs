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
    public GameObject grava;
    //public bool gravOn = false;
        // Use this for initialization
        void Start()
        {
            backUpGrav = grav;

        }

    // Update is called once per frame
    void FixedUpdate()
    {



        // GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();


                


                



                if (playerGravOn == true)
                    {

                        grav = playerPlanet.GetComponent<gravPlanetInfo>().gravHere;
                Vector3 downVec = playerPlanet.transform.position - cameraCube.transform.position;
                Vector3 bodyUp = cameraCube.transform.up;


                Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, downVec / grav) * cameraCube.transform.rotation;
                cameraCube.transform.rotation = Quaternion.Slerp(cameraCube.transform.rotation, targetRotation, 2 * Time.deltaTime);

                    }

                


            }
        }
    


   
