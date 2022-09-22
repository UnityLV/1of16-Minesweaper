using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RemainingBombsIndicator : MonoBehaviour
{
    private int _markedBombs;
    private int _bombsAmount;

    public event UnityAction<int> RemainingBombUpdated;

    public void OnPlateBombMarkUpdated(bool isMarked)
    {
        if (isMarked)
        {
            _markedBombs++;

            int remainingBombs = _bombsAmount - _markedBombs; 

            RemainingBombUpdated?.Invoke(remainingBombs);
            return;
        }

        _markedBombs--;

    }

    public void ResetBombAmount(int value)
    {
        _bombsAmount = value;
        _markedBombs = 0;
    }
    
}
