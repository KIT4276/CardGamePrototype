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
        private MeshRenderer _icon;
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
        public bool Rush { get; set; }

        private void Update() => CheckHealth();

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
            Rush = description.Contains("Rush");
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

        private void SelectCardOnTable()
        {
            if (GameManager.Self.IsPlayer1Turn && transform.parent.TryGetComponent<TableCard1>(out var t))
            {
                Debug.Log("Attack=" + _data.Attack);
                transform.localScale *= 1.5f;
                transform.position += new Vector3(0f, 2f, 0f);
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardSelected = false;
                IsCardSelected = true;
                GameManager.Self._selectedCard = this;
                Debug.Log(_name.text + " " + "Selected " + IsCardSelected);
            }
            else if(!GameManager.Self.IsPlayer1Turn && transform.parent.TryGetComponent<TableCard2>(out var r))
            {
                Debug.Log("Attack=" + _data.Attack);
                transform.localScale *= 1.5f;
                transform.position += new Vector3(0f, 2f, 0f);
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardSelected = false;
                IsCardSelected = true;
                GameManager.Self._selectedCard = this;
                Debug.Log(_name.text + " " + "Selected " + IsCardSelected);
            }
            else
            {
                var cards = FindObjectsOfType<Card>();
                foreach (var card in cards) card.IsCardAttacked = false;

                IsCardAttacked = true;

                GameManager.Self._attackedCard = this;
                GameManager.Self.StartJoinTheFight();

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
                if (_data.Health < 100) _health.text = _data.Health.ToString(); // костыль
                else _health.text = "0";
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

        [ContextMenu("Switch Visual")]
        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }

        private void CheckHealth()
        {
            if (_data.Health <= 0 || _data.Health >= 100) Destroy(gameObject); // костыль
        }

        public CardPropertiesData GetData() => _data;
    }
}
