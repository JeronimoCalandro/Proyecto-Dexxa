using UnityEngine;
using UnityEngine.UI;

namespace Bitel.PlanetaParacaidas.Map
{
    public class LevelButton : MonoBehaviour
    {

        public Sprite ActiveButtonSprite;
        public Sprite LockedButtonSprite;
        public Button button;
        public Text numberText;
        public bool Interactable { get; private set; }

        /// <summary>
        /// Set button interactable if button "active" or appropriate level is passed. Show stars or Lock image
        /// </summary>
        /// <param name="active"></param>
        /// <param name="activeStarsCount"></param>
        /// <param name="isPassed"></param>
        internal void SetActive(bool active, int activeStarsCount, bool isPassed)
        {

            button.image.sprite = (isPassed || active) ? ActiveButtonSprite : LockedButtonSprite;



            Interactable = active || isPassed;
            button.interactable = Interactable;
            if (active)
            {
                MapController.Instance.ActiveButton = this;
            }

        }
    }

}

