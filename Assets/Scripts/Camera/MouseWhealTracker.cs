using UnityEngine;

public class MouseWhealTracker : MonoBehaviour
{
    [SerializeField] private float _maxDictance = 100f;
    [SerializeField] private float _minDictance = 1f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 0.3f;

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
