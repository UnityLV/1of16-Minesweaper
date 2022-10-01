using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RemainingBombsIndicator : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private PlatesGrid _platesGrid;    
    

    public event UnityAction<int, Vector2Int> MarkPlaced;

    private void OnEnable()
    {
        _platesGrid.PlatesMarkChanged += OnPlatesMarkChanged;       
    }


    private void OnDisable()
    {
        _platesGrid.PlatesMarkChanged -= OnPlatesMarkChanged;        
    }

    private void OnPlatesMarkChanged(bool isMarked, Vector2Int position)
    {
        if (isMarked)
        {
            int remainingBombs = _settings.BombsAmount - _platesGrid.MarkedBombs - _platesGrid.FalseMarkedBombs;
            MarkPlaced?.Invoke(remainingBombs, position);
        }
    }  
}
