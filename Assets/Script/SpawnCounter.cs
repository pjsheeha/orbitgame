using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        PlanetSpawner ps;
        if (ps = GetComponentInParent<PlanetSpawner>())
            ps.activeSpawns--;

        MeshVertexSpawner ms;
        if (ms = GetComponentInParent<MeshVertexSpawner>())
            ms.activeSpawns--;
    }
}
