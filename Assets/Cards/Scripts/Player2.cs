using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Player2 : PlayerManager
    {
        private bool _thisIsTheEnd;

        private void Awake() => _sideType = Panel2.Self.GetSideType();

        private void Update()
        {
            _thisIsTheEnd = GameManager.Self.CheckHealth(_halth);
            if (_thisIsTheEnd) Debug.Log("Player 2 lost");
        }
    }
}
