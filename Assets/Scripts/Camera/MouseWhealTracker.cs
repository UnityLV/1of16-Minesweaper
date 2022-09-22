using UnityEngine;

public class MouseWhealTracker : MonoBehaviour
{
    [SerializeField] private float _maxDictance = 100f;
    [SerializeField] private float _minDictance = 1f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 0.3f;    
    private float _defaultOrthographicSize;
    private void Start()
    {
        _defaultOrthographicSize = _camera.orthographicSize;
    }

    void Update()
    {
        TryZoom();
    }

    public void ResetZoom()
    {
        _camera.orthographicSize = _defaultOrthographicSize;
    }

    private void TryZoom()
    {
        _camera.orthographicSize += Input.mouseScrollDelta.y * -_speed;

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minDictance, _maxDictance);
    }
}
