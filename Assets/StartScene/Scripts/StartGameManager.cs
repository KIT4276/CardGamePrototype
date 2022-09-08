using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class StartGameManager : MonoBehaviour
    {
        public uint[] Deck1Id { get; set; }
        public uint[] Deck2Id { get; set; }

        public static StartGameManager Self;

        void Start()
        {
            Self = this;

            Deck1Id = new uint[CardManager.Self.GetCountCardInDeck()];
            Deck2Id = new uint[CardManager.Self.GetCountCardInDeck()];
        }
    }
}
