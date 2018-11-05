using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    public List<GameObject> playerCubes = new List<GameObject>();
    [SerializeField] public float speed = 0;

    public float zScale = 0;
    public float zOffset = 0;
    public bool returnIn = false;
    public int myNum = 0;
    public Material matPressed;
    public Material normMat;
    public bool letGo = false;
    public Transform target;
    public bool pressed = false;
	// Use this for initialization
	void Start () {
        zScale = transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
        if (myNum != -1 )
        {
            playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = normMat;

            if (pressed == true && letGo == false)
            {
                print("WWWWW");
                scaleInto(myNum);
                playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = matPressed;
            }

            playerCubes[myNum].transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + zOffset);

        }
        for (int i = 0; i < playerCubes.Count; i++)
        {
            if (playerCubes[i].gameObject.activeSelf == true)
            {
                string charWhich = playerCubes[i].name;
                //print(charWhich);
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich))  && letGo == false)
                {
                    //scaleInto(i);
                    myNum = i;
                    pressed = true;

                }

                if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich)) && letGo == false)
                {
                    //print("EEE");
                    pressed = false;

                    SendForceOut(i);
                    returnIn = true;
                    playerCubes[myNum].transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + zOffset);
                    playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = normMat;

                    myNum = -1;

                }
                if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), charWhich)) && letGo == true)
                {
                    //print("EEE");
                    zOffset = 0;
                    letGo = false;

                }
            }

        }


        if (GetComponent<LineRenderer>().enabled == true){
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, target.position);

        }
        if (Input.GetKey(KeyCode.Space)){
            transform.GetComponent<Rigidbody>().angularDrag = 8;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().angularDrag = 0;
        }



	}
    void scaleInto(int i){
        if (zOffset >-2){
            print("OO");
            zOffset -= .02f;
        }
        if (zOffset <= -2){
            pressed = false;
            returnIn = true;

            pressed = false;
            letGo = true;
            playerCubes[myNum].transform.gameObject.GetComponent<Renderer>().material = normMat;
        }


    }
    void SendForceOut(int number){
        
        print((-playerCubes[number].transform.forward * (speed * ((-1 * (zOffset * 2))))));
        transform.GetComponent<Rigidbody>().AddForce(-playerCubes[number].transform.forward*(speed*((-2*(zOffset*2)))));
        zOffset = 0;


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "engineTurner"){
            bool a = true;
            for (int i = 0; i < playerCubes.Count; i++)
            {
                if (a == true)
                {
                    if (playerCubes[i].activeSelf == false)
                    {
                        playerCubes[i].SetActive(true);
                        a = false;
                    }
                }
            }
            Destroy(other.gameObject);
        }
    }


    public void lineGenerator(Transform other){
        GetComponent<LineRenderer>().enabled = true;
        target = other; 
    }
    public void CheckIfVis(){
        if (GetComponent<Renderer>().isVisible) //Check if Camera is turned towards the GameObject first
        {
            RaycastHit hit;
            // Calculate Ray direction
            Vector3 direction = Camera.main.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.tag != "MainCamera") //hit something else before the camera
                {
                    //do something here
                }
            }
        }
    }
}
