using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemLoop : MonoBehaviour {

    private const float kilo = 1000f;
    public float thresholdInKilometers = 10f;
    public float teleportCooldownInSeconds = 0.2f; 

    private GameObject[] physicsObjects;
    private float[] lastTeleport;

    private SphereCollider boundary;    //for visualization


	// Use this for initialization
	void Start () {
        physicsObjects = GameObject.FindGameObjectsWithTag("hasPhysics");  //To be updated to hasPhysics
        lastTeleport = new float[physicsObjects.Length];
        
        boundary = this.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        boundary.center = Vector3.zero;
        boundary.radius = thresholdInKilometers * kilo;
        boundary.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {

        // Teleports physics object to opposite side of solar system boundary if:
        //      1) object is moving away from origin
        //      2) object is past solar system boundary as defined by threshold in kilometers
        //      3) object is not on teleport cooldown
        for (uint i = 0; i < physicsObjects.Length; i++)
        {
            GameObject phys = physicsObjects[i];

            Vector3 vecFromOrigin = phys.GetComponent<Rigidbody>().position;
            Vector3 velocity = phys.GetComponent<Rigidbody>().velocity;
            float distFromOrigin = vecFromOrigin.magnitude;

            bool goingAway = velocity.magnitude > double.Epsilon && Vector3.Dot(velocity.normalized, vecFromOrigin.normalized) >= 0;
            if (goingAway && (distFromOrigin > thresholdInKilometers * kilo) && (Time.time - lastTeleport[i] > teleportCooldownInSeconds))
            {
                Debug.Log("Teleport at " + Time.time);
                lastTeleport[i] = Time.time;
                phys.transform.position = (-1 * thresholdInKilometers * kilo) * vecFromOrigin.normalized;
            }
        }
	}
}
