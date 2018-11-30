using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to gameobject prefab to spawn
public class MeshVertexSpawner : MonoBehaviour {

    public GameObject spawnPrefab;
    public GameObject[] meshSpawners;
    public float respawnThreshold = 0.5f;
    public float respawnDelay = 1f;
    public float cullRadius = 5f;
    public bool useColors = true;

    public int activeSpawns;
    private int initialSpawns;
    private List<GameObject> spawnedObjects;

	// Use this for initialization
	void Start () {

        spawnedObjects = new List<GameObject>();
        SpawnMeshVertex();

        activeSpawns = spawnedObjects.Count;
        initialSpawns = activeSpawns;
	}
	
	// Update is called once per frame
	void Update () {
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

    private void SpawnMeshVertex()
    {
        foreach (GameObject meshObj in meshSpawners)
        {
            MeshFilter m;
            if (!(m = meshObj.GetComponent<MeshFilter>()))
            {
                Debug.Log("No mesh filter component assigned to mesh object " + meshObj.transform.name);
                continue;
            }

            for (int i = 0; i < m.mesh.vertices.Length; i++)
            {
                if (useColors)
                {
                    if (m.mesh.colors.Length == 0)
                        continue;

                    Color c = m.mesh.colors[i];
                    if (c != Color.red)
                        continue;
                }

                Vector3 vertex = m.mesh.vertices[i];

                Vector3 spawnPosition = meshObj.transform.position + Vector3.Scale(meshObj.transform.rotation * vertex, meshObj.transform.localScale);

                if (!PlanetSpawner.CheckSpawnDistanceThreshold(spawnPosition, cullRadius))
                {
                    Quaternion q = Quaternion.FromToRotation(Vector3.up, meshObj.transform.rotation * m.mesh.normals[i]);

                    GameObject obj = GameObject.Instantiate(spawnPrefab);
                    obj.transform.position = spawnPosition;
                    obj.transform.rotation = q * obj.transform.rotation;
                    obj.transform.SetParent(transform);

                    spawnedObjects.Add(obj);
                }
            }
        }
    }
}
