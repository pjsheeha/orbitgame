using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlanet : MonoBehaviour {

    public GameObject player;
    public GameObject restartScreen;

    private bool showRestart = false;

	// Use this for initialization
	void Start () {
        if (restartScreen != null)
            restartScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (showRestart && restartScreen != null)
            restartScreen.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            showRestart = true;
        }
    }
}
