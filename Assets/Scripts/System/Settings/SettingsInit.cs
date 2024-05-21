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
    public class SettingsInit : MonoBehaviour
    {
        //Values
        public int targetFrameRate = 300;
        public bool cleanAllValues = false;

        #region Standart system methods

        void Awake()
        {
            ClearAll();
            RecogniseDeviceType();
            SetTargetFrameRate();
        }

        #endregion

        #region Settings initialise

        private void RecogniseDeviceType()
        {
            float width = Screen.safeArea.width;
            float height = Screen.safeArea.height;
            float proportion = height > width ? height / width : width / height;

            if (proportion <= 1.5)
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPad);
            else if (proportion <= 1.8)
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPhoneClassic);
            else
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPhoneModern);

            CustomPlayerPrefs.Save();
        }

        private void SetTargetFrameRate()
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 1;
        }

        private void ClearAll()
        {
            if (cleanAllValues)
                PlayerPrefs.DeleteAll();
        }

        #endregion
    }
}