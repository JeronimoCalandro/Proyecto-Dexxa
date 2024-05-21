using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bitel.PlanetaEscape.Game
{
    public class PrizeInstance : MonoBehaviour
    {
        public delegate void Pickup(PrizeClass pc);
        public static event Pickup pickupEvent;
        public AudioClip pickUpSound;
        public PrizeClass prizeSettings { get; private set; }
        public MeshRenderer meshRenderer;

        //SoundManager soundManager;

        private void Awake()
        {

            //SoundManager soundSrc = SoundManager.instance;
            //if (soundSrc != null) soundManager = soundSrc;
        }


        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                //if (PrizeManager.useServices)
                //{
                    if (prizeSettings != null)
                    {
                      
                        SoundController.instance.playSound(SoundController.instance.PrizeSound, false, SoundController.instance.fxAudioSource);
                        pickupEvent?.Invoke(prizeSettings);
                        Destroy(this.gameObject);
                        Debug.Log("Prize picked up");
                    }
                //}
                //else
                //{
                //    //Testing only
                //    pickupEvent?.Invoke(prizeSettings);
                //    Destroy(this.gameObject);
                //    Debug.Log("Prize picked up");
                //}


            }
            
        }

        public void SetPrizeClass(PrizeClass pc) => prizeSettings = pc;
        public void SetTexture(Texture tex) => meshRenderer.material.mainTexture = tex;
    }
}
