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

       

        public void OnPointerDown(PointerEventData eventData)
        {

            if (left) ZDGGameController.left = true;
            else ZDGGameController.right = true;

            //ZDGPlayer.turnRange = 40;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ZDGGameController.turn = false;
            ZDGGameController.left = false;
            ZDGGameController.right = false;

            ZDGGameController.turn = false;
            if (ZDGGameController.flipSound != 2 && ZDGGameController.flipSound != 0)
            {
                SoundController.instance.stopFxSound(SoundController.instance.carAudioSource);
                ZDGGameController.flipSound = 2;
            }
            //ZDGPlayer.turnRange = 0;
        }
    }
}

