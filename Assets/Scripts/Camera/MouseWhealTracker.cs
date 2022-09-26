using System.Collections;
using UnityEngine;

public class MouseWhealTracker : MonoBehaviour
{
    [SerializeField] private float _maxDictance;
    [SerializeField] private float _minDictance;
    [SerializeField] private float _defaultDistance = 5;
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private Camera _camera;
    [SerializeField] private Settings _settings;
    [SerializeField] private PlatesGrid _platesGrid;
    private int _maxDefaultMapSize = 10;

    private void OnEnable()
    {
        _settings.MapSizeChanged += OnMapSizeChanged;
        _platesGrid.StartedGame += OnGameStarted;
        
    }


    private void OnDisable()
    {
        _settings.MapSizeChanged -= OnMapSizeChanged;
        _platesGrid.StartedGame -= OnGameStarted;
        
    }

    private void OnMapSizeChanged(int mapLength)
    {
        _maxDictance = mapLength / 2f;
    }

    private void OnGameStarted()
    {
        _camera.orthographicSize = _maxDictance;
        if (_settings.MapSize > _maxDefaultMapSize)
        {
            StartCoroutine(SmoothZoomTo());
        }
    }

    

    void Update()
    {
        TryZoom();
    }

    private void TryZoom()
    {
        _camera.orthographicSize += Input.mouseScrollDelta.y * -_speed * Mathf.Log(_camera.orthographicSize);

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minDictance, _maxDictance);
    }

    private IEnumerator SmoothZoomTo()
    {
        var t = 0f;               

        while (t < 0.6f && Input.mouseScrollDelta.y == 0)
        {
            t += Time.deltaTime / 3f;

            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _defaultDistance, t);            

            yield return null;
        }
    }
}
