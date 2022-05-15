using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Plates : MonoBehaviour, IPointerClickHandler
{
    private bool _isBomb;
    private int _nearbyBobmAmount;
    private bool _isOpen;
    private bool _isCheked;
    private bool _isBombMark;
    private bool _gameOver;
    private bool _isFalseBombMark;
    public event UnityAction<bool,int> LeftClick;
    public event UnityAction<bool> RightClick;
    public event UnityAction<Vector2Int> Opened;
    public event UnityAction GameOver;
    public event UnityAction FirstBomb;
    public event UnityAction FalseBombMark;
    public Vector2Int Position { get; private set; }
    public bool IsCheked => _isCheked;
    public bool IsBombMark => _isBombMark;
    public bool IsBomb => _isBomb;
    public bool IsFalseBombMark => _isFalseBombMark;
    public int NearbyBobmAmount => _nearbyBobmAmount;
    public void Init(bool isBomb,int nearbyBobmAmount,Vector2Int position)
    {
        Position = position;
        _isBomb = isBomb;
        _nearbyBobmAmount = nearbyBobmAmount;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId==-1 && !_isBombMark && !_gameOver)
        {
            if (_isOpen == false)
            {
                Open();
                if (_isBomb == true)
                {
                    Debug.Log("Game Ower");
                    _gameOver = true;
                    GameOver?.Invoke();
                    FirstBomb?.Invoke(); 
                }               

                if (_nearbyBobmAmount == 0)
                    Opened?.Invoke(Position);
            }
            
        }
        if (eventData.pointerId == -2 && !_isOpen && !_gameOver)
        {
            RightClick?.Invoke(_isBombMark);
            _isBombMark = !_isBombMark;
            
        }
        
        
    }
    public void MarkToOpen()
    {        
        _isCheked = true;
        Open();
    }

    public void Open()
    {
        LeftClick?.Invoke(_isBomb, _nearbyBobmAmount);
        _isOpen = true;
    }
    public void SetGameOver()
    {
        _gameOver = !_gameOver;
    }
    public void SetFalseBombMark()
    {
        FalseBombMark?.Invoke();
    }



}
