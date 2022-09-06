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
                for (int i = _deck1.Length - 1; i >= 0; i--)
                {
                    if (_deck1[i] == null) continue;

                    _playerHand1.SetNewCard(_deck1[i].GetComponent<Card>());
                    _deck1[i] = null;
                    break;
                }
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
                deck[i].transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
