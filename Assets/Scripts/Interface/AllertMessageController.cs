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
    public class AllertMessageController : MonoBehaviour
    {
        //Components
        private GameObject canvas;
        private Text textTitle;
        private Text textContext;

        #region Standart system methods

        void Awake()
        {
            //Get components
            canvas = GameObject.Find("CanvasAllert");
            textTitle = canvas.transform.Find("Panel").Find("TextTitle").GetComponent<Text>();
            textContext = canvas.transform.Find("Panel").Find("TextContext").GetComponent<Text>();
        }

        #endregion

        #region State

        /// <summary>
        /// Call to show allert message
        /// </summary>
        /// <param name="title"> Title</param>
        /// <param name="context"> Context</param>
        public void ShowWithTitle(string title, string context)
        {
            textTitle.text = title;
            textContext.text = context;

            canvas.SetActive(true);
        }

        /// <summary>
        /// Hide allert
        /// </summary>
        public void Hide()
        {
            canvas.SetActive(false);
        }

        #endregion
    }
}