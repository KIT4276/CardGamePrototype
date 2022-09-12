using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PlayerHand1 : PlayerHand
    {
        public static PlayerHand1 Self;

        private void Start()
        {
            Self = this;
            _cards1 = new Card[_positions1.Length];
        }

        public void RemovingCardFromArray(Card card) 
        {
            for (int i = 0; i < _cards1.Length; i++)
            {
                if (_cards1[i] == card)
                {
                    _cards1[i] = null;
                    break;
                }
            }
        }
    }
}
