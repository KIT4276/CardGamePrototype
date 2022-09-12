using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class Effects : MonoBehaviour
    {
        [SerializeField]
        protected Card _cardPrefab;

        public static Effects Self;

        private void Start() => Self = this;

        private bool IsTauntExists(List<Card> tableCard)
        {
            bool taunt = false;
            for (int i = 0; i < tableCard.Count; i++)
            {
                if (tableCard[i].Taunt) taunt = true;
            }
            return taunt;
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

        public void CheckBattlecry(Card card)
        {
            if (card.GetName() == "Murloc Tidehunter") //     полетит ли?         NO
            {
                var murlocScoutProp = SummonMurlocScout();
                var parent = card.transform.parent;

                var murloc = Instantiate(_cardPrefab, parent);
                murloc.transform.SetParent(parent);// мурлок будет в том же родителе 
                murloc.transform.position = card.transform.position - new Vector3(0f,0f,100f); // тут мурлок встанет на вызвавшую его карту, но немного ниже. будет странно
                murloc.transform.eulerAngles = card.transform.eulerAngles;
                murloc.GetComponent<Card>().SwitchVisual();

                var newMaterial = new Material(murloc.GetComponent<Card>()._icon.GetComponent<Shader>());// что-то пойдёт не так
                newMaterial.mainTexture = murlocScoutProp.Texture;

                murloc.GetComponent<Card>().Configuration(murlocScoutProp, CardUtility.GetDescriptionById(murlocScoutProp.Id), newMaterial, murlocScoutProp.Id);
                murloc.GetComponent<Card>().State = CardStateType.OnTable;
            }
        }

        public CardPropertiesData SummonMurlocScout()
        {
            CardPropertiesData murlocScout = CardManager.Self.GetAllCards()[0];
            foreach (var card in CardManager.Self.GetAllCards())
            {
                if(card.Type == CardUnitType.Murloc && card.Attack == 1 && card.Health == 1)
                {
                    murlocScout = card;
                }
            }

            return murlocScout;
        }
    }
}
