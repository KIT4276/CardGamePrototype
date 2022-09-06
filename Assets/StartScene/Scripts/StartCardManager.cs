using Cards.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class StartCardManager : CardManager
    {
        public static StartCardManager Self;

        private List<Card> _deckPlayer1;
        private List<Card> _deckPlayer2;

        [Space]
        public GameObject _headerPlayer1;
        public GameObject _headerPlayer2;
        [Space]
        public Text _messageText;
        [Space]
        public Button _playButton;

        [Space, SerializeField]
        private Transform _parent;

        [Space, SerializeField]
        private Transform[] _positions;

        [Space]
        public Text _LeftChooseCardsCount1;

        private void Start()
        {
            Self = this;
            _deckPlayer1 = new List<Card>();
            _heap = CreateHip();

            //_heapDictionaryData = new Dictionary<uint, CardPropertiesData>();
            //_heapDictionary = CreateCardsDictionaryHipData();
        }

        
        private void LateUpdate()
        {
            if (_isPlayer1Turn) _LeftChooseCardsCount1.text = (30 - _cardNumber1).ToString();
            if (!_isPlayer1Turn) _LeftChooseCardsCount1.text = (30 - _cardNumber2).ToString();
        }

        // мб к этому вернусь
        //public Dictionary<Card, uint> CreateCardsDictionaryHip()
        //{
        //    var HipDictionary = new Dictionary<Card, uint>();

        //    for (int i = 0, j = 0; i < _allCards.Count; i++, j++)
        //    {
        //        _heap[i] = Instantiate(_cardPrefab, _positions[j]);
        //        _heap[i].transform.position = _positions[j].position;
        //        _heap[i].transform.eulerAngles = new Vector3(-90f, 180f, 0f);
        //        _heap[i].transform.localScale = new Vector3(630f, 9f, 900f);

        //        var randomCard = _allCards[Random.Range(0, _allCards.Count)];

        //        var newMaterial = new Material(_baseMaterial);
        //        newMaterial.mainTexture = randomCard.Texture;

        //        _heap[i].Configuration(randomCard, CardUtility.GetDescriptionById(randomCard.Id), newMaterial);

        //        HipDictionary[_heap[i]] = randomCard.Id;
        //    }
        //    return HipDictionary;
        //}

        //public Dictionary<Card, uint> CreateCardsDictionaryHipData()
        //{
        //    var heap = new Card[_allCards.Count];
        //    var HipDictionary = new Dictionary<Card, uint>();

        //    for (int i = 0, j = 0; i < _allCards.Count; i++, j++)
        //    {

        //        heap[i] = Instantiate(_cardPrefab, _positions[j]);
        //        heap[i].transform.position = _positions[j].position;
        //        heap[i].transform.eulerAngles = new Vector3(-90f, 180f, 0f);
        //        heap[i].transform.localScale = new Vector3(630f, 9f, 900f);

        //        var randomCard = _allCards[Random.Range(0, _allCards.Count)];

        //        var newMaterial = new Material(_baseMaterial);
        //        newMaterial.mainTexture = randomCard.Texture;

        //        heap[i].Configuration(randomCard, CardUtility.GetDescriptionById(randomCard.Id), newMaterial);

        //        HipDictionary[heap[i]] = randomCard.Id;
        //        //_heapDictionaryData[HipDictionary[heap[i]]] = randomCard;

        //    }

        //    return HipDictionary;
        //}

        private Card[] CreateHip()
        {
            var hip = new Card[_allCards.Count];
            var heap = new Card[_allCards.Count];

            for (int i = 0, j = 0; i < _allCards.Count; i++, j++)
            {

                heap[i] = Instantiate(_cardPrefab, _positions[j]);
                heap[i].transform.position = _positions[j].position;
                heap[i].transform.eulerAngles = new Vector3(-90f, 180f, 0f);
                heap[i].transform.localScale = new Vector3(630f, 9f, 900f);

                var randomCard = _allCards[Random.Range(0, _allCards.Count)];

                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = randomCard.Texture;

                heap[i].Configuration(randomCard, CardUtility.GetDescriptionById(randomCard.Id), newMaterial);
            }
            return hip;
        }
    }
}
