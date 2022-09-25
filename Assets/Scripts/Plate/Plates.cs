using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class Plates : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPooleable
{
    private bool _isOpen; 
    private bool _endOfGame;
    private Vector2 _mouseDownPosition;
    private float _maxDistaceToClick = 10f;
    
    public event UnityAction<bool,int> LeftClick;
    public event UnityAction<bool> RightClick;
    public event UnityAction<bool> BombMarkUpdated;
    public event UnityAction<Vector2Int> OpenedZero;
    public event UnityAction GameOver;
    public event UnityAction Winable;
    public event UnityAction FirstBomb;
    public event UnityAction FalseBombMark;
    public event UnityAction<Vector2Int> PressedOnNumber;
    public event UnityAction<IPooleable> Deactivation;
    

    public Vector2Int Position { get; private set; }
    public bool IsCheked { get; private set; }
    public bool IsBombMark { get; private set; }
    public bool IsBomb { get; private set; }
    public bool IsFalseBombMark { get; private set; }
    public int NearbyBobmAmount { get; private set; }

    public void Init(FillingPlates nearbyBobmAmount,Vector2Int position)
    {
        Position = position;
        IsBomb = nearbyBobmAmount.IsBomb;
        NearbyBobmAmount = nearbyBobmAmount.Number;
    }    

    public void OnPointerDown(PointerEventData eventData)
    {
        _mouseDownPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {        
        if (pointerEventData.pointerId == -1 && !IsBombMark && !_endOfGame)
        {
            bool valableForCLick = Vector3.Distance(_mouseDownPosition, pointerEventData.position) < _maxDistaceToClick;
            if (valableForCLick)
            {
                SimulatePressingLeft();

                if (NearbyBobmAmount == 0)
                    OpenedZero?.Invoke(Position);
            }

            if (NearbyBobmAmount > 0)
            {
                PressedOnNumber?.Invoke(Position);
            }           
        }        

        if (pointerEventData.pointerId == -2 && !_isOpen && !_endOfGame)
        {
            RightClick?.Invoke(IsBombMark);
            IsBombMark = !IsBombMark;
            BombMarkUpdated?.Invoke(IsBombMark);
            Winable?.Invoke();
        }
    }

    public void SimulatePressingLeft()
    {
        if (!IsBombMark && !_endOfGame)
        {
            if (_isOpen == false)
            {
                Open();
                if (IsBomb == true)
                {
                    SetLooseInGame();
                }
                
            }
        }

    }

    public void MarkToCheked()
    {
        IsCheked = true;        
    }

    public void OpenedZeroIvent() => OpenedZero?.Invoke(Position);

    public void Open()
    {
        LeftClick?.Invoke(IsBomb, NearbyBobmAmount);
        _isOpen = true;
    }

    
    public void SetEndOfGAme() => _endOfGame = true;
    public void SetFalseBombMark() => FalseBombMark?.Invoke();
    private void SetLooseInGame()
    {       
        GameOver?.Invoke();
        FirstBomb?.Invoke();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Deactivation?.Invoke(this);
        ResetValues();
    }

    public void Activate()
    {
        gameObject.SetActive(true);        
    }

    private void ResetValues()
    {
        _isOpen = false;
        _endOfGame = false;

        IsCheked = false;
        IsBombMark = false;
        IsBomb = false;
        IsFalseBombMark = false;
        NearbyBobmAmount = 0;
}

}
