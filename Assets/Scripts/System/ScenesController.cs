/* 
 *       _______             _____ __            ___     
 *      /_  __(_)___  __  __/ ___// /___  ______/ (_)___ 
 *       / / / / __ \/ / / /\__ \/ __/ / / / __  / / __ \
 *      / / / / / / / /_/ /___/ / /_/ /_/ / /_/ / / /_/ /
 *     /_/ /_/_/ /_/\__, //____/\__/\__,_/\__,_/_/\____/ 
 *                 /____/                                
 *
 *      Created by Alice Vinnik in 2022.
 * 
 *      If you want to reskin, customization or development contact me. Im available to hire.
 *      Website: https://alicevinnik.wixsite.com/tinystudio
 *      Email: tinystudio.main@gmail.com
 *      
 *      Thanks for buying my codes.
 *      Have a nice day!
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BitelPlanetaParacaidasGame
{
    public class ScenesController : MonoBehaviour
    {
        //Values
        static public ScenesController instance;
        public float delay = 2f;

        [Header("Scenarios")]
        public bool isLaunchScreen = false;
        public float launchScreenDelay = 1.6f;

        #region Standart system methods

        void Awake()
        {
            instance = this;

            RunScenarios();
        }

        #endregion

        #region Change Scenes

        /// <summary>
        /// Use that method for change scene.
        /// </summary>
        /// <param name="sceneName">Name of future scene</param>
        public void ChangeScene(string name)
        {
            //Save previous scene
            CustomPlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);

            //If we open game it first time open tutorial
            name = TryToOpenTutorialIfPlayerPressToStartGame(name);
            MarkTutorialAsOpenedIfWeOpenTutorial(name);

            //Open new scene
            SceneManager.LoadScene(name);
        }

        public void ChangeSceneWithDelay(string name)
        {
            //If we open game it first time open tutorial
            GameObject animationController = GameObject.Find("AnimationController");
            if (animationController != null)
                animationController.GetComponent<AnimationController>().SceneWillBeClosedSoon();

            instance.StartCoroutine(instance.RunSceneWithDelay(name));

            SoundController.instance.stopMusicSound();
        }

        private IEnumerator RunSceneWithDelay(string name)
        {
            yield return new WaitForSeconds(delay);

            ChangeScene(name);
        }

        /// <summary>
        /// Call that method for open previous scene
        /// </summary>
        public void BackToPreviousScene()
        {
            SceneManager.LoadScene(CustomPlayerPrefs.GetString("previousScene", "MenuScene"));
        }

        public void BackToPreviousSceneWithDelay()
        {
            GameObject animationController = GameObject.Find("AnimationController");
            if (animationController != null)
                animationController.GetComponent<AnimationController>().SceneWillBeClosedSoon();

            instance.StartCoroutine(instance.RunBackToPreviousSceneWithDelay());
        }

        IEnumerator RunBackToPreviousSceneWithDelay()
        {
            yield return new WaitForSeconds(delay);

            BackToPreviousScene();
        }

        private string TryToOpenTutorialIfPlayerPressToStartGame(string name)
        {
            if (name == "GameScene")
            {
                if (CustomPlayerPrefs.GetInt("tutorialWasOpened", 0) == 0)
                {
                    CustomPlayerPrefs.SetInt("tutorialWasOpened", 1);
                    CustomPlayerPrefs.SetString("previousScene", "GameScene");
                    return "TutorialScene";
                }
            }

            return name;
        }

        private void MarkTutorialAsOpenedIfWeOpenTutorial(string name)
        {
            if (name == "TutorialScene")
            {
                CustomPlayerPrefs.SetInt("tutorialWasOpened", 1);
            }
        }

        #endregion

        #region Special transactions

        private void RunScenarios()
        {
            if (isLaunchScreen)
                instance.StartCoroutine(instance.ScenarioLaunchScreen());
        }

        IEnumerator ScenarioLaunchScreen()
        {
            yield return new WaitForSeconds(launchScreenDelay);

            ChangeSceneWithDelay("MenuScene");
        }

        #endregion
    }
}