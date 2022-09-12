﻿using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class GameCardManager : CardManager
    {
        [Space, SerializeField]
        private Transform _deck1Parent;
        [SerializeField]
        private Transform _deck2Parent;
        [SerializeField]
        private PlayerHand _playerHand1;
        [SerializeField]
        private PlayerHand _playerHand2;
        [SerializeField]
        private PlayerManager _player1;
        [SerializeField]
        private PlayerManager _player2;

        private void Start()
        {
            _deck1 = CreateDeck(_deck1Parent, _deck1Id); 
            _deck2 = CreateDeck(_deck2Parent, _deck2Id);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isPlayer1Turn) SetNewCardInHand(_deck1);
                else SetNewCardInHand(_deck2);
            }

            if (Input.GetKeyDown(KeyCode.Z)) GameManager.Self.ChangePlayersTurn();
        }

        private void SetNewCardInHand(Card[] deck)
        {
            for (int i = deck.Length - 1; i >= 0; i--)
            {
                if (deck[i] == null) continue;
           
                if(_isPlayer1Turn)_playerHand1.SetNewCard(deck[i].GetComponent<Card>());
                else _playerHand2.SetNewCard(deck[i].GetComponent<Card>());

                deck[i] = null;
                break;
            }
        }

        private Card[] CreateDeck(Transform parent, uint[] id) // рабочий метод
        {
            var deck = new Card[_countCardInDeck];
            var offset = 0.7f;

            for (int i = 0; i < _countCardInDeck; i++)
            {
                deck[i] = Instantiate(_cardPrefab, parent);
                deck[i].transform.localPosition = new Vector3(0f, offset, 0f);
                deck[i].transform.eulerAngles = parent.eulerAngles;
                deck[i].SwitchVisual();
                deck[i].State = CardStateType.InDeck;
                offset += 0.7f;

                var randomCard = _allCards[Random.Range(0, _allCards.Count)];

                foreach (var item in _allCards)
                {
                    if (item.Id == id[i]) randomCard = item;
                }

                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = randomCard.Texture;

                deck[i].Configuration(randomCard, CardUtility.GetDescriptionById(id[i]), newMaterial, id[i]);
            }

            Shuffle<Card>(deck);
            return deck;
        }

        //private Card[] CreateDeck(Transform parent, uint[] id) // для проверки игры
        //{
        //    var offset = 0.7f;
        //    var newDeck = new Card[30];

        //    for (int i = 0; i < 30; i++)
        //    {
        //        newDeck[i] = Instantiate(_cardPrefab, parent); 
        //        newDeck[i].transform.SetParent(parent);
        //        newDeck[i].transform.localPosition = new Vector3(0f, offset, 0f);
        //        newDeck[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);
        //        newDeck[i].SwitchVisual();
        //        offset += 0.7f;

        //        var random = _allCards[Random.Range(0, 29)];
        //        var newMaterial = new Material(_baseMaterial);
        //        newMaterial.mainTexture = random.Texture;

        //        newDeck[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMaterial, random.Id);
        //    }
        //    return newDeck;
        //}

        private void Shuffle<T>(IList<T> inputList) //Кирилл нашел вот такой миленький шафл :D но все равно как то странно "работает"...
        {
            for (int i = 0; i < inputList.Count - 1; i++)
            {
                T temp = inputList[i];
                int random = Random.Range(i, inputList.Count);
                inputList[i] = inputList[random];
                inputList[random] = temp;
            }
        }

    }
}
