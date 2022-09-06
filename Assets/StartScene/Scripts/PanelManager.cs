using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Cards
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField]
        private Image _player1Panel;
        [SerializeField]
        private Image _player2Panel;
        [SerializeField]
        private Image _deckSelectionPanel1;
        //[SerializeField]
        //private Image _deckSelectionPanel2;
        [SerializeField]
        protected SideType _sideType;

        private Controls _controls;
        //private Image _activePanel;

        void Start()
        {
            _controls = new Controls();
            //_activePanel = _player1Panel;
        }
        public SideType GetSideType()
        {
            return _sideType;
        }

        #region SetSideType
        public void SetMageType()
        {
            _sideType = SideType.Mage;
        }

        public void SetWarriorType()
        {
            _sideType = SideType.Warrior;
        }

        public void SetPriestType()
        {
            _sideType = SideType.Priest;
        }

        public void SetHunterType()
        {
            _sideType = SideType.Hunter;
        }
        #endregion

        protected IEnumerator MoveFromTo(Vector3 targetToMove)
        {
            var currentTime = 0f;
            var startPos = GetComponent<RectTransform>().transform.position;
            targetToMove = new Vector3(startPos.x, targetToMove.y, startPos.z);

            while (currentTime < 2)
            {
                GetComponent<RectTransform>().transform.position = Vector3.Lerp(startPos, targetToMove, currentTime / 2);
                currentTime += 2 * Time.deltaTime;
                yield return null;
            }
            GetComponent<RectTransform>().transform.position = targetToMove;
        }

        public void MovePanelkSelectBar()
        {
            _player1Panel.gameObject.SetActive(false);
            _player2Panel.gameObject.SetActive(false);

        }
    }
}
