using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinSpawner : MonoBehaviour {

    public List<GameObject> planets;
    public GameObject coinPrefab;
    public int defaultCoins = 100;

    private int totalCoins;

	// Use this for initialization
	void Start () {

        foreach (GameObject p in planets)
        {
            string planetName = p.transform.name;
            switch(planetName)
            {
                default:

                    for (int i = 0; i < defaultCoins; i++)
                    {
                        float u1 = Random.Range(-1.0f, 1.0f);
                        float u2 = Random.Range(0.0f, 1.0f);
                        Vector3 sample = UniformSphereSampler(u1, u2).normalized;

                        Vector3 spawnPosition = p.transform.position + p.transform.localScale.x * p.GetComponent<SphereCollider>().radius * sample;
                        Quaternion q = Quaternion.FromToRotation(Vector3.up, sample);

                        GameObject coin = GameObject.Instantiate(coinPrefab);
                        coin.transform.position = spawnPosition;
                        coin.transform.rotation = q * coin.transform.rotation;
                        coin.transform.SetParent(this.transform);
                    }
              
                    break;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		// Possibly spawn more coins as player picks up more?
	}

    // From http://mathworld.wolfram.com/SpherePointPicking.html
    Vector3 UniformSphereSampler(float u1, float u2)
    {
        float r = Mathf.Sqrt(1 - u1 * u1);
        float theta = 2 * Mathf.PI * u2;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return new Vector3(x, y, u1);
    }
}
