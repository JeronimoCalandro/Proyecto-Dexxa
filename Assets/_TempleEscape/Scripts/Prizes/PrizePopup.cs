using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Bitel.PlanetaEscape.Game
{
    public class PrizePopup : MonoBehaviour
    {
        public GameObject fireworkParticles;
        public GameObject effects;
        public List<GameObject> prizeSlots;

        private Sprite lastSprite;
        public void ActivateFireworks()
        {
            Vector3 particlesPosR = effects.transform.position + new Vector3(Random.Range(2f, 3f), Random.Range(1f, 2f), 0);
            GameObject particlesR = Instantiate(fireworkParticles, particlesPosR, Quaternion.identity) as GameObject;
            particlesR.transform.parent = effects.transform;

            Vector3 particlesPosL = effects.transform.position + new Vector3(Random.Range(-2f, -3f), Random.Range(1f, 2f), 0);
            GameObject particlesL = Instantiate(fireworkParticles, particlesPosL, Quaternion.identity) as GameObject;
            particlesL.transform.parent = effects.transform;
        }

        public void OnFinished()
        {
            GameObject currentSlot = AvailableSlot();
            if (currentSlot != null)
                currentSlot.GetComponent<Image>().sprite = lastSprite;
            gameObject.SetActive(false);
            foreach (Transform child in effects.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetImage(Sprite img)
        {
            lastSprite = img;
            GetComponent<Image>().sprite = img;
        }

        public GameObject AvailableSlot()
        {
            GameObject toReturn = null;
            foreach (GameObject go in prizeSlots)
            {
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                    return go;
                }
            }

            return toReturn;
        }
    }
}