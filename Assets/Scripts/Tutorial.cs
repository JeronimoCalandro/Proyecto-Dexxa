using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieDriveGame;

namespace Bitel.PlanetaCorrer
{
    public class Tutorial : MonoBehaviour
    {
        public static Tutorial instance;

        public GameObject tutorialFirstStep;

        public bool wasTutorialSeen;

        public GameObject playNowButton;
        public GameObject[] objects;
        public Text tutorialText;
        public GameObject obstacles;
        public Animator lightAnimator;

        float speed;
        //public PlayerManager playerManager;

        public bool canStart = false;
        int tutorialCont;
        void Awake()
        {
            instance = this;
            
        }

        private void Start()
        {
            tutorialCont = 1;
            StartCoroutine(TutorialDisplayCoroutine());
            /*if (LevelManager.instance.levelNumber == 1)
            {
                if (!wasTutorialSeen)
                {

                    
                }
            }
            else this.enabled = false;*/
        }

        private void Update()
        {
            /*if(!canStart)
                ZDGGameController.instance.playerObject.speed = 0;*/
        }

        public void SubstractTutorial()
        {
            if (tutorialCont > 1)
            {
                tutorialCont--;
            }
            SetTutorial();
        }

        public void AddTutorial()
        {
            if (tutorialCont < 5)
            {
                tutorialCont++;
            }

            SetTutorial();
        }

        public void SetTutorial()
        {
            switch (tutorialCont)
            {
                case 1:
                    tutorialFirstStep.SetActive(true);
                    //tutorialSecondStep.SetActive(false);
                    break;

                default:
                    break;
            }
        }

        public void PlayNow()
        {
            tutorialFirstStep.SetActive(false);

            playNowButton.SetActive(false);
            lightAnimator.enabled = true;

            foreach (var item in objects)
            {
                item.SetActive(false);
            }

            canStart = true;
            ZDGGameController.instance.playerObject.speed = speed;
            ZDGGameController.instance.tutorial = false;
            ZDGGameController.instance.ReadyGoEffecf();
            StartCoroutine(ZDGGameController.instance.IncresedSpeed());
            this.enabled = false;
            //pauseMenu.StartCountdown();
        }

        IEnumerator TutorialDisplayCoroutine()
        {
            yield return new WaitForSeconds(0f);

            speed = ZDGGameController.instance.playerObject.speed;
            ZDGGameController.instance.playerObject.speed = 0;
            
            tutorialFirstStep.SetActive(true);

            playNowButton.SetActive(true);
            obstacles.SetActive(true);

            if (ZDGGameController.instance.IsMobileCheck.CheckMobile())
            {
                
                foreach (var item in objects)
                {
                    item.SetActive(true);
                }
            }
            else tutorialText.text = "Evita chocar con los obstaculos moviendote hacia los costados usando las flechas del teclado";


        }


        IEnumerator Message()
        {
            yield return new WaitForSeconds(4.5f);
            canStart = true;
        }
    }
}


