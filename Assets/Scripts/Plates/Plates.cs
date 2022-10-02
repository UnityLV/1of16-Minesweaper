using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public sealed class Plates : MonoBehaviour,IPooleable
{
    private bool _endOfGame; 
    
    public event UnityAction<bool,int> LeftClick;
    public event UnityAction<bool> RightClick;
    public event UnityAction<bool, Vector2Int> MarkChanged;    
    public event UnityAction PlayerOpenedZero;
    public event UnityAction<Vector2Int> OpenedZero;
    public event UnityAction FirstBombPressed;
    public event UnityAction PressedOnBomb;
    public event UnityAction ShowedBombs;
    public event UnityAction FalseBombMarkFinded;
    public event UnityAction<Vector2Int> PressedOnNumber;
    public event UnityAction<IPooleable> Deactivation;

    public Vector2Int Position { get; private set; }    
    public bool IsOpen { get; private set; }
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
    
    public void PressingRightMouseButton()
    {
        if ((IsOpen || _endOfGame) == false)
        {
            IsBombMark = !IsBombMark;

            RightClick?.Invoke(IsBombMark);  
            MarkChanged?.Invoke(IsBombMark, Position);
            
        }
    }
    public void SimulatePressingLeftMouseButton()
    {
        if (_endOfGame == false)
        {
            if (IsOpen == false && IsBombMark == false)
            {
                PressedLeftOnClose();
            } 
        }
    }

    public void PlayerPressingLeftMouseButton()
    {
        if (_endOfGame == false)
        {
            if (IsOpen == false && IsBombMark == false)
            {
                PressedLeftOnClose();
                if (NearbyBobmAmount == 0)
                {
                    PlayerOpenedZero?.Invoke();
                }
            }
        }
    }


    public void PressingOnNumber()
    {
        if (NearbyBobmAmount > 0 && IsBomb == false && IsBombMark == false)
        {
            PressedOnNumber?.Invoke(Position);
        }
    }

    private void PressedLeftOnClose()
    {
        Open();
        if (IsBomb == true)
        {
            PressedOnBomb?.Invoke();            
            FirstBombPressed?.Invoke();
        }
        if (NearbyBobmAmount == 0)
            OpenedZero?.Invoke(Position);
    }   
    
    private void Open()
    {
        LeftClick?.Invoke(IsBomb, NearbyBobmAmount);
        IsOpen = true;
    }    

    public void SetFalseBombMark() => FalseBombMarkFinded?.Invoke();

    public void ShowBomb() => ShowedBombs?.Invoke();

    public void ShowNumber()
    {
        if (IsBomb == false)
        {
            LeftClick?.Invoke(IsBomb, NearbyBobmAmount);
        }
    }

    public void Chek() => IsCheked = true;

    public void SetGameOver() => _endOfGame = true;

    public void Activate() => gameObject.SetActive(true);

    public void Deactivate()
    {
        ResetValues();
        Deactivation?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void ResetValues()
    {
        IsOpen = false;
        _endOfGame = false;

        IsCheked = false;
        IsBombMark = false;
        IsBomb = false;
        IsFalseBombMark = false;
        NearbyBobmAmount = 0;
    }

}
