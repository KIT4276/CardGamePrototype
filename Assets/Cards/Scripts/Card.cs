using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Cards;
using System.Collections;
using System;
using UnityEngine.InputSystem;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IDragHandler
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

        private float _yPosition;
        


        //private Camera _camera;
        //[SerializeField]
        //private InputAction _input;
        //private Transform _selected;
        //private Vector3 _offset;

        public bool IsEnable
        {
            get => _icon.enabled;
            set
            {
                _icon.enabled = value;
                _frontCard.SetActive(value);
            }
        }

        public CardStateType State { get; set; } = CardStateType.InDeck;

        //private void Start()
        //{
        //    _camera = Camera.main;
        //    _input.Enable();
        //    _input.performed +=  _ => OnClick(true);
        //    _input.canceled += _=> OnClick(false);
        //}


        //private void Update()
        //{
        //    if (_selected == null) return;

        //    Vector3 position = (Vector3)Mouse.current.position.ReadValue() + _offset;
        //    position = _camera.ScreenToWorldPoint(position);

        //    _selected.position = position ;
        //}

        private void Start()
        {
            
        }

        public void Configuration(CardPropertiesData data, string description, Material image)
        {
            _icon.sharedMaterial = image;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _description.text = description;
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _attack.text = data.Attack.ToString();
            _health.text = data.Health.ToString();
        }

        public void OnDrag(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    var hitPos = eventData.pointerCurrentRaycast.worldPosition;
                    var pos = transform.position;
                    transform.position = new Vector3(hitPos.x, _yPosition, hitPos.z);

                    DefinitionObjectUder();
                    break;
                case CardStateType.OnTable:
                    break;
                
            }
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("OnPointerClick " + eventData);

            //DefinitionObjectUder();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    break;
                case CardStateType.OnTable:
                    break;
                case CardStateType.Discard:
                    break;
                default:
                    if (CardManager.Self.GetIsPlayer1Turn())
                    {
                        //Debug.Log("добавить карту в колоду 1 " + eventData);
                        //добавить карту в колоду 1
                        CardManager.Self.PlaceCardInDeck1(this, CardManager.Self.GetCardNumber1());
                    }
                    else if (!CardManager.Self.GetIsPlayer1Turn())
                    {
                        StartCardManager.Self._messageText.text = "Left to choose cards";
                        //добавить карту в колоду 2
                        CardManager.Self.PlaceCardInDeck2(this, CardManager.Self.GetCardNumber2());
                    }

                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _yPosition = transform.position.y+2;
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale *= 1.5f;
                    transform.position += new Vector3(0f, 2f, 0f);
                    break;
                case CardStateType.OnTable:
                    break;
                default:
                    transform.localScale *= 2f;
                    transform.position += new Vector3(0f, 1f, -7f);
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
               case CardStateType.InHand:
                    transform.localScale /= 1.5f;
                    transform.position -= new Vector3(0f, 2f, 0f);
                    break;
               case CardStateType.OnTable:
                    break;
                default:
                    transform.localScale /= 2f;
                    transform.position -= new Vector3(0f, 1f, -7f);
                    break;
            }
        }

        private void DefinitionObjectUder()
        {
            //Debug.Log("DefinitionObjectUder ");

            Vector3 origin = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            Vector3 direction = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
            Physics.Raycast(origin, direction, out var hit, 1000f);

             //hit.transform.TryGetComponent<TableCard>(out TableCard comp);//не находит карту под собой
            //if (comp == null) return;

            //else if (hit.transform.TryGetComponent<TableCard>(out TableCard component))
            //{
            //    Debug.Log("hit is TableCard " + component);
            //}
        }

        [ContextMenu("Switch Visual")]
        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }
    }
}
