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

namespace BitelPlanetaParacaidasGame
{
    public class ImageDeviceManagerUI : MonoBehaviour
    {
        //Components
        private Image image;

        //Values
        public Sprite spriteIPhoneClassic;
        public Sprite spriteIPhoneModern;
        public Sprite spriteIPad;

        #region Standart system methods

        void Start()
        {
            //Get components
            image = GetComponent<Image>();

            //Set values
            SetCorrentImage();
        }

        #endregion

        #region Work with object

        private void SetCorrentImage()
        {
            SettingsGlobal.DeviceType deviceType = SettingsGlobal.GetCurrentDeviceType();

            switch (deviceType)
            {
                case SettingsGlobal.DeviceType.iPhoneClassic:
                    image.sprite = spriteIPhoneClassic;
                    break;
                case SettingsGlobal.DeviceType.iPhoneModern:
                    image.sprite = spriteIPhoneModern;
                    break;
                case SettingsGlobal.DeviceType.iPad:
                    image.sprite = spriteIPad;
                    break;
            }
        }

        #endregion
    }
}