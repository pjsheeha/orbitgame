using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class NPCBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    //quick and dirty way to access n sprites: put them in an array
    //link them from the inspector manually
    //sprites[o].name will be the name of the original asset minus the extension 
    public Sprite[] sprites;
    public AudioClip[] audioClipList;
    public KeyboardDialogueUI keyboard1;
    public sceneSwitcher sceneswir;
    public DialogueLauncher dialoguemaker;

    private List<playerManager> players = new List<playerManager>();
    private AudioSource _AudioSource;

    void Start()
    {
        // TODO: Do something smarter here when we have more than 1 player
        players.Add(playerManager.Instance);

        sr = gameObject.GetComponent<SpriteRenderer>();
        _AudioSource = GetComponent<AudioSource>();

        TurnOff("Fff");
    }

    // Update is called once per frame
    void Update()
    {
        if (_AudioSource.isPlaying)
        {
            keyboard1.sound = true;
        }
        else
        {
            keyboard1.sound = false;
            keyboard1.done = false;
        }
    }

    /*
    in order for this to work you have to:
    - import Yarn.Unity above
    - call the function from yarn specifying the receiver and the arguments
    <<SetSprite Smiley cat>>
    - make sure there is a game obje with that name "Smiley" and a function that matches in some script
    - add the YarnCommand tag with the function name as referenced in the yarn
    */




    [YarnCommand("SetSprite")]
    public void SetSprite(string arg)
    {
        Sprite s = null;
        //retreive the sprite
        for (int i = 0; i < sprites.Length && s == null; i++)
        {
            if (sprites[i].name == arg)
            {
                s = sprites[i];
            }
        }

        if (s == null)
        {
            print("error: I didn't find any sprite linked in " + gameObject.name + "'s sprites called " + arg);

        }
        else
        {
            sr.sprite = s;
        }

    }
 


    [YarnCommand("MoveOn")]
    public void MoveOn(string audioName, string dspTime)
    {

        keyboard1.done = false;
       // print("Audio/" + dialoguemaker.dialogueRunner.startNode + "_" + int.Parse(audioNum) + "");
        AudioClip audioClip1 = Resources.Load<AudioClip>("Audio/" + audioName + "");
        GetComponent<AudioSource>().clip = audioClip1;
        GetComponent<AudioSource>().Play();
        keyboard1.sound = true;
        GetComponent<AudioSource>().SetScheduledEndTime(int.Parse(dspTime));


    }



    [YarnCommand("ChangeState")]
    public void ChangeState(string stateVal)
    {
        dialoguemaker.state = int.Parse(stateVal);

    }

    [YarnCommand("ChangeSubState")]
    public void ChangeSubState(string stateVal)
    {
        dialoguemaker.substate = int.Parse(stateVal);

    }

    [YarnCommand("AddSubState")]
    public void AddSubState(string stateVal)
    {
        dialoguemaker.substate += int.Parse(stateVal);
    }

    [YarnCommand("AffectControl")]
    public void AffectControl(string stateVal)
    {
        if (bool.Parse(stateVal) == true)
        {
            dialoguemaker.playerController.GetComponent<EventSystem>().enabled = true;

        }
        if (bool.Parse(stateVal) == false)
        {
            dialoguemaker.playerController.GetComponent<EventSystem>().enabled = false;

        }
    }


    [YarnCommand("SpawnPosition")]
    public void SpawnPosition(string state)
    {
        if (state == "tutorial")
        {
            players[0].transform.position = new Vector3(-24.98f, 1.5f, 8.853f);
        }
        else if (state == "arena")
        {
            players[0].transform.position = new Vector3(-26.8f, 130.5f, 41.6f);
        }
    }



    [YarnCommand("TurnOff")]
    public void TurnOff(string obj)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
                players[i].DisablePlayer();
        }
    }
    [YarnCommand("TurnOn")]
    public void TurnOn(string obj)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
                players[i].EnablePlayer();
        }
    }

    [YarnCommand("StopDialogue")]
    public void StopDialogue()
    {
        GetComponent<DialogueLauncher>().previousRunning = GetComponent<DialogueLauncher>().dialogueRunner.isDialogueRunning;
    }

    [YarnCommand("SetScene")]
    public void SetScene(string sceneName)
    {
        if (sceneName == "menu")
        {
           

            sceneswir.switchMenu();
        }
        if (sceneName == "arena")
        {
            sceneswir.switcharena();
        }
        if (sceneName == "Get100")
        {
            sceneswir.switchGet100();
        }
        if (sceneName == "Tutorial")
        {
            sceneswir.switchTutorial();
        }
        if (sceneName == "deityscene1")
        {
            sceneswir.switchDeityScene1();
        }
        if (sceneName == "deityscene2")
        {
            sceneswir.switchDeityScene2();
        }
        if (sceneName == "deityscene3")
        {
            sceneswir.switchDeityScene3();
        }
        if (sceneName == "submiteFunbucks")
        {
            sceneswir.switchSubmitFunbucks();
        }

    }
}
