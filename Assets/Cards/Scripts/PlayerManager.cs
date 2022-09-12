using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    public class PlayerManager : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected int _halth = 20;
        
        [Space, SerializeField]
        protected TextMeshPro _halthIndicator;
        [SerializeField]
        protected TextMeshPro _sideTypeText;

        [Space, Tooltip("All class materials"), SerializeField]
        protected Material _MageMaterial;
        [SerializeField]
        protected Material _WarriorMterial;
        [SerializeField]
        protected Material _PriestMterial;
        [SerializeField]
        protected Material _HunterMterial;

        protected SideType _sideType;

        public static PlayerManager Self;

        protected void Start()
        {
            Self = this;
            _sideTypeText.text = _sideType.ToString();
            SetMaterial();
        }

        private void LateUpdate() => _halthIndicator.text = _halth.ToString();

        protected void SetMaterial()
        {
            switch (_sideType)
            {
                case SideType.Mage:
                    gameObject.GetComponent<MeshRenderer>().material = _MageMaterial;
                    break;
                case SideType.Warrior:
                    gameObject.GetComponent<MeshRenderer>().material = _WarriorMterial;
                    break;
                case SideType.Priest:
                    gameObject.GetComponent<MeshRenderer>().material = _PriestMterial;
                    break;
                case SideType.Hunter:
                    gameObject.GetComponent<MeshRenderer>().material = _HunterMterial;
                    break;
            }
        }

        public void SetHalth(int value) => _halth = value;

        public int GetHalth() => _halth;

        public void OnPointerClick(PointerEventData eventData)
        {
            var attack = GameManager.Self._selectedCard.GetData().Attack;
            GameManager.Self.StartJoinTheFight(this.transform);
            StartCoroutine(DealtDamage(attack));
        }

        private IEnumerator DealtDamage(ushort attack)
        {
            yield return new WaitForSeconds(3f);
            _halth -= attack;
            yield return new WaitForSeconds(2f);
            GameManager.Self.ChangePlayersTurn();
            yield return null;
        }
    }
}
