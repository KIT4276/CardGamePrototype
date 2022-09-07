using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        protected Card[] _cards1;
        protected Card[] _cards2;

        [SerializeField]
        protected Transform[] _positions1;
        [SerializeField]
        protected Transform[] _positions2;

        protected Vector3 _startPos;
        protected Vector3 _intermedPos;
        protected Vector3 _startRot;
        protected Vector3 _endRot1;
        protected Vector3 _endRot2;

        protected void Awake()
        {
            
            _endRot1 = new Vector3(0f, 0f, 180f);
            _endRot2 = new Vector3(0f, 180f, -180f);
        }

        public bool SetNewCard(Card card)
        {
            int result;
            var cards = new Card[_positions1.Length];
            if (GameManager.Self.IsPlayer1Turn)
            {
                cards = _cards1;
                result = GetLastPos(_cards1);
            }
            else
            {
                cards = _cards2;
                result = GetLastPos(_cards2);
            }

            if (result == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            cards[result] = card;

            _startRot = card.transform.eulerAngles;
            _startPos = card.transform.position;
            _intermedPos = new Vector3(card.transform.position.x, card.transform.position.y + 100, card.transform.position.z);
            cards[result].State = CardStateType.InHand;

            StartCoroutine(MoveCardUp(card));
            StartCoroutine(RotateCard(card));
            StartCoroutine(SwitchVisualCorutine(card));

            if (GameManager.Self.IsPlayer1Turn)
            StartCoroutine(MoveCardInHand(card, _positions1[result]));
            else StartCoroutine(MoveCardInHand(card, _positions2[result]));

            return true;
        }

        private IEnumerator MoveCardUp(Card card)
        {
            var time = 0f;

            while (time < 1.5f)
            {
                card.transform.position = Vector3.Lerp(_startPos, _intermedPos, time);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator RotateCard(Card card)
        {
            var time = 0f;

            yield return new WaitForSeconds(0.5f);

            while (time < 1f)
            {
                if(GameManager.Self.IsPlayer1Turn) card.transform.eulerAngles = Vector3.Lerp(_startRot, _endRot1, time);
                else card.transform.eulerAngles = Vector3.Lerp(_startRot, _endRot2, time);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator SwitchVisualCorutine(Card card)
        {
            yield return new WaitForSeconds(1f); 

            card.SwitchVisual();
            yield return null;
        }

        private IEnumerator MoveCardInHand(Card card, Transform parent)
        {
            var time = 0f;
            var endPos = new Vector3(parent.position.x, parent.position.y+2, parent.position.z);
            card.transform.parent = parent;
            card.State = CardStateType.InHand;

            yield return new WaitForSeconds(1.7f);

            while (time < 3f)
            {
                card.transform.position = Vector3.Lerp(_intermedPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            
        }

        private int GetLastPos(Card[] cards)
        {
            for(int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == null) return i;
            }

            return -1;
        }
    }
}
