﻿using UnityEngine;
using UnityEngine.UI;

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

        private Card[] _gameDeck1;

        private void Start()
        {
            _deck1 = CreateDeck(_deck1Parent);
            _deck2 = CreateDeck(_deck2Parent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isPlayer1Turn) SetNewCardInHand(_deck1);
                else SetNewCardInHand(_deck2);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameManager.Self.CangePlayersTurn();
            }
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

        private Card[] CreateDeck(Transform parent) // пока не придумала, как предавать выбранную колоу, в игре они - из случайных карт
        {
            var deck = new Card[_countCardInDeck];
            var offset = 0.7f;

            for (int i = 0; i < _countCardInDeck; i++)
            {
                deck[i] = Instantiate(_cardPrefab, parent);
                deck[i].transform.localPosition = new Vector3(0f, offset, 0f);
                deck[i].transform.eulerAngles = parent.eulerAngles;
                deck[i].SwitchVisual();
                offset += 0.7f;

                var randomCard = _allCards[Random.Range(0, _allCards.Count)];

                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = randomCard.Texture;

                deck[i].Configuration(randomCard, CardUtility.GetDescriptionById(randomCard.Id), newMaterial);

                //if (_deck1Dictionary.TryGetValue(random, out uint value))
                //{
                //    Id = value;
                //    if (_deck1DictionaryData.TryGetValue(value, out CardPropertiesData data))
                //    {
                //        cardData = data;
                //    }
                //}
                //newMaterial.mainTexture = cardData.Texture;

                //deck[i].Configuration(cardData, CardUtility.GetDescriptionById(Id), newMaterial);
            }
            return deck;
        }
    }
}
