using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using ZombieDriveGame;

namespace BitelPlanetaParacaidasGame
{
    public class ButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool left;
        public GameObject buttonToHide;
        public ZDGGameController ZDGGameController;
        public ZDGPlayer ZDGPlayer;
        public static event Action<GameObject, PointerEventData> OnPointerEnterEvent;
        public static event Action<GameObject, PointerEventData> OnPointerClickEvent;
        public static event Action<GameObject> OnPointerExitEvent;

        /*private void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
    
#endif
            if(Application.platform == RuntimePlatform.Android)
            {
                
            }

            if (Input.touchCount > 0)
            {
                var lastTouch = Input.GetTouch(Input.touchCount);
                if (lastTouch.position.x < (float)Screen.width * .5f)
                {
                    if (lastTouch.phase == TouchPhase.Began)
                    {
                        ZDGGameController.TurnLeft();
                    }
                    if (lastTouch.phase == TouchPhase.Ended)
                    {
                        ZDGGameController.turn = false;
                    }
                }
                else if (lastTouch.position.x > (float)Screen.width * .5f)
                {
                    if (lastTouch.phase == TouchPhase.Began)
                    {
                        ZDGGameController.TurnRight();
                    }
                    if (lastTouch.phase == TouchPhase.Ended)
                    {
                        ZDGGameController.turn = false;
                    }
                }
            }
                


        }*/

        public void OnPointerDown(PointerEventData eventData)
        {

            if (left) ZDGGameController.TurnLeft();
            else ZDGGameController.TurnRight();

            //ZDGPlayer.turnRange = 40;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ZDGGameController.turn = false;
            
            //ZDGPlayer.turnRange = 0;
        }
    }
}

