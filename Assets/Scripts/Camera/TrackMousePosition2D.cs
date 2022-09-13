using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class TrackMousePosition2D : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 vector3 = GetWorldMousePosition2D();
        vector3.Set(vector3.x, vector3.y, _transform.position.z);
        _transform.position = vector3;
    }
    private Vector2 GetWorldMousePosition2D() => _camera.ScreenToWorldPoint(Input.mousePosition);
}
