using System.Collections;
using UnityEngine;

public sealed class CameraMovement : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private PlatesGrid _platesGrid;
    [SerializeField] private float _speed = 30;
    private Vector3 _startPosition;
    private float _plateSize = 1f;
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

    private void OnFindetStartPosition(Vector3 position)
    {
        
        _startPosition = position;
        if (_settings.MapSize > 10)
        {
            position.Set(position.x, position.y, _zOffset);
            StartCoroutine(SmoothMoveTo(position));
        }
        else
        {
            transform.position = GetCenterPosition();
        }
    }

    private void OnGameStarted()
    {
        StopAllCoroutines();
        transform.position = GetCenterPosition();
    }

    private Vector3 GetCenterPosition()
    {
        Vector3 topRoghtCorner = new Vector3(
            (_settings.MapSize * _plateSize / 2) + (-_plateSize / 2f),
            (_settings.MapSize * _plateSize / 2) + (-_plateSize / 2f),
            _zOffset);

        return topRoghtCorner;
    }

    public void SetDefaultPosition()
    {
        transform.position = new Vector3(0, 0, -10);
    }
    private IEnumerator SmoothMoveTo(Vector3 point)
    {
        var t = 0f;

        while (t < 0.17f)
        {
            t += Time.deltaTime / 5f;

            transform.position = Vector3.Lerp(transform.position, point, t);

            yield return null;
        }
    }
}
