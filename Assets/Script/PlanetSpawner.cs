using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to planet
public class PlanetSpawner : MonoBehaviour {

    public enum SpawnType
    {
        UniformRandom,
        Spherical,
        Mesh
    };

    public SpawnType spawnType;
    public GameObject spawnPrefab;
    //public int respawnThreshold = 0.5f; // fraction of initial spawns remaining before respawn
    //public float cullRadius = 0.5f; // guaranteed distance between spawns 

    // Uniform Random - Spawns a specified number by uniformly randomly picking positions
    public int randomSpawns = 100;

    // Spherical - Spawns along latitude and longitude divisions
    public int thetaDivisions = 40; // latitude
    public int phiDivisions = 20;   // longitude
    public Vector3 sphereUp;

    // Mesh - Spawns on vertices of given mesh. Mesh must have same origin as planet.
    public GameObject meshSpawner;

    private int initialSpawns;
    private int totalSpawns;

    void Start() {
        SpawnObjects();
    }

    void Update() {
        // How to count objects being picked up?
        /* if (totalSpawns < respawnThreshold * initialSpawns)
         *      SpawnObjects();
         * */
    }

    public void SpawnObjects()
    {
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
        initialSpawns = totalSpawns;
    }

    public void SpawnMesh()
    {
        Mesh m;
        if (!meshSpawner || meshSpawner.transform.position != transform.position ||
            !(m = meshSpawner.GetComponent<Mesh>()))
            return;

        foreach (Vector3 vertex in m.vertices)
        {
            Vector3 spawnPosition = GetSpawnPositionFromUnitVector(vertex.normalized);
            Quaternion q = Quaternion.FromToRotation(Vector3.up, vertex.normalized);
            SpawnObject(spawnPosition, q);
        }
    }

    public void SpawnSpherical()
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
                Quaternion q = Quaternion.FromToRotation(Vector3.up, v);
                SpawnObject(spawnPosition, q);
            }
        }
    }

    public void SpawnUniformlyRandom()
    {
        for (int i = 0; i < randomSpawns; i++)
        {
            float u1 = Random.Range(-1.0f, 1.0f);
            float u2 = Random.Range(0.0f, 1.0f);
            Vector3 sample = UniformSphereSampler(u1, u2).normalized;

            Vector3 spawnPosition = GetSpawnPositionFromUnitVector(sample);
            Quaternion q = Quaternion.FromToRotation(Vector3.up, sample);
            SpawnObject(spawnPosition, q);
        }
    }

    private Vector3 GetSpawnPositionFromUnitVector(Vector3 v)
    {
        return transform.position + transform.localScale.x * GetComponent<SphereCollider>().radius * v;
    }

    private void SpawnObject(Vector3 position, Quaternion q)
    {
        GameObject obj = GameObject.Instantiate(spawnPrefab);
        obj.transform.position = position;
        obj.transform.rotation = q * obj.transform.rotation;
        obj.transform.SetParent(this.transform);

        totalSpawns++;
    }

    // From http://mathworld.wolfram.com/SpherePointPicking.html
    private Vector3 UniformSphereSampler(float u1, float u2)
    {
        float r = Mathf.Sqrt(1 - u1 * u1);
        float theta = 2 * Mathf.PI * u2;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return new Vector3(x, y, u1);
    }
}
