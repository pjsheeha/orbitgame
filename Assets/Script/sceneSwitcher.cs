using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class sceneSwitcher : MonoBehaviour
    {
        public List<GameObject> myObjs = new List<GameObject>();
        public GameObject sceneManager1;
        // Use this for initialization
        void Start()
        {
        figureScene();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void figureScene()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {

                if (myObjs[i].activeSelf)
                {
                    sceneManager1.GetComponent<DialogueLauncher>().scene = myObjs[i];
                }
            }
            print("WO");
            sceneManager1.GetComponent<DialogueLauncher>().substate = 0;

        }
        public void switchGet100()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[1])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }

            figureScene();
        }
        public void switchMenu()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[2])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }

            figureScene();
        }

        public void disableTutorial()
        {
            myObjs[3].SetActive(false);
        }
        public void disableArena()
        {
            myObjs[5].SetActive(false);
        }
        public void switchTutorial()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[3])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();

        }

         public void switchToAdWatch()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[11])
                {
                    myObjs[i].SetActive(true);

                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();


        }
        public void switchSubmitFunbucks()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[4])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();

        }
        public void switcharena()
        {
  
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[5])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }


        }
        public void switchDeityScene1()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[6])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();

        }
        public void switchDeityScene2()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[7])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();

        }
        public void switchDeityScene3()
        {
            for (int i = 0; i < myObjs.Count; i++)
            {
                if (myObjs[i] == myObjs[8])
                {
                    myObjs[i].SetActive(true);
                }
                else
                {
                    myObjs[i].SetActive(false);
                }
            }
            figureScene();

        }

        public void offLine()
        {
            sceneManager1.GetComponent<DialogueLauncher>().offlineGame();
            switchSubmitFunbucks();
        }
        public void online()
        {
            print("OO");
            sceneManager1.GetComponent<DialogueLauncher>().onlineGame();
            switchSubmitFunbucks();

        }


        public void substateShifter(string substate)
        {
            sceneManager1.GetComponent<NPCBehavior>().ChangeSubState(substate);
        }

        public void stateShifter(string substate)
        {
            sceneManager1.GetComponent<NPCBehavior>().ChangeState(substate);
        }

        public void revealEntity(GameObject ent)
        {
            ent.SetActive(true);
        }
        public void hideEntity(GameObject ent)
        {
            ent.SetActive(false);
        }



        public void scoresNotClose()
        {
            sceneManager1.GetComponent<DialogueLauncher>().scoresNotClose();
        }
        public void scoresAreClose()
        {
            sceneManager1.GetComponent<DialogueLauncher>().scoresAreClose();

        }

    }
