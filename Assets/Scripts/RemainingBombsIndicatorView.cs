using UnityEngine;

public class RemainingBombsIndicatorView : MonoBehaviour
{
    [SerializeField] private RemainingBombsIndicator _bombsIndicator;
    [SerializeField] private PopUp _popUp;
    [SerializeField] private Camera _camera;
    private int _amountToShow = 10;
    private float _zOffset = -20;   
    

    private void OnEnable()
    {
        _bombsIndicator.MarkPlaced += OnMarkPlaced;
    }


    private void OnDisable()
    {
        _bombsIndicator.MarkPlaced -= OnMarkPlaced;
    }

    private void OnMarkPlaced(int amount, Vector2Int posotion)
    {
        if (amount > 0 && _amountToShow > amount)
        {
            CreatePopUp(amount, posotion);
        }
    }   

    private void CreatePopUp(int amount, Vector2Int point)
    {
        Vector3 position = new(point.x, point.y, _zOffset);      

        var popUp = Instantiate(_popUp, position, Quaternion.identity);
        popUp.Init(amount);
    }
}
