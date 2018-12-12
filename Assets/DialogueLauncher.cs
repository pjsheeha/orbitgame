using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using UnityEngine.EventSystems;


    public class DialogueLauncher : MonoBehaviour
    {

        //this script is meant to be attached to objects that trigger dialogues
        [Tooltip("Drag and drop dialogue object here")]
        public DialogueRunner dialogueRunner;
        public GameObject PlayerEnabler; 
       // public GameObject tutorialStages;
        public GameObject player;
        public int state = 0;
        public GameObject scene;
        public int substate = 0;
        //public bool try= true;
        //during dialogue disable the controller ie the movement
        [Tooltip("Drag and drop the player object here")]
        public GameObject playerController;

        public string startNode = "Start";

        public float range = 3;

        //fugure out when it stops running
        public bool previousRunning = false;

        // Use this for initialization
        void Start()
        {

            if (dialogueRunner == null)
            {
                print("No dialogue runner reference!");
            }

            if (playerController == null)
            {
                print("No player controller reference!");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
            
            //check the distance to the player
            //float dist = Vector3.Distance(playerController.gameObject.transform.position, gameObject.transform.position);
            if (substate != -1)
            {
                if (!dialogueRunner.isDialogueRunning)
                {
                    //saveScript.ChangeState();
                    dialogueRunner.startNode = state.ToString() + "_" + scene.name + "_" + substate.ToString();
                    dialogueRunner.StartDialogue();
                    print(state.ToString() + "_" + scene.name + "_" + substate.ToString());
                    //playerController.GetComponent<EventSystem>().enabled = false;
                    substate = -1;
                }
            }

            if (substate != -1)
            {
                previousRunning = dialogueRunner.isDialogueRunning;
            }
        }

        public void scoresNotClose()
        {
            dialogueRunner.startNode = "scoresNotClose";
                      //  dialogueRunner.StartDialogue();

           // dialogueRunner.StartDialogue();
        }
        public void scoresAreClose()
        {
            dialogueRunner.startNode = "scoresAreClose";
                      //  dialogueRunner.StartDialogue();


           // dialogueRunner.StartDialogue();
        }
        public void offlineGame()
        {
            print("WOWEEEEEEE");
            dialogueRunner.startNode = "offlinegame";
                        dialogueRunner.StartDialogue();



            //dialogueRunner.StartDialogue();
        }
        public void onlineGame()
        {
            dialogueRunner.startNode = "onlinegame";
                        dialogueRunner.StartDialogue();
                        


            //dialogueRunner.StartDialogue();
        }
        public void sub4()
        {
   
        substate = 4;

        }
        
        
    }
