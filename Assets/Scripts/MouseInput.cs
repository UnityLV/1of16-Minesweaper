using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _platesLayerMask;

    private Vector2 _startDragPosition;
    private float _minDragDistance = 50f;

    public event UnityAction LeftCliked;
    public event UnityAction RightCliked;
    
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())        
            return;        

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))        
            _startDragPosition = Input.mousePosition;
        

        if (Input.GetMouseButtonUp(0))
        {            
            var collider = Physics2D.OverlapPoint(GetMousePosition(), _platesLayerMask);
            if (collider != null)            
                if (collider.gameObject.TryGetComponent(out Plates plate))
                {
                    if (IsAvalableForClick())
                    {
                        plate.PlayerPressingLeftMouseButton();
                        LeftCliked?.Invoke();
                    }
                    plate.PressingOnNumber();
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
                    RightCliked?.Invoke();

                    plate.PressingOnNumber();
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
