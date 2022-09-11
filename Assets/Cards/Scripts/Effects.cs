using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class Effects : MonoBehaviour
    {
        public static Effects Self;

        private void Start()
        {
            Self = this;
        }
        public bool DealtTaunt()
        {
            var cardsInGame = FindObjectsOfType<Card>();
            List<Card> tableCard2 = new List<Card>();
            List<Card> tableCard1 = new List<Card>();
            

            foreach (var item in cardsInGame)
            {
                if (item.transform.parent.TryGetComponent<TableCard2>(out var r)) tableCard2.Add(item);
                if(item.transform.parent.TryGetComponent<TableCard1>(out var r1)) tableCard1.Add(item);
            }

            bool tauntExists2 = IsTauntExists(tableCard2);
            bool tauntExists1 = IsTauntExists(tableCard1);

            if (GameManager.Self.IsPlayer1Turn && tauntExists2) return true;
            if (!GameManager.Self.IsPlayer1Turn && tauntExists1) return true;
            return false;

        }

        private bool IsTauntExists(List<Card> tableCard)// почему некорректно работает?
        {
            bool taunt = false;
            for (int i = 0; i < tableCard.Count; i++)
            {
                if (tableCard[i].Taunt) taunt = true;
            }
            return taunt;
        }
    }
}
