using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PlayerHand1 : PlayerHand
    {
        private void Start()
        {
            _cards1 = new Card[_positions1.Length];
        }
    }
}
