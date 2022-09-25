using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _platesLayerMask;

    private Vector2 _startDragPosition;
    private float _minDragDistance = 10.5f;
    
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _startDragPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {            
            var collider = Physics2D.OverlapPoint(GetMousePosition(), _platesLayerMask);
            if (collider != null)
            {
                if (collider.gameObject.TryGetComponent(out Plates plate))
                {
                    if (IsAvalableForClick())
                    {
                        plate.PressingLeftMouseButton();
                    }
                    plate.PressingOnNumber();
                }
            }
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var collider = Physics2D.OverlapPoint(mousePosition, _platesLayerMask);

            if (collider != null)
            {
                if (collider.gameObject.TryGetComponent(out Plates plate))
                {
                    plate.PressingRightMouseButton();
                }
            }
        }

        
    }

    private bool IsAvalableForClick()
    {
        return Vector2.Distance(_startDragPosition, Input.mousePosition) < _minDragDistance;
    }

    private Vector2 GetMousePosition() => _camera.ScreenToWorldPoint(Input.mousePosition);
}
