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

        protected int _cardNumber1 = 0;
        protected int _cardNumber2 = 0;


        [SerializeField]
        protected CardPackConfiguration[] _packs;
        [SerializeField]
        protected Card _cardPrefab;



        protected int _countCardInDeck = 30;
        protected bool _isPlayer1Turn = true;

        protected void Awake()
        {
            Self = this;
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();

            foreach (var pack in _packs) cards = pack.UnionProperties(cards);

            _allCards = new List<CardPropertiesData>(cards);

            _baseMaterial = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMaterial.renderQueue = 2990;

            _deck1 = new Card[30];
            _deck2 = new Card[30];
        }

        public bool GetIsPlayer1Turn()
        {
            return _isPlayer1Turn;
        }
        public int GetCardNumber1() =>  _cardNumber1;

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
                //if(_cardNumber1 == 29) StartCardManager.Self._messageText.text = "The deck is full. Second player's turn";
                 
                _isPlayer1Turn = false;
            }
            else
            {
                _deck1[i] = card;
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

        private Card[] CreatwDeck(Transform parent) // заглушка, пока колоды не сформированы
        {
            var deck = new Card[_countCardInDeck];
            var offset = 0.7f;

            for (int i = 0; i < _countCardInDeck; i++)
            {
                deck[i] = Instantiate(_cardPrefab, parent);
                deck[i].transform.localPosition = new Vector3(0f, offset, 0f);
                deck[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);
                deck[i].SwitchVisual();
                offset += 0.7f;

                //по заданию тут переделать: колода уже готова, а порядок в ней рандомный
                var random = _allCards[Random.Range(0, _allCards.Count)];

                var newMaterial = new Material(_baseMaterial);
                newMaterial.mainTexture = random.Texture;

                deck[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMaterial);
            }

            return deck;
        }
    }
}
