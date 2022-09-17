using UnityEngine;

public sealed class CameraClickAndDrug : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _origin;
    private Vector3 _difference;    

    private bool _drag = false;    

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
            if(_drag == false)
            {
                _drag = true;
                _origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag)
        {
            _camera.transform.position = _origin - _difference;
        }

        

    }
}
