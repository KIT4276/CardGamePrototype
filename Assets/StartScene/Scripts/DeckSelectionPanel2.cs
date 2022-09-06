using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class DeckSelectionPanel2 : PanelManager
    {
        public void MoveDeckPanel2() => StartCoroutine(MoveFromTo(new Vector3(0f, 0f, 100f)));

        public void MoveOutDeckPanel2() => StartCoroutine(MoveFromTo(new Vector3(0f, -150f, 100f)));
    }
}
