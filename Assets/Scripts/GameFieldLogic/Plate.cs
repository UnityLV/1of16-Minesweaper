using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class Plate : MonoBehaviour, IPointerClickHandler
{
    private bool _isOpen; 
    private bool _endOfGame; 
    
    public event UnityAction<bool,int> LeftClick;
    public event UnityAction<bool> RightClick;
    public event UnityAction<Vector2Int> Opened;
    public event UnityAction GameOver;
    public event UnityAction Winable;
    public event UnityAction FirstBomb;
    public event UnityAction FalseBombMark;
    public event UnityAction<Vector2Int> PressedOnNumber;
    public Vector2Int Position { get; private set; }
    public bool IsCheked { get; private set; }
    public bool IsBombMark { get; private set; }
    public bool IsBomb { get; private set; }
    public bool IsFalseBombMark { get; private set; }
    public int NearbyBobmAmount { get; private set; }

    public void Init(FillingPlate nearbyBobmAmount,Vector2Int position)
    {
        Position = position;
        IsBomb = nearbyBobmAmount.IsBomb;
        NearbyBobmAmount = nearbyBobmAmount.Number;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((IsBombMark && _endOfGame) == false)
        {
            if (eventData.pointerId == -1 )
            {
                PressingLeftMouseButton();
            }
            if (eventData.pointerId == -2 )
            {
                RightClick?.Invoke(IsBombMark);
                IsBombMark = !IsBombMark;
                Winable?.Invoke();
            }    

        }
    }

    private void PressingLeftMouseButton()
    {
        if (_isOpen == false)
        {
            Open();
            if (IsBomb == true)
            {
                SetLooseInGame();
            }
            if (NearbyBobmAmount == 0)
                Opened?.Invoke(Position);
        }
        else
        {
            if (NearbyBobmAmount > 0)
            {
                PressedOnNumber?.Invoke(Position);
            }
        }        
    }

    public void MarkToOpen()
    {        
        IsCheked = true;
        Open();
    }
    public void OpenedIvent() => Opened?.Invoke(Position);
    public void Open()
    {
        LeftClick?.Invoke(IsBomb, NearbyBobmAmount);
        _isOpen = true;
    }

    public void SimulatePressingLeft()
    {
        if (!IsBombMark && !_endOfGame)        
            if (_isOpen == false)
            {
                Open();
                if (IsBomb == true)                
                    SetLooseInGame();
                
                if (NearbyBobmAmount == 0)
                    Opened?.Invoke(Position);
            }  
    }
    public void SetEndOfGAme() => _endOfGame = true;
    public void SetFalseBombMark() => FalseBombMark?.Invoke();
    private void SetLooseInGame()
    {       
        GameOver?.Invoke();
        FirstBomb?.Invoke();
    }   
   
}
