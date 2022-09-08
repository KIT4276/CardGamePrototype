using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class PlayerManager : MonoBehaviour
    {
        protected int _halth = 20;

        
        [SerializeField]
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

        protected void Start()
        {
            _sideTypeText.text = _sideType.ToString();
            SetMaterial();
        }

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

        public void SetHalth(int value)
        {
            _halth = value;
        }

        public int GetHalth()
        {
            return _halth;
        }

        private void LateUpdate()
        {
            _halthIndicator.text = _halth.ToString();
        }
    }
}
