using UnityEngine;

public class RemainingBombsIndicatorView : MonoBehaviour
{
    [SerializeField] private RemainingBombsIndicator _bombsIndicator;
    [SerializeField] private PopUp _popUp;
    [SerializeField] private Camera _camera;

    private float _zOffset = 10;
    private float _yOffset = 1;
    private int _maxBombAmountForShow = 1000;
    

    private void OnEnable()
    {
        _bombsIndicator.RemainingBombUpdated += OnRemainingBombsAMounntChanged;
    }

    private void OnDisable()
    {
        _bombsIndicator.RemainingBombUpdated -= OnRemainingBombsAMounntChanged;
    }

    private void OnRemainingBombsAMounntChanged(int amount)
    {
        if (_maxBombAmountForShow >= amount && amount > 0) 
        {
            CreatePopUp(amount);
        }
    }

    private void CreatePopUp(int amount)
    {
        var positon = _camera.ScreenToWorldPoint(Input.mousePosition);
        positon.Set(positon.x, positon.y + _yOffset, positon.z + _zOffset);

        var popUp = Instantiate(_popUp, positon, Quaternion.identity, transform);
        popUp.Init(amount);
    }
}
