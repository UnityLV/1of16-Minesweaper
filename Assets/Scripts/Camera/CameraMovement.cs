using System.Collections;
using UnityEngine;

public sealed class CameraMovement : MonoBehaviour
{
    [SerializeField] private Settings _settings;    
    [SerializeField] private PlatesGrid _platesGrid;
    [SerializeField] private float _speed = 30;
    private int _noMoveMapLinght = 10;
    private float _zOffset = -10;

    private void OnEnable()
    {
        _platesGrid.FindetStartPosition += OnFindetStartPosition;
        _platesGrid.StartedGame += OnGameStarted;
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
        _platesGrid.StartedGame -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        TrySetStartPosition();
    }

    private void OnFindetStartPosition(Vector3 arg0)
    {
        if (_settings.MapLength > _noMoveMapLinght)
        {
            StartCoroutine(MoveToTarget(new Vector3(arg0.x, arg0.y, transform.position.z)));            
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        var t = 0f;        ;        
        while (t < 0.08f)
        {
            t += Time.deltaTime * 0.04f;           
            transform.position = Vector3.Lerp(transform.position, target, t);
            yield return null;           
        }
    }

    public void TrySetStartPosition()
    {
        if (_settings.MapLength < _noMoveMapLinght)
        {
            SetCenterPostion();
        }      

    }

    private void SetCenterPostion()
    {
        int lenght = _settings.MapLength;
        Vector3 center = new Vector3(-50 + (lenght / 2f * 10f), 50 - (lenght / 2f * 10f), _zOffset);
        transform.position = center;
    }
}
