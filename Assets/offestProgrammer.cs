using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offestProgrammer : MonoBehaviour {

    // Scroll main texture based on time

    public float scrollSpeed = 0.5f;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}