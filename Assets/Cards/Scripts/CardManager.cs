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
        public uint[] _deck1Id;
        protected Card[] _deck2;
        public uint[] _deck2Id;
        protected Card[] _heap;

        protected int _cardNumber1 = 0;
        protected int _cardNumber2 = 0;

        [SerializeField, Tooltip("All Scriptble Objects of cards")]
        protected CardPackConfiguration[] _packs;
        [SerializeField]
        protected Card _cardPrefab;
        [SerializeField]
        protected int _countCardInDeck = 30;

        protected bool _isPlayer1Turn;

        public bool GetIsPlayer1Turn() => _isPlayer1Turn;
        public int GetCountCardInDeck() => _countCardInDeck;
        public int GetCardNumber1() => _cardNumber1;
        public int GetCardNumber2() => _cardNumber2;

        private void Awake()
        {
            Self = this;
            _isPlayer1Turn = true;
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();

            foreach (var pack in _packs) cards = pack.UnionProperties(cards);

            _allCards = new List<CardPropertiesData>(cards);

            _baseMaterial = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMaterial.renderQueue = 2990;
        }

        private void LateUpdate() => _isPlayer1Turn = GameManager.Self.IsPlayer1Turn;

        public void PlaceCardInDeck1(int i, uint id)
        {
            if (_cardNumber1 >= 29)
            {
                if (_cardNumber1 == 29)
                {
                    _deck1Id[i] = id;
                    StartGameManager.Self.Deck1Id[i] = id;
                    StartCardManager.Self._messageText.text = "The deck is full. Second player's turn";
                }
                StartCardManager.Self._headerPlayer1.SetActive(false);
                StartCardManager.Self._headerPlayer2.SetActive(true);

                _isPlayer1Turn = false;
            }
            else
            {
                _deck1Id[i] = id;
                StartGameManager.Self.Deck1Id[i] = id;
                _cardNumber1++;
            }
        }

        public void PlaceCardInDeck2(int i, uint id)
        {
            if (_cardNumber2 >= 29)
            {
                if (_cardNumber2 == 29)
                { 
                    StartCardManager.Self._messageText.text = "The deck is full. It's time to play!";
                    _deck2Id[i] = id;
                    StartGameManager.Self.Deck2Id[i] = id;
                }
                StartCardManager.Self._playButton.gameObject.SetActive(true);
            }
            else
            {
                _deck2Id[i] = id;
                StartGameManager.Self.Deck2Id[i] = id;
                _cardNumber2++;
            }
        }

        public void StartGame() => SceneManager.LoadScene("SampleScene");

        public List<CardPropertiesData> GetAllCards() => _allCards;
    }
}
