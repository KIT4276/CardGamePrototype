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
        
        protected SideType _sideType;


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
