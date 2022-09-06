using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Player1 : PlayerManager
    {
        private void Start()
        {
            //_sideType = Panel1.Self.GetSideType();
            Debug.Log("Player1 is " + _sideType);
        }
    }
}
