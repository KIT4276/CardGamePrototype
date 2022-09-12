using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Cards;
using System.Collections;
using System;
using UnityEngine.InputSystem;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _frontCard;

        [Space, SerializeField]
        public MeshRenderer _icon;
        [SerializeField]
        private TextMeshPro _cost;
        [SerializeField]
        private TextMeshPro _name;
        [SerializeField]
        private TextMeshPro _attack;
        [SerializeField]
        private TextMeshPro _health;
        [SerializeField]
        private TextMeshPro _type;
        [SerializeField]
        private TextMeshPro _description;
        private uint _id;

        private CardPropertiesData _data;

        private Transform _landingPoint;

        public static Card Self;

        public bool IsCardSelected { get; set; }
        public bool IsCardAttacked { get; set; }

        public TextMeshPro Description { get => _description; }

        public bool IsEnable
        {
            get => _icon.enabled;
            set
            {
                _icon.enabled = value;
                _frontCard.SetActive(value);
            }
        }

        public CardStateType State { get; set; } = CardStateType.InChoise;
        public bool Taunt { get; set; }
        public bool Charge { get; set; }

        private void Start() => Self = this;

        private void Update() => CheckHealth();

        private void CheckHealth()
        {
            if (_data.Health <= 0 || _data.Health >= 100) Destroy(gameObject); // костыль
        }

        private void SelectCardOnTable()
        {
            if (GameManager.Self.IsPlayer1Turn && transform.parent.TryGetComponent<TableCard1>(out var t))
            {
                transform.localScale *= 1.5f;
                transform.position += new Vector3(0f, 2f, 0f);
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardSelected = false;
                IsCardSelected = true;
                GameManager.Self._selectedCard = this;
            }
            else if (!GameManager.Self.IsPlayer1Turn && transform.parent.TryGetComponent<TableCard2>(out var r))
            {
                transform.localScale *= 1.5f;
                transform.position += new Vector3(0f, 2f, 0f);
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardSelected = false;
                IsCardSelected = true;
                GameManager.Self._selectedCard = this;
            }
            else
            {
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardAttacked = false;

                IsCardAttacked = true;

                GameManager.Self._attackedCard = this;
                GameManager.Self.StartJoinTheFight(this.transform);

                StartCoroutine(OnAttack());
            }
        }

        private IEnumerator OnAttack()
        {
            yield return new WaitForSeconds(3f);
            var isTauntExists = Effects.Self.DealtTaunt();

            if (isTauntExists && Taunt || !isTauntExists)
            {
                _data.Health -= GameManager.Self._selectedCard._data.Attack;
                _health.text = _data.Health.ToString();

                GameManager.Self.ChangePlayersTurn();
            }
            else Debug.Log("-----------------Бить можно только по таунту!");

            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    if (other.gameObject.TryGetComponent<TableCard1>(out TableCard1 comp1) && GameManager.Self.IsPlayer1Turn)
                    {
                        _landingPoint = other.transform;
                    }
                    if (other.gameObject.TryGetComponent<TableCard2>(out TableCard2 comp2) && !GameManager.Self.IsPlayer1Turn)
                    {
                        _landingPoint = other.transform;
                    }
                    break;
                case CardStateType.OnTable:
                    // todo
                    break;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Card>(out Card card)) Debug.Log(card._health);
        }

        public void Configuration(CardPropertiesData data, string description, Material image, uint id)
        {
            _icon.sharedMaterial = image;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _description.text = description;
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _attack.text = data.Attack.ToString();
            _health.text = data.Health.ToString();
            _data = data;
            Taunt = description.Contains("Taunt");
            Charge = description.Contains("Charge");
            _id = data.Id;
        }

        public void OnDrag(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    var hitPos = eventData.pointerCurrentRaycast.worldPosition;
                    transform.position = new Vector3(hitPos.x, transform.position.y, hitPos.z);
                    break;
                case CardStateType.OnTable:
                    var hitPosT = eventData.pointerCurrentRaycast.worldPosition;
                    transform.position = new Vector3(hitPosT.x, transform.position.y, hitPosT.z);
                    break;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.position = new Vector3(_landingPoint.position.x, _landingPoint.position.y + 2, _landingPoint.position.z);
                    transform.parent = _landingPoint;
                    State = CardStateType.OnTable;

                    if(GameManager.Self.IsPlayer1Turn) PlayerHand1.Self.RemovingCardFromArray(this);
                    else PlayerHand2.Self.RemovingCardFromArray(this);

                    if (Description.text.Contains("Restore 2 Health"))
                    {
                        if(GameManager.Self.IsPlayer1Turn) Player1.Self.SetHalth(Player1.Self.GetHalth() + 2);
                        else Player2.Self.SetHalth(Player2.Self.GetHalth() + 2);
                        Debug.Log("Restore 2 Health");
                    }
                    if (!Description.text.Contains("Charge")) GameManager.Self.ChangePlayersTurn();

                    Effects.Self.CheckBattlecry(this);

                    break;
                case CardStateType.OnTable:
                    transform.position = _landingPoint.position;
                    transform.parent = _landingPoint;
                    State = CardStateType.OnTable;
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InDeck: 
                    break;
                case CardStateType.InHand:
                    transform.localScale *= 1.5f;
                    transform.position += new Vector3(0f, 2f, 0f);
                    break;
                case CardStateType.OnTable:
                    break;
                case CardStateType.InChoise:
                    transform.localScale *= 1.5f;
                    transform.position += new Vector3(0f, 1f, -7f);
                    break;
            }

            Debug.Log(State);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InDeck: 
                    break;
                case CardStateType.InHand:
                    transform.localScale /= 1.5f;
                    transform.position -= new Vector3(0f, 2f, 0f);
                    break;
               case CardStateType.OnTable:
                    break;
                case CardStateType.InChoise:
                    transform.localScale /= 1.5f;
                    transform.position -= new Vector3(0f, 1f, -7f);
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.OnTable:
                    SelectCardOnTable();
                    break;
                case CardStateType.InChoise:
                    var id = _id;
                    if (CardManager.Self.GetIsPlayer1Turn()) CardManager.Self.PlaceCardInDeck1(CardManager.Self.GetCardNumber1(), id);
                    else CardManager.Self.PlaceCardInDeck2(CardManager.Self.GetCardNumber2(), id);
                    break;
                default:
                    break;
            }
        }

        [ContextMenu("Switch Visual")]
        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }

        public CardPropertiesData GetData() => _data;

        public string GetName() => _name.text;
    }
}
