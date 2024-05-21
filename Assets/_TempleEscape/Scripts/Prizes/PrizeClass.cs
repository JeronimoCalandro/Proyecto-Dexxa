using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bitel.PlanetaEscape.Game
{
    public class PrizeClass
    {
        public enum PrizeType
        { MB, Life, Min };
        public PrizeType prizeType;
        public int value;
        public int serviceId;
        public int spriteIndex;

        public PrizeClass(PrizeType PrizeType, int Value, int servId, int sprite)
        {
            prizeType = PrizeType;
            value = Value;
            serviceId = servId;
            spriteIndex = sprite;
        }

        public bool Equals(PrizeClass pc)
        {
            bool r = true;

            if (serviceId != pc.serviceId)
                r = false;

            return r;
        }
    }
}
