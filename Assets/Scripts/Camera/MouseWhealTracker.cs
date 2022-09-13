using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;


public class MouseWhealTracker : MonoBehaviour
{
    [SerializeField] private float _maxDictance = 100f;
    [SerializeField] private float _minDictance = 1f;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _speed = -0.3f;

    void Update()
    {
        _camera.m_Lens.OrthographicSize += Input.mouseScrollDelta.y * _speed;

        _camera.m_Lens.OrthographicSize = Mathf.Clamp(_camera.m_Lens.OrthographicSize, _minDictance, _maxDictance);

    }    
}
