﻿using System.Collections;
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


        private void Awake()
        {
            Self = this;
            IsPlayer1Turn = true;
        }
        private void Start()
        {
            _turn1CameraEulerAngles = new Vector3(0f, 0f, 0f);
            _turn2CameraEulerAngles = new Vector3(0f, 180f, 0f);
            _turn1PlayerEulerAngles = new Vector3(0f, 180f, 0f);
            _turn2PlayerEulerAngles = new Vector3(0f, 0f, 0f);
        }

        public void CangePlayersTurn()
        {
            if (IsPlayer1Turn)
            {
                foreach (var card in _player2Hand) card.SetActive(true);
                foreach (var card in _player1Hand) card.SetActive(false);
                IsPlayer1Turn = false;
                StartCoroutine(CangeCameraAngle(_turn2CameraEulerAngles, _turn2PlayerEulerAngles));
            }
            else
            {
                foreach (var card in _player1Hand) card.SetActive(true);
                foreach (var card in _player2Hand) card.SetActive(false);
                IsPlayer1Turn = true;
                StartCoroutine(CangeCameraAngle(_turn1CameraEulerAngles, _turn1PlayerEulerAngles));
            }
        }

        private IEnumerator CangeCameraAngle(Vector3 cameraEndRot, Vector3 playerEndRot)
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
    }
}
