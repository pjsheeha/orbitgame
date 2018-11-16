using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour {

    public List<GameObject> planets;
    public GameObject spawnPrefab;
    public int defaultSpawns = 100;

    private int totalSpawns;

	// Use this for initialization
	void Start () {

        foreach (GameObject p in planets)
        {
            string planetName = p.transform.name;
            switch (planetName)
            {
                case "StartPlanet":
                    SpawnAlongCircumfrence(p, 0);
                    break;

                default:
                    SpawnUniformly(p);
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		// Possibly spawn more coins as player picks up more?
	}

    public void SpawnAlongCircumfrence(GameObject p, float theta)
    {
        SpawnUniformly(p);
    }

    public void SpawnUniformly(GameObject p)
    {
        for (int i = 0; i < defaultSpawns; i++)
        {
            float u1 = Random.Range(-1.0f, 1.0f);
            float u2 = Random.Range(0.0f, 1.0f);
            Vector3 sample = UniformSphereSampler(u1, u2).normalized;

            Vector3 spawnPosition = p.transform.position + p.transform.localScale.x * p.GetComponent<SphereCollider>().radius * sample;
            Quaternion q = Quaternion.FromToRotation(Vector3.up, sample);

            GameObject coin = GameObject.Instantiate(spawnPrefab);
            coin.transform.position = spawnPosition;
            coin.transform.rotation = q * coin.transform.rotation;
            coin.transform.SetParent(this.transform);
        }
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
