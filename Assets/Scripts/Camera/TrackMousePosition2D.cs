using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackMousePosition2D : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Settings _settings;
    [SerializeField] private float _speed;
    private int _defaultPorder = 50;
    private readonly float _screenDistanceToMove = 40;


    void Update()
    {        

        if (Input.mousePosition.x >= Screen.width - _screenDistanceToMove)
        {
            transform.Translate(Vector2.right * Time.deltaTime * _speed);
        }
        if (Input.mousePosition.x <= _screenDistanceToMove)
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
        }
        if (Input.mousePosition.y >= Screen.width - _screenDistanceToMove)
        {
            transform.Translate(Vector2.up * Time.deltaTime * _speed);
        }
        if (Input.mousePosition.y <= _screenDistanceToMove)
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }

        Vector3 vector3 = transform.position;

        vector3.x = Mathf.Clamp(vector3.x, -_defaultPorder, (_settings.MapSize - 5) * 10);
        vector3.y = Mathf.Clamp(vector3.y, -(_settings.MapSize - 5) * 10, _defaultPorder);
        vector3.Set(vector3.x, vector3.y, transform.position.z);

        transform.position = vector3;
    }
    private Vector2 GetWorldMousePosition2D() => _camera.ScreenToWorldPoint(Input.mousePosition);
}
