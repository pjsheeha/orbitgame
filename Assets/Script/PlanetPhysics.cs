using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From https://gamedev.stackexchange.com/questions/106623/how-can-i-accurately-simulate-orbits-in-unity
public class PlanetPhysics : MonoBehaviour {

    const float multiplier = 100f;
    public float gravity = 100f;
    public float rotationSpeed = 300f;
    public GameObject player;

    private void Start()
    {
        Vector3 initialPush = 500 * (this.transform.position - player.GetComponent<Rigidbody>().position).normalized;
        player.GetComponent<Rigidbody>().AddForce(initialPush);
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            ApplyGravity(rb);

            if (other.CompareTag("Player"))
            {
                Debug.Assert((rb.constraints & RigidbodyConstraints.FreezeRotation) != RigidbodyConstraints.None);
                OrientToPlanetNormal(rb);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        // Handle player rotation in atmosphere manually
        if (other.CompareTag("Player"))
            rb.constraints |= RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotation;
    }

    public void ApplyGravity(Rigidbody rb)
    {
        Vector3 direction = this.transform.position - rb.position;

        Vector3 forceDirection = direction.normalized;
        float forceMagnitude = multiplier * gravity / direction.sqrMagnitude;

        Vector3 forceVector = forceDirection * forceMagnitude;

        rb.AddForce(forceVector);
    }
    
    public void OrientToPlanetNormal(Rigidbody rb)
    {
        Vector3 targetNormal = (rb.position - this.transform.position).normalized;
        Debug.DrawRay(this.transform.position, targetNormal, Color.white);

        if (targetNormal == rb.transform.up) return;

        float angle = Vector3.SignedAngle(rb.transform.up, targetNormal, rb.transform.right);
        Quaternion targetRotation = Quaternion.AngleAxis(angle, rb.transform.right);
        Quaternion finalRotation = Quaternion.LerpUnclamped(rb.transform.rotation, rb.transform.rotation * targetRotation, Time.time * rotationSpeed);
        rb.MoveRotation(finalRotation);
    }
}
