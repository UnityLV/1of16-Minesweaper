using UnityEngine;

public class MouseWhealTracker : MonoBehaviour
{
    [SerializeField] private float _maxDictance;
    [SerializeField] private float _minDictance;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private Settings _settings;

    private void OnEnable()
    {
        _settings.MapSizeChanged += OnMapSizeChanged;
    }

    private void OnDisable()
    {
        _settings.MapSizeChanged -= OnMapSizeChanged;
    }

    private void OnMapSizeChanged(int mapLength)
    {
        _maxDictance = mapLength;
    }

    void Update()
    {
        TryZoom();
    }

    private void TryZoom()
    {
        _camera.orthographicSize += Input.mouseScrollDelta.y * -_speed;

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minDictance, _maxDictance);
    }
}
