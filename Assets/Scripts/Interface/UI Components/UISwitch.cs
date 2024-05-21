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
using UnityEngine.UI;
using UnityEngine.Events;

namespace BitelPlanetaParacaidasGame
{
    public class UISwitch : MonoBehaviour
    {
        //Components
        private Text text;
        private Image pin;
        private Animation pinAnimation;

        //Values
        private bool isInitialisationState = true;

        [System.Serializable]
        public enum Mode { On, Off };
        private Mode mode = Mode.On;

        private float positionPinDefault = 23.17f;
        private float positionPin = 25.0f;

        public float speed = 5.0f;
        public string value = "";
        public UnityEvent valueChanged;

        #region Standart system methods

        void Start()
        {
            GetComponents();
            LoadState();
        }

        private void FixedUpdate()
        {
            MovePin();
        }

        private void GetComponents()
        {
            text = transform.Find("Text").GetComponent<Text>();
            pin = transform.Find("Pin").GetComponent<Image>();
            pinAnimation = pin.gameObject.GetComponent<Animation>();

            positionPinDefault = pin.GetComponent<RectTransform>().localPosition.x;
        }

        #endregion

        #region Work with state

        private void LoadState()
        {
            mode = CustomPlayerPrefs.GetBool(value, true) ? Mode.On : Mode.Off;
            RunPinAnimation(mode);
            ChangeStateTo(mode);
            MoveInstant();
        }

        private void SaveState()
        {
            CustomPlayerPrefs.SetBool(value, mode == Mode.On ? true : false);
        }

        public void ChangeState()
        {
            ChangeStateTo(mode == Mode.On ? Mode.Off : Mode.On);
        }

        private void ChangeStateTo(Mode newMode)
        {
            if (mode != newMode || isInitialisationState)
            {
                mode = newMode;
                RunPinAnimation(mode);
                switch (mode)
                {
                    case Mode.On:
                        positionPin = positionPinDefault;
                        break;
                    case Mode.Off:
                        positionPin = -positionPinDefault;
                        break;
                }

                SaveState();
                if (!isInitialisationState)
                    if (valueChanged != null)
                        valueChanged.Invoke();

                isInitialisationState = false;
            }
        }

        #endregion

        #region Animation

        private void RunPinAnimation(Mode withMode)
        {
            switch (withMode)
            {
                case Mode.On:
                    pinAnimation.clip = pinAnimation.GetClip("SwitchPin_AnimationFadeOn");
                    break;
                case Mode.Off:
                    pinAnimation.clip = pinAnimation.GetClip("SwitchPin_AnimationFadeOff");
                    break;
            }
            pinAnimation.Play();
        }

        private void MovePin()
        {
            pin.transform.localPosition = Vector3.MoveTowards(pin.transform.localPosition,
                new Vector3(positionPin, 0.0f, 0.0f),
                Time.deltaTime * speed);
        }

        private void MoveInstant()
        {
            pin.transform.localPosition = new Vector3(positionPin, 0.0f, 0.0f);
        }

        #endregion
    }
}