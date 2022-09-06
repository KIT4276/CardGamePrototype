using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Panel1 : PanelManager
    {
        public static Panel1 Self;

        private void Start() => Self = this;
        public void MovePanel1() => StartCoroutine(MoveFromTo(new Vector3(0f, 150f, 100f)));
    }
}
