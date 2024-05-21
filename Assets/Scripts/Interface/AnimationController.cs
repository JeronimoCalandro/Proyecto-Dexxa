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

namespace BitelPlanetaParacaidasGame
{
    public class AnimationController : MonoBehaviour
    {
        //Objects
        public GameObject animationSceneOn;
        public GameObject animationSceneOff;

        //Values
        public bool animateSceneOn = true;
        public bool animateSceneOff = true;

        public bool isSetToAnimationHolder = false;

        public bool isIncreaseSize = false;
        public float increaseBy = 2f;

        #region Standart system methods

        void Start()
        {
            if (animateSceneOn)
                ShowSceneOn();
        }

        #endregion

        #region Work with animations

        private void ShowSceneOn()
        {
            SetObject(animationSceneOn);
        }

        private void ShowSceneOff()
        {
            if (isSetToAnimationHolder)
                SetObjectToCamera(animationSceneOff);
            else
                SetObject(animationSceneOff);
        }

        private void SetObject(GameObject objectToSet)
        {
            Vector3 localScale = objectToSet.transform.localScale;

            GameObject newObject = (GameObject)Instantiate(objectToSet);
            newObject.transform.localPosition = objectToSet.transform.localPosition;
            newObject.transform.localScale = new Vector3(
                isIncreaseSize ? localScale.x * increaseBy : localScale.x,
                isIncreaseSize ? localScale.y * increaseBy : localScale.y,
                isIncreaseSize ? localScale.z * increaseBy : localScale.z);
        }

        private void SetObjectToCamera(GameObject objectToSet)
        {
            Vector3 localScale = objectToSet.transform.localScale;
            GameObject animationHolder = GameObject.Find("AnimationHolder");

            GameObject newObject = (GameObject)Instantiate(objectToSet);
            newObject.transform.SetParent(animationHolder.transform);
            newObject.transform.localPosition = objectToSet.transform.localPosition;
            newObject.transform.localScale = new Vector3(
                isIncreaseSize ? localScale.x * increaseBy : localScale.x,
                isIncreaseSize ? localScale.y * increaseBy : localScale.y,
                isIncreaseSize ? localScale.z * increaseBy : localScale.z);
        }

        #endregion

        #region Notifications

        public void SceneWillBeClosedSoon()
        {
            if (animateSceneOff)
                ShowSceneOff();
        }

        #endregion
    }
}
