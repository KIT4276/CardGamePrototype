using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PlayerHand2 : PlayerHand
    {
        public static PlayerHand2 Self;

        private void Start()
        {
            Self = this;
            _cards2 = new Card[_positions2.Length];
        }

        public void RemovingCardFromArray(Card card)
        {
            for (int i = 0; i < _cards2.Length; i++)
            {
                if (_cards2[i] == card)
                {
                    _cards2[i] = null;
                    break;
                }
            }
        }
    }
}
