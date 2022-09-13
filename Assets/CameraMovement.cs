using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlatesGrid _platesGrid;
    [SerializeField] private float _speed = 30;

    private void OnEnable()
    {
        _platesGrid.FindetStartPosition += OnFindetStartPosition;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * _speed);
        }
    }

    private void OnDisable()
    {
        _platesGrid.FindetStartPosition -= OnFindetStartPosition;
    }

    private void OnFindetStartPosition(Vector3 arg0)
    {
        transform.position = new Vector3(arg0.x , arg0.y , transform.position.z);
    }

    public void SetDefaultPosition()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
