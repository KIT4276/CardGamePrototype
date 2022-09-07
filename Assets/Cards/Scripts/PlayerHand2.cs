using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PlayerHand2 : PlayerHand
    {
        private void Start()
        {
            _cards2 = new Card[_positions2.Length];
        }
    }
}
