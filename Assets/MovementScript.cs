using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    public List<GameObject> playerOrbs = new List<GameObject>();
    public List<GameObject> playerOrbits = new List<GameObject>();
    public GameObject camCube;
    [SerializeField] public float speed = 0;
    public bool willSlowDown = false;
    public float zScale = 0;
    public float zOffset = 0;
    public bool returnIn = false;
    public float scrollSpeedX = 0;
    public float scrollSpeedY = 0;

    public string dir = "up"; 
    public int myNum = 0;
    public Material matPressed;
    public bool thrustReverse = false;
    public Material normMat;
    public bool letGo = false;
    public List<GameObject> pressedObjects = new List<GameObject>();
    public GameObject grid;
    public List<float> playerOrbitSpeeds = new List<float>();
    public Transform target;
   // public List<bool> pressed = new List<bool>();
    // Use this for initialization
    void Start () {
        zScale = transform.localScale.z;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            print("WWW");
            scrollSpeedY = -1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            scrollSpeedY = 1;

        }
        if (Input.GetKey(KeyCode.A))
        {

            scrollSpeedX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {

            scrollSpeedX = 1;
        }
        for (int i = 0; i < playerOrbits.Count; i++)
        {
            GameObject thisOrbit = playerOrbits[i];
            //playerOrbits[i].GetComponent<rotateOrbit>().rotateSpeed = playerOrbitSpeeds[i];
            for (int x = 0; x < thisOrbit.transform.childCount; x++)
            {


                GameObject currOrb = thisOrbit.transform.GetChild(x).gameObject;
                if (currOrb.tag != "orbit")
                {
                    string charWhich = currOrb.name;
                    currOrb.GetComponent<MeshRenderer>().material = normMat;
                    //currOrb.GetChild(0).gameObject.setActive(false);
                    if (!Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich)))
                    {
                    currOrb.GetComponent<myRocket>().myrocke1.SetActive(false);
                        currOrb.GetComponent<myRocket>().myrocke2.SetActive(false);

                    }

                    if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich)))
                    {
  
                        currOrb.GetComponent<MeshRenderer>().material = matPressed;
                        //currOrb.GetChild(0).gameObject.setActive(true);
                        currOrb.GetComponent<myRocket>().myrocke1.SetActive(true);
                        currOrb.GetComponent<myRocket>().myrocke2.SetActive(true);

                        //camCube.GetComponent<followPosition>().leader = currOrb.GetComponent<myRocket>().myCyli.gameObject;
                        if (!pressedObjects.Contains(currOrb) && currOrb.activeSelf)
                        {
                            pressedObjects.Add(currOrb);
                        }
                        for (int u = 0; u < pressedObjects.Count; u++)
                        {
                            if (thrustReverse == true)
                            {
                                transform.GetComponent<Rigidbody>().AddForce(-pressedObjects[u].transform.up * speed);

                            }
                            if (thrustReverse == false)
                            {
                                transform.GetComponent<Rigidbody>().AddForce(pressedObjects[u].transform.up * speed);
                            }
                        }
                    }
                }
                //  if (Input.GetKey(KeyCode.Space))
                //   {
                for (int u = 0; u < pressedObjects.Count; u++)
                {
                    if (thrustReverse == true)
                    {
                        transform.GetComponent<Rigidbody>().AddForce(-pressedObjects[u].transform.up * speed);

                    }
                    if (thrustReverse == false)
                    {
                        transform.GetComponent<Rigidbody>().AddForce(pressedObjects[u].transform.up * speed);
                    }

                    //     }
                }
                pressedObjects.Clear();


            }
        }
        float offset = Time.time * scrollSpeedX;
        float offset2 = Time.time * scrollSpeedY;

        grid.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(offset2, offset));

        scrollSpeedY = 0;
        scrollSpeedX = 0;


        /*

        for (int i = 0; i < playerOrbs.Count; i++)
        {
            if (playerOrbs[i].gameObject.activeSelf == true)
            {
                string charWhich = playerCubes[i].name;
                //print(charWhich);
                //pressed[i] = false;
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich)))
                {
                    //print("SSSSS");
                    //scaleInto(i);
                    myNum = i;
                    //pressed[myNum] = true;
                   // pressed[i] = true;


                    if (playerOrbs[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed > .1f && playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed > 0f)
                    {
                        //print("AAAAAA");
                        playerOrbs[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed -= .1f;
                    }



                    if (playerOrbs[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed < -.1f && playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed < 0f)
                    {
                       // print("LLLLLL");
                        playerOrbs[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed += .1f;
                        print(playerOrbs[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed);
                    }


                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //print("EEE");


                        playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = normMat;




                    }
                }




            }

                print("OK");

            if (pressed[i] == false && playerCubes[myNum].transform.parent != playerCubes[i].transform.parent)
                {
                
                    if (playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed < 1f && playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed > 0f)
                    {
                        playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed += .1f;
                    }
                    if (playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed > -1f && playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed < 0f)
                    {
                        playerCubes[myNum].transform.parent.GetComponent<rotateOrbit>().rotateSpeed -= .1f;
                    }
                }
                if (myNum != -1)
                {
                    playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = normMat;

                    if (pressed[myNum] == true && letGo == false)
                    {
                        print("WWWWW");
                        playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = matPressed;
                    }

                    //playerCubes[myNum].transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + zOffset);

                }

            pressed[myNum] = false;

        }
        willSlowDown = false;
*/
    }
    void SendForceOut(int number){
        
       // print((-playerCubes[number].transform.forward * (speed * ((-1 * (zOffset * 2))))));
        transform.GetComponent<Rigidbody>().AddForce(-playerOrbs[number].transform.forward*(speed*((-2*(zOffset*2)))));
        zOffset = 0;


    }
   
}

