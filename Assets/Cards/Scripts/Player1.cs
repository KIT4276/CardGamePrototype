using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Player1 : PlayerManager
    {
        private void Awake() => _sideType = Panel1.Self.GetSideType();
    }
}
