using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to planet
public class PlanetSpawner : MonoBehaviour {

    public GameObject atmosphere;

    public enum SpawnType
    {
        UniformRandom,
        Spherical,
        Mesh
    };

    public SpawnType spawnType;
    public GameObject spawnPrefab;
    public float respawnThreshold = 0.5f; // fraction of initial spawns remaining before respawn
    public float respawnDelay = 1f;
    public float cullRadius = 5f; // guaranteed distance in m^2 between spawns 

    // Uniform Random - Spawns a specified number by uniformly randomly picking positions
    public int randomSpawns = 100;

    // Spherical - Spawns along latitude and longitude divisions
    public int thetaDivisions = 40; // latitude
    public int phiDivisions = 20;   // longitude
    public Vector3 sphereUp;

    // Mesh - Spawns on vertices of given mesh. Mesh must have same origin as planet.
    public GameObject meshSpawner;

    private List<GameObject> spawnedObjects;
    private int initialSpawns;
    public int activeSpawns;

    private int layerMask;
    private float distToAtmosphere;
    private float distToPlanet;
    private float maxDistRay;

    void Start() {

        layerMask = ~(1 << 2);
        distToAtmosphere = transform.localScale.x * atmosphere.transform.localScale.x * atmosphere.GetComponent<SphereCollider>().radius;
        distToPlanet = transform.localScale.x * GetComponent<SphereCollider>().radius;
        maxDistRay = distToAtmosphere - distToPlanet + 1;

        Debug.Log(distToAtmosphere + " " + distToPlanet + " " + maxDistRay);

        spawnedObjects = new List<GameObject>();
        switch (spawnType)
        {
            case SpawnType.Mesh:
                SpawnMesh();
                break;

            case SpawnType.Spherical:
                SpawnSpherical();
                break;

            default:
                SpawnUniformlyRandom();
                break;
        }

        activeSpawns = spawnedObjects.Count;
        initialSpawns = activeSpawns;
    }

    void Update() {
        if (activeSpawns < respawnThreshold * initialSpawns)
            StartCoroutine(WaitAndRespawn());
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        foreach (GameObject obj in spawnedObjects)
        {
            obj.SetActive(true);
        }
        activeSpawns = initialSpawns;
    }

    private void SpawnMesh()
    {
        MeshFilter m;
        if (!meshSpawner)
        {
            Debug.Log("No mesh assigned to planet " + transform.name);
            return;
        }

        if (!(m = meshSpawner.GetComponent<MeshFilter>()))
        {
            Debug.Log("No mesh filter component assigned to mesh object " + meshSpawner.transform.name);
            return;
        }

        foreach (Vector3 vertex in m.mesh.vertices)
        {
            Vector3 spawnPosition = GetSpawnPositionFromUnitVector(vertex.normalized);

            if (spawnPosition != Vector3.zero && !CheckSpawnDistanceThreshold(spawnPosition, cullRadius))
            {
                Quaternion q = Quaternion.FromToRotation(Vector3.up, vertex.normalized);
                SpawnObject(spawnPosition, q);
            }
        }
    }

    private void SpawnSpherical()
    {
        if (sphereUp == Vector3.zero)
            sphereUp = Vector3.up;
        sphereUp = sphereUp.normalized;

        // Spawn along spherical coordinates
        for (int phiCount = 1; phiCount < phiDivisions; phiCount++)
        {
            for (int thetaCount = 0; thetaCount <= thetaDivisions; thetaCount++)
            {
                float phi = phiCount * Mathf.PI / phiDivisions;
                float theta = thetaCount * 2 * Mathf.PI / thetaDivisions;

                float sphi = Mathf.Sin(phi);
                float x = sphi * Mathf.Cos(theta);
                float z = sphi * Mathf.Sin(theta);
                float y = Mathf.Cos(phi);

                Vector3 v = new Vector3(x, y, z);

                if (sphereUp != Vector3.up)
                {
                    Quaternion rt = Quaternion.FromToRotation(Vector3.up, sphereUp);
                    v = rt * v;
                }

                Vector3 spawnPosition = GetSpawnPositionFromUnitVector(v);
                if (spawnPosition != Vector3.zero && !CheckSpawnDistanceThreshold(spawnPosition, cullRadius))
                {
                    Quaternion q = Quaternion.FromToRotation(Vector3.up, v);
                    SpawnObject(spawnPosition, q);
                }
            }
        }
    }

    private void SpawnUniformlyRandom()
    {
        for (int i = 0; i < randomSpawns; i++)
        {
            Vector3 sample = Random.onUnitSphere;

            Vector3 spawnPosition = GetSpawnPositionFromUnitVector(sample);
            if (spawnPosition != Vector3.zero && !CheckSpawnDistanceThreshold(spawnPosition, cullRadius))
            {
                Quaternion q = Quaternion.FromToRotation(Vector3.up, sample);
                SpawnObject(spawnPosition, q);
            }
        }
    }

    private Vector3 GetSpawnPositionFromUnitVector(Vector3 v)
    {
        Vector3 startPosition = transform.position + distToAtmosphere * v;
        RaycastHit hit;
        if (!(Physics.Raycast(startPosition, -v, out hit, distToAtmosphere, layerMask)))
        {
            Debug.Log("Raycast " + v + " from " + transform.name + " didn't hit!");
            Debug.DrawRay(startPosition, -v);
            return Vector3.zero;
        }

        return hit.point;
    }

    private void SpawnObject(Vector3 position, Quaternion q)
    {
        GameObject obj = GameObject.Instantiate(spawnPrefab);
        obj.transform.position = position;
        obj.transform.rotation = q * obj.transform.rotation;
        obj.transform.SetParent(transform);

        spawnedObjects.Add(obj);
    }

    public static bool CheckSpawnDistanceThreshold(Vector3 p, float cull)
    {
        if (cull == 0)
            return false;

        Collider[] inRadius = Physics.OverlapSphere(p, cull);
        foreach (Collider c in inRadius)
        {
            if (c.gameObject.CompareTag("coin"))
                return true;
        }

        return false;
    }
}