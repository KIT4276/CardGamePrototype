using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Self;
        public bool IsPlayer1Turn { get; set; }
        
        [SerializeField]
        private Transform _cameraAxis;
        [SerializeField]
        private Transform _player1;
        [SerializeField]
        private Transform _player2;
        [Space, SerializeField]
        private Transform[] _allCardsInGame;
        [SerializeField]
        private GameObject[] _player1Hand;
        [SerializeField]
        private GameObject[] _player2Hand;

        private Vector3 _turn1CameraEulerAngles;
        private Vector3 _turn2CameraEulerAngles;
        private Vector3 _turn1PlayerEulerAngles;
        private Vector3 _turn2PlayerEulerAngles;

        public Card _selectedCard;
        public Card _attackedCard;

        private void Awake()
        {
            Self = this;
            IsPlayer1Turn = true;
            CardManager.Self._deck1Id = StartGameManager.Self.Deck1Id;
            CardManager.Self._deck2Id = StartGameManager.Self.Deck2Id;
        }

        private void Start()
        {
            _turn1CameraEulerAngles = new Vector3(0f, 0f, 0f);
            _turn2CameraEulerAngles = new Vector3(0f, 180f, 0f);
            _turn1PlayerEulerAngles = new Vector3(0f, 180f, 0f);
            _turn2PlayerEulerAngles = new Vector3(0f, 0f, 0f);
        }

        public void ChangePlayersTurn()
        {
            if (IsPlayer1Turn)
            {
                foreach (var card in _player2Hand) card.SetActive(true);
                foreach (var card in _player1Hand) card.SetActive(false);
                IsPlayer1Turn = false;
                StartCoroutine(ChangeCameraAngle(_turn2CameraEulerAngles, _turn2PlayerEulerAngles));
            }
            else
            {
                foreach (var card in _player1Hand) card.SetActive(true);
                foreach (var card in _player2Hand) card.SetActive(false);
                IsPlayer1Turn = true;
                StartCoroutine(ChangeCameraAngle(_turn1CameraEulerAngles, _turn1PlayerEulerAngles));
            }
        }

        private IEnumerator ChangeCameraAngle(Vector3 cameraEndRot, Vector3 playerEndRot)
        {
            var time = 0f;

            while (time < 6f)
            {
                _cameraAxis.eulerAngles = Vector3.Lerp(_cameraAxis.eulerAngles, cameraEndRot, time);
                _player1.eulerAngles = Vector3.Lerp(_player1.eulerAngles, playerEndRot, time);
                _player2.eulerAngles = Vector3.Lerp(_player2.eulerAngles, playerEndRot, time);
                foreach (var card in _allCardsInGame)
                {
                    card.eulerAngles = Vector3.Lerp(card.eulerAngles, cameraEndRot, time);
                }
                time += Time.deltaTime;
                yield return null;
            }
        }

        public void StartJoinTheFight(Transform target) => StartCoroutine(JoinTheFight(target));
        private IEnumerator JoinTheFight(Transform target)
        {
            var time = 0f;
            var endPos = target.position;
            _selectedCard.transform.localScale /= 1.5f;
            _selectedCard.transform.position -= new Vector3(0f, 2f, 0f);
            var startPos = _selectedCard.transform.position;

            while (time < 3f)
            {
                _selectedCard.transform.position = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;

            while (time < 1.5f)
            {
                _selectedCard.transform.position = Vector3.Lerp(endPos, startPos, time);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
