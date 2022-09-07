using Cards.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cards
{

    public class CardManager : MonoBehaviour
    {
        public static CardManager Self;
        protected Material _baseMaterial;
        protected List<CardPropertiesData> _allCards;

        protected Card[] _deck1;
        protected Card[] _deck2;
        protected Card[] _heap;
        //protected Dictionary<Card, uint> _heapDictionary;
        //protected Dictionary<uint, CardPropertiesData> _heapDictionaryData;
        //protected Dictionary<Card, uint> _deck1Dictionary;
        //protected Dictionary<uint, CardPropertiesData> _deck1DictionaryData;

        protected int _cardNumber1 = 0;
        protected int _cardNumber2 = 0;

        [SerializeField]
        protected CardPackConfiguration[] _packs;
        [SerializeField]
        protected Card _cardPrefab;
        [SerializeField]
        protected int _countCardInDeck = 30;

        protected bool _isPlayer1Turn;

        protected void Awake()
        {
            Self = this;
            _isPlayer1Turn = true;
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();

            foreach (var pack in _packs) cards = pack.UnionProperties(cards);

            _allCards = new List<CardPropertiesData>(cards);

            _baseMaterial = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMaterial.renderQueue = 2990;

            _deck1 = new Card[_countCardInDeck];
            _deck2 = new Card[_countCardInDeck];
            _heap = new Card[_allCards.Count];
            //_deck1Dictionary = new Dictionary<Card, uint>(_countCardInDeck);
            //_deck1DictionaryData = new Dictionary<uint, CardPropertiesData>(_countCardInDeck);
        }

        private void LateUpdate()
        {
            _isPlayer1Turn = GameManager.Self.IsPlayer1Turn;
            Debug.Log(_isPlayer1Turn);
        }


        public int GetCardNumber1() => _cardNumber1;

        public int GetCardNumber2() => _cardNumber2;

        public void PlaceCardInDeck1(Card card, int i)
        {
            if (_cardNumber1 >= 29)
            {
                if (_cardNumber1 == 29)
                {
                    _deck1[i] = card;
                    
                    StartCardManager.Self._messageText.text = "The deck is full. Second player's turn";
                }
                StartCardManager.Self._headerPlayer1.SetActive(false);
                StartCardManager.Self._headerPlayer2.SetActive(true);

                _isPlayer1Turn = false;
            }
            else
            {
                _deck1[i] = card;
                //if (_heapDictionary.TryGetValue(card, out uint value))
                //{
                //    _deck1Dictionary[_deck1[i]] = value;

                //    if (_heapDictionaryData.TryGetValue(value, out CardPropertiesData data))
                //    {
                //        _deck1DictionaryData[value] = data;
                //    }
                //}
                _cardNumber1++;
            }
        }

        public void PlaceCardInDeck2(Card card, int i)
        {
            if (_cardNumber2 >= 29)
            {
                if (_cardNumber2 == 29)
                { 
                    StartCardManager.Self._messageText.text = "The deck is full. It's time to play!";
                    _deck2[i] = card;
                }
                StartCardManager.Self._playButton.gameObject.SetActive(true);
            }
            else
            {
                _deck2[i] = card;
                _cardNumber2++;
            }
        }

        public void StartGame() => SceneManager.LoadScene("SampleScene");
    }
}
