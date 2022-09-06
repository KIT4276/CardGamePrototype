using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        protected Card[] _cards;

        [SerializeField]
        protected Transform[] _positions;

        protected Vector3 _startPos;
        protected Vector3 _intermedPos;
        protected Vector3 _startRot;
        protected Vector3 _endRot;

        protected void Awake()
        {
            _cards = new Card[_positions.Length];
            _endRot = new Vector3(0f, 0f, 180f);
        }

        public bool SetNewCard(Card card)
        {
            var result = GetLastPos();

            if (result == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            _cards[result] = card;

            _startRot = card.transform.eulerAngles;
            _startPos = card.transform.position;
            _intermedPos = new Vector3(card.transform.position.x, card.transform.position.y + 100, card.transform.position.z);

            StartCoroutine(MoveCardUp(card));
            StartCoroutine(RotateCard(card));
            StartCoroutine(SwitchVisualCorutine(card));
            StartCoroutine(MoveCardInHand(card, _positions[result]));

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
                card.transform.eulerAngles = Vector3.Lerp(_startRot, _endRot, time);
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
            var endPos = parent.position;

            yield return new WaitForSeconds(1.7f);

            while (time < 3f)
            {
                card.transform.position = Vector3.Lerp(_intermedPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            card.transform.parent = parent;
            card.State = CardStateType.InHand;
        }

        private int GetLastPos()
        {
            for(int i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] == null) return i;
            }

            return -1;
        }
    }
}
