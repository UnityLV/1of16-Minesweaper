﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraClickAndDrug : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _origin;
    private Vector3 _difference;    
    private bool _drag = false;

    public bool IsDraging => _drag;



    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            _difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
            if(_drag == false)
            {
                _drag = true;
                _origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag)
        {
            _camera.transform.position = _origin - _difference;
        }

        

    }
}
