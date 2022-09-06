using System.Collections;
using System.Collections.Generic;
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

        private Card[] _randomDeck1;
        private Card[] _randomDeck2;

        private void Start()
        {
            //_deck1 = CreatwDeck(_deck1Parent);
            //_deck2 = CreatwDeck(_deck2Parent);
            _randomDeck1 = SetDeck(_deck1, _deck1Parent);
            _randomDeck2 = SetDeck(_deck2, _deck2Parent);

            //_randomDeck1 = CreatRandomDeck(_deck1, _deck1Parent);
            //_randomDeck2 = CreatRandomDeck(_deck2, _deck2Parent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = _deck1.Length - 1; i >= 0; i--)
                {
                    if (_deck1[i] == null) continue;

                    _playerHand1.SetNewCard(_deck1[i]);
                    _deck1[i] = null;
                    break;
                }
            }
        }

        private Card[] SetDeck(Card[] deck, Transform parent)
        {
            var offset = 0.7f;
            var newDeck = new Card[deck.Length];

            for (int i = 0; i < deck.Length; i++)
            {
                newDeck[i] = Instantiate(_cardPrefab, parent); // тут null
                newDeck[i].transform.SetParent(parent);
                newDeck[i].transform.localPosition = new Vector3(0f, offset, 0f);
                newDeck[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                //card.SwitchVisual();
                offset += 0.7f;

                var random = deck[Random.Range(0, 29)];
                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = random.GetComponent<CardPropertiesData>().Texture;

                newDeck[i].Configuration(random.GetComponent<CardPropertiesData>(), CardUtility.GetDescriptionById(random.GetComponent<CardPropertiesData>().Id), newMaterial);
            }
            return newDeck;
        }
    }
}
