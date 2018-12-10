using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementScript))]
public class playerManager : MonoBehaviour
{
    public static playerManager Instance { get; private set; }

    public string playerName;
    public Color playerColor;
    [SerializeField] float respawnTime = 5f;

    public Camera cam;

    public Camera deathCamera
    {
        get
        {
            return _deathCamera;
        }
        set
        {
            if (_deathCamera != null)
                _deathCamera.gameObject.SetActive(false);

            _deathCamera = value;
        }
    }
    private Camera _deathCamera;

    public bool alive = false;
    Animator anim; //the animator accessed from the network animator

    public FirstPersonController fpsController { get; private set; }


    //we only send animator paramters for other players to do
    void Start()
    {
       
        DisablePlayer();
        SyncCameraEnabled();
    }

    void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Tried to create two playerss at a time!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SyncCameraEnabled()
    {
        fpsController.enabled = alive;


        if (deathCamera != null && alive == deathCamera.gameObject.activeSelf)
            deathCamera.gameObject.SetActive(!alive);
    }

    private void Update()
    {
        SyncCameraEnabled();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void DisablePlayer()
    {
        alive = false;
    }

    public void EnablePlayer()
    {
        alive = true;
    }

    public void Die()
    {
        anim.SetTrigger("Died");
        DisablePlayer();
    }

    void Respawn()
    {

        anim.SetTrigger("Restart");

        //Transform spawn = NetworkManager.singleton.GetStartPosition();
        //transform.position = spawn.position;
        //transform.rotation = spawn.rotation;

        anim.SetTrigger("Restart"); // kicked back into default state


        EnablePlayer();
    }


    void OnNameChanged(string value)
    {
        playerName = value;
        //this.gameObject.name = playerName;

        // GetComponentInChildren<Text> (true).text = playerName; //looks for disabled objects ;>
    }
    void OnColorChanged(Color value)
    {
        playerColor = value;
        //GetComponentInChildren<RendererToggler>().ChangeColor(playerColor);
    }

    public void Won()
    {
        print("Won!");
        GameObject.Find("Smiley").GetComponent<DialogueLauncher>().substate = -2;
    }
    public void Lose()
    {
        print("Lose!");
        GameObject.Find("Smiley").GetComponent<DialogueLauncher>().substate = -2;
    }
    public void ScoresClose()
    {
        GameObject.Find("Smiley").GetComponent<DialogueLauncher>().scoresAreClose();
    }
    public void ScoresNotClose()
    {
        GameObject.Find("Smiley").GetComponent<DialogueLauncher>().scoresNotClose();

    }



    /*
     public void Won()
     {
         //tell other players

         for (int i = 0; i < players.Count; i++)
             players[i].RpcGameOver(netId, name);

         //eventually go back to lobby
         Invoke("BackToLobby", 5f);

     }
 */
    /*
   // /[ClientRpc]
    void GameOver(string name) // who won? network id is unique
    {
        DisablePlayer();

        //releases cursor! oooh
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    */

}

