using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Player2 : PlayerManager
    {
        private void Awake()
        {
            _sideType = Panel2.Self.GetSideType();
            Debug.Log("Player2 is " + _sideType);
        }
    }
}
