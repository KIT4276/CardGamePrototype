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
        
        //private Material _baseMaterial;

        //private CardPropertiesData[] _allCommonCards;
        //private CardPropertiesData[] _allMagCards;
        //private CardPropertiesData[] _allWarriorCards;
        //private CardPropertiesData[] _allPriestCards;
        //private CardPropertiesData[] _allHunterCards;

        private List<Card> _deckPlayer1;
        private List<Card> _deckPlayer2;

        private Card[] _heap;
        [Space]
        public GameObject _headerPlayer1;
        public GameObject _headerPlayer2;
        [Space]
        public Text _messageText;
        [Space]
        public Button _playButton;

        //[SerializeField]
        //private CardPackConfiguration[] _packs;
        //[SerializeField]
        //private Card _cardPrefab;
        [SerializeField]
        private Transform _parent;

        [Space, SerializeField]
        private Transform[] _positions;

        [Space]
        public Text _LeftChooseCardsCount1;

        //[SerializeField]
        //private Text _LeftChooseCardsCount2;

        //private void Awake()
        //{
        //    _deckPlayer1 = new List<Card>();
        //}
        

        private void Start()
        {
            Self = this;
            _deckPlayer1 = new List<Card>();
            //Debug.Log(_allCards.Count);

            _heap = CreateCardsHip();

        }

        private void LateUpdate()
        {
            if(_isPlayer1Turn)  _LeftChooseCardsCount1.text = (30 - _cardNumber1).ToString();
            if (!_isPlayer1Turn)
            {
                _LeftChooseCardsCount1.text = (30 - _cardNumber2).ToString();
            }
        }

        

        public Card[] CreateCardsHip()
        {
            var heap = new Card[_allCards.Count];

            for (int i = 0, j = 0; i < _allCards.Count; i++, j++)
            {

                heap[i] = Instantiate(_cardPrefab, _positions[j]);
                heap[i].transform.position = _positions[j].position;
                heap[i].transform.eulerAngles = new Vector3(-90f,180f,0f);
                heap[i].transform.localScale = new Vector3(630f,9f,900f);

                var random = _allCards[Random.Range(0, _allCards.Count)];

                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = random.Texture;

                heap[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMaterial);
            }
            return heap;
        }

        //private void ArrangeCards()
        //{
        //    foreach (var item in heap)
        //    {

        //    }
        //}

        //private void FormationMageCards()
        //{
        //    _allMagCards = new CardPropertiesData[6];




        //    IEnumerable<CardPropertiesData> Magcards = new List<CardPropertiesData>();
        //    foreach (var pack in _packs) Magcards = pack.UnionProperties(Magcards);

        //    _allMagCards = new List<CardPropertiesData>(Magcards);

        //    _baseMaterial = new Material(Shader.Find("TextMeshPro/Sprite"));
        //    _baseMaterial.renderQueue = 2990;
        //}
    }
}
