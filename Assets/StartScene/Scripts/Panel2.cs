using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Panel2 : PanelManager
    {
        public static Panel2 Self;

        private void Start() => Self = this;

        public void MovePanel2() => StartCoroutine(MoveFromTo(new Vector3(0f, 0f, 100f)));

        public void MoveOutPanel2() => StartCoroutine(MoveFromTo(new Vector3(0f, -150f, 100f)));
    }
}
