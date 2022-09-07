using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Self;
        public bool IsPlayer1Turn { get; set; }


        [SerializeField]
        private Button _buttpn;
        [SerializeField]
        private Transform _cameraAxis;
        [SerializeField]
        private Transform _player1;
        [SerializeField]
        private Transform _player2;
        [SerializeField]
        private Transform[] _allCardsInGame;

        private void Awake()
        {
            Self = this;
            IsPlayer1Turn = true;
        }

        public void CangePlayersTurn()
        {
            if (IsPlayer1Turn) IsPlayer1Turn = false;
            else IsPlayer1Turn = true;

            StartCoroutine(CangeCameraAngle());
            _buttpn.interactable = true;
        }

        private IEnumerator CangeCameraAngle()
        {
            var time = 0f;
            var cameraEndRot = new Vector3(0f, 180f, 0f);
            var playerEndRot = new Vector3(0f, 0f, 0f);

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
    }
}
