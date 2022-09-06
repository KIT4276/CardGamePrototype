using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Cards
{
    public class ShiftCard : MonoBehaviour
    {


        //private Camera _camera;
        //[SerializeField]
        //private InputAction _input;
        //private Transform _selected;
        //private Vector3 _offset;

        //private void OnEnable()
        //{
        //    _input.Enable();
        //}
        //private void Start()
        //{
        //    _camera = Camera.main;

        //    _input.performed += _ => OnClick(true);
        //    _input.canceled += _ => OnClick(false);
        //}

        //private void Update()
        //{
        //    if (_selected == null) return;

        //    Vector3 position = (Vector3)Mouse.current.position.ReadValue() + _offset;
        //    position = _camera.ScreenToWorldPoint(position);

        //    _selected.position = position;
        //}

        //private void OnClick(bool isSelect)
        //{
        //    if (isSelect)
        //    {
        //        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //        if (Physics.Raycast(ray, out var hit))
        //        {
        //            _selected = hit.transform;
        //            _offset = _selected.position - _camera.transform.position;
        //            //_offset = _selected.position - _camera.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
        //            ////_offset.x = _camera.transform.position.x - _selected.position.x;
        //            ////_offset.y =   _camera.transform.position.y - _selected.position.y;
        //            ////_offset.z = _selected.position.z - _camera.transform.position.z;

        //            Debug.Log("_offset " + _offset);
        //        }
        //    }
        //    else
        //    {
        //        _selected = null;
        //    }
        //}

        //private void OnDisable()
        //{
        //    _input.Disable();
        //}
    }
}
