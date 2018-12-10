using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public int pointsToDestroy = 0;
    public int pointsEarned = 5;

    public bool explode = true;
    public float explodeForce = 500.0f;
    public float explodeRadius = 30.0f;
    public float destroyTimeScale = 1.0f;
    public float shardLifetime = 5.0f;

    private Mesh m;
    private MeshFilter mf;
    private MeshRenderer mr;

	// Use this for initialization
	void Start () {

		if (!(mf = GetComponent<MeshFilter>()))
        {
            Debug.Log("Destructible: No MeshFilter component assigned to object " + transform.name);
            this.enabled = false;
        }

        if (!(mr = GetComponent<MeshRenderer>()))
        {
            Debug.Log("Destructible: No MeshRenderer component assigned to object " + transform.name);
            this.enabled = false;
        }

        m = mf.mesh;
        Debug.Log("Destructible " + transform.name + ": " + m.subMeshCount + " submeshes, " + (m.triangles.Length / 3) + " triangles");

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player")
            && obj.GetComponentInChildren<PlayerProgress>().GetTotalPoints() >= pointsToDestroy)
        {
            obj.GetComponentInChildren<PlayerProgress>().AddPoints(pointsEarned);
            StartCoroutine(SplitMesh());
        }
    }

    //Based on https://answers.unity.com/questions/256558/Convert-mesh-to-triangles-js.html
    private IEnumerator SplitMesh()
    {
        GetComponent<Collider>().enabled = false;

        Vector3[] verts = m.vertices;
        Vector3[] norms = m.normals;
        Vector2[] uvs = m.uv;

        for (int submesh = 0; submesh < m.subMeshCount; submesh++)
        {
            int[] indices = m.GetTriangles(submesh);
            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNorms = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];

                for (int j = 0; j < 3; j++)
                {
                    int index = indices[i + j];
                    newVerts[j] = verts[index];
                    newNorms[j] = norms[index];
                    newUvs[j] = uvs[index];
                }

                Mesh newMesh = new Mesh();
                newMesh.vertices = newVerts;
                newMesh.normals = newNorms;
                newMesh.uv = newUvs;

                newMesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };    //has front and back face
                GameObject shard = new GameObject(transform.name + "-shard" + submesh + "-" + (i / 3));
                shard.transform.position = transform.position;
                shard.transform.rotation = transform.rotation;
                shard.transform.localScale = transform.localScale;
                shard.AddComponent<MeshRenderer>().material = mr.materials[submesh];
                shard.AddComponent<MeshFilter>().mesh = newMesh;
                shard.AddComponent<BoxCollider>();
                shard.AddComponent<Rigidbody>();

                if (explode)
                    shard.GetComponent<Rigidbody>().AddExplosionForce(explodeForce, transform.position, explodeRadius);

                Destroy(shard, shardLifetime);
            }
        }

        Time.timeScale = destroyTimeScale;
        mr.enabled = false;
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
