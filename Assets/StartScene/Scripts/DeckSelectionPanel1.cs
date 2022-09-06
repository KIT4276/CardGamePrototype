using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class DeckSelectionPanel1 : PanelManager
    {
        [SerializeField]
        private Material _material;
        
        [Space, SerializeField]
        private GameObject _pageClassCards;
        [SerializeField]
        private GameObject _page1;
        [SerializeField]
        private GameObject _page2;
        [SerializeField]
        private GameObject _page3;
        [SerializeField]
        private GameObject _page4;
        [SerializeField]
        private GameObject _page5;
        [SerializeField]
        private GameObject _page6;
        [SerializeField]
        private GameObject _page7;
        [SerializeField]
        private GameObject _page8;
        [SerializeField]
        private GameObject _page9;
        [SerializeField]
        private GameObject _page10;
        [SerializeField]
        private GameObject _page11;
        [SerializeField]
        private GameObject _page12;

        private void Start()
        {
            _material.renderQueue = 2500;
        }

        public void MoveDeckPanel1() => StartCoroutine(MoveFromTo(new Vector3(0f, 0f, 100f)));

        public void MoveOutDeckPanel1() => StartCoroutine(MoveFromTo(new Vector3(0f, 150f, 100f)));

        public void DisableAllPages()
        {
            _pageClassCards.SetActive(false);
            _page1.SetActive(false);
            _page2.SetActive(false);
            _page3.SetActive(false);
            _page4.SetActive(false);
            _page5.SetActive(false);
            _page6.SetActive(false);
            _page7.SetActive(false);
            _page8.SetActive(false);
            _page9.SetActive(false);
            _page10.SetActive(false);
            _page11.SetActive(false);
            _page12.SetActive(false);
        }

        #region Enable Pages
        public void EnablePageClassCards() => _pageClassCards.SetActive(true);

        public void EnablePage1() => _page1.SetActive(true);

        public void EnablePage2() => _page2.SetActive(true);

        public void EnablePage3() => _page3.SetActive(true);

        public void EnablePage4() => _page4.SetActive(true);

        public void EnablePage5() => _page5.SetActive(true);

        public void EnablePage6() => _page6.SetActive(true);

        public void EnablePage7() => _page7.SetActive(true);

        public void EnablePage8() => _page8.SetActive(true);

        public void EnablePage9() => _page9.SetActive(true);

        public void EnablePage10() => _page10.SetActive(true);

        public void EnablePage11() => _page11.SetActive(true);

        public void EnablePage12() => _page12.SetActive(true);

        #endregion

    }
}
