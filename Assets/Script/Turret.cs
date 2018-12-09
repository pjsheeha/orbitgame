using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public enum FalloffType
    {
        Linear,
        None
    };

    public GameObject bulletPrefab;
    public float fireDelay = 3.0f;
    public float bulletSpeed = 5.0f;
    //public float accuracy = 85.0f;
    public float minAccuracyRadius = 0.1f;
    public FalloffType accuracyFalloff = FalloffType.Linear;

    private GameObject player;
    private bool noticed = false;
    private float elapsedSinceLastFire = 0;
    private float noticeRadius;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        noticeRadius = transform.localScale.x * GetComponent<SphereCollider>().radius;
	}
	
	// Update is called once per frame
	void Update () {
		if (noticed)
        {
            transform.LookAt(player.transform);
            if (elapsedSinceLastFire >= fireDelay)
            {
                ShootAtPlayer();
            }
            elapsedSinceLastFire += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Assert(!noticed);
            noticed = true;
            elapsedSinceLastFire = 0;

            Debug.Log("Turret " + transform.name + " noticed player");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Assert(noticed);
            noticed = false;

            Debug.Log("Turret " + transform.name + " lost player");
        } else if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    private void ShootAtPlayer()
    {
        Vector3 aimVector = AimAtPlayer();
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = transform.position + aimVector;
        bullet.GetComponent<Rigidbody>().velocity = bulletSpeed * aimVector;
        elapsedSinceLastFire = 0;
    }

    // Returns aim vector to player
    private Vector3 AimAtPlayer()
    {
        Vector3 vecToPlayer = player.transform.position - transform.position;

        switch (accuracyFalloff)
        {
            case FalloffType.Linear:
                float accuracyRadius = (vecToPlayer.magnitude / noticeRadius) * minAccuracyRadius;
                Vector3 offset = Random.insideUnitCircle * accuracyRadius;
                Vector3 newTarget = transform.position + vecToPlayer.normalized + transform.rotation * offset;

                Debug.DrawRay(transform.position, noticeRadius * (newTarget - transform.position).normalized, Color.white, fireDelay);

                return (newTarget - transform.position).normalized;
            default:

                Debug.DrawRay(transform.position, vecToPlayer, Color.white, fireDelay);
                return vecToPlayer.normalized;
        }
    }
}
