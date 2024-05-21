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
    [System.Serializable]
    public class Counter
    {
        //Values
        public int value;
        private int valueDefault;
        private bool isValueDefaultSet = false;

        #region Work with value

        /// <summary>
        /// Perfom all processed of one dereasing step. Restore if it's reach limit.
        /// </summary>
        /// <returns>It's reach limit.</returns>
        public bool PerfomAndCheck()
        {
            Decrease();
            bool isReady = IsReady();
            if (isReady)
                Restore();
            return isReady;
        }

        /// <summary>
        /// Decrease value.
        /// </summary>
        public void Decrease()
        {
            TryToSetDefault();
            value--;
        }

        /// <summary>
        /// Check is reach 0.
        /// </summary>
        /// <returns>Is reach limit</returns>
        public bool IsReady()
        {
            return value <= 0;
        }

        /// <summary>
        /// Restore value to default.
        /// </summary>
        public void Restore()
        {
            value = valueDefault;
        }

        #endregion

        #region State

        /// <summary>
        /// If it's possible set delault value.
        /// </summary>
        private void TryToSetDefault()
        {
            if (!isValueDefaultSet)
            {
                isValueDefaultSet = true;
                valueDefault = value;
            }
        }

        #endregion
    }
}