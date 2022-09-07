using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Cards;
using System.Collections;
using System;
using UnityEngine.InputSystem;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
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

        private Transform _landingPoint;
        

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
                    transform.position = new Vector3(hitPos.x, transform.position.y, hitPos.z);
                    break;
                case CardStateType.OnTable:
                    var hitPosT = eventData.pointerCurrentRaycast.worldPosition;
                    var posT = transform.position;
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
                    transform.localScale *= 1.5f;
                    transform.position += new Vector3(0f, 2f, 0f);
                    break;
                default:
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
                    transform.localScale /= 1.5f;
                    transform.position -= new Vector3(0f, 2f, 0f);
                    break;
                default:
                    transform.localScale /= 1.5f;
                    transform.position -= new Vector3(0f, 1f, -7f);
                    break;
            }
        }

        //private void DefinitionObjectUder() // как-то некорректно работает
        //{
        //    Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //    Vector3 direction = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
        //    Physics.Raycast(origin, direction, out var hit, 1000f);
            
        //    Debug.Log(hit.rigidbody);
        //    if (hit.rigidbody.GetComponent<TableCard>() != null) return;
        //    else transform.position = hit.transform.position;
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<TableCard>(out TableCard component))
            {
                _landingPoint = other.transform;
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            Vector3 direction = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
            Physics.Raycast(origin, direction, out var hit, 1000f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, direction);
        }

        [ContextMenu("Switch Visual")]
        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }

        
    }
}
