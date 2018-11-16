using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravManager : MonoBehaviour
{

    public GameObject playerPlanet;
    public float grav = 50;
    public bool playerGravOn = false;
    public GameObject cameraCube;
    public List<GameObject> hasGravity = new List<GameObject>();

    private float multiplier = 4;

    void Start()
    {
        multiplier *= Mathf.Pow(playerPlanet.transform.localScale.x, 2);      
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("hasPhysics"))
        {
            hasGravity.Add(obj.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerGravOn == true)
        {
            for (int i = 0; i < hasGravity.Count; i++)
            {
                Vector3 downVec = playerPlanet.transform.position - hasGravity[i].transform.position;
                float forceMagnitude = multiplier * grav / downVec.sqrMagnitude;
                Vector3 forceVector = downVec.normalized;

                Vector3 bodyUp = hasGravity[i].transform.up;

                Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, forceMagnitude * downVec) * hasGravity[i].transform.rotation;
                hasGravity[i].transform.rotation = Quaternion.Slerp(hasGravity[i].transform.rotation, targetRotation, 2 * Time.deltaTime);

                if (hasGravity[i].name != "CameraCube")
                {

                    Vector3 forceDown = downVec * multiplier;
                    hasGravity[i].GetComponent<Rigidbody>().AddForce(forceVector * forceMagnitude);
                }
            }
        }
    }
}
    


   
