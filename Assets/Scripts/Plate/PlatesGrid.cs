using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public sealed class PlatesGrid : MonoBehaviour
{
    [SerializeField] private int _with;
    [SerializeField] private int _hight;
    [SerializeField] private Settings _settings;
    [SerializeField] private GeneratePlatesField _generatePlatesField;
    [SerializeField] private GridLayoutGroup _gridLayout;
    [SerializeField] private RemainingBombsIndicator _remainingIndicator;
    private Plates[,] _plates;    
    

    public event UnityAction GameOver;
    public event UnityAction StartedGame;
    public event UnityAction<Vector3> FindetStartPosition;    


    public void SpawnGrid()
    {
        SetSize();
        _plates = _generatePlatesField.SpawnPlates(_settings.BombsAmount, _with, _hight);
        Subscribe();
        StartedGame?.Invoke();

        _remainingIndicator.ResetBombAmount(Mathf.Clamp(_settings.BombsAmount,1, _with * _hight));
        StartCoroutine(WaitAndOpenRandomZeros(0.2f));
    }

    private void SetSize()
    {
        _with = _settings.MapLength;
        _hight = _settings.MapLength;
        _gridLayout.constraintCount = _settings.MapLength;
    }

    public void ReSpawnGrid()
    {
        Clear();        
        SpawnGrid();
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out Plates plates))
            {
                plates.Deactivate();
            }            
        }
        UnSubscribe();
    }

    private void Subscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero += OnOpened;
            plate.GameOver += OpenAllBombs;
            plate.GameOver += BlockAllPlates;
            plate.GameOver += SetAllFalseBombMark;
            plate.GameOver += InvokeGameOver;      
            plate.Winable += CheckWin;
            plate.PressedOnNumber += OnPressedOnNumber;
            plate.BombMarkUpdated += _remainingIndicator.OnPlateBombMarkUpdated;
        }
    }    

    private void UnSubscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero -= OnOpened;
            plate.GameOver -= OpenAllBombs;
            plate.GameOver -= BlockAllPlates;
            plate.GameOver -= SetAllFalseBombMark;
            plate.GameOver -= InvokeGameOver;
            plate.Winable -= CheckWin;
            plate.PressedOnNumber -= OnPressedOnNumber;
            plate.BombMarkUpdated -= _remainingIndicator.OnPlateBombMarkUpdated;
        }
    }

    private IEnumerator WaitAndOpenRandomZeros(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        TryOpenRandomZeros();        
    }

    private IEnumerator OpenNeerbyZeros(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].NearbyBobmAmount == 0)
                        if (_plates[i + x, j + y].IsCheked == false && _plates[i + x, j + y].IsBombMark == false)
                        {
                            _plates[i + x, j + y].MarkToCheked();
                            ShowPlates(i + x, j + y);
                            yield return OpenNeerbyZeros(i + x, j + y);
                        }        
        
    }

    private void TryOpenRandomZeros()
    {
        int x, y;
        int maxTryAmount = _with * _hight;
        do
        {
           x = Random.Range(0, _with);
           y = Random.Range(0, _hight);
            maxTryAmount--;

            if (maxTryAmount < 0)            
                return;
            

        } while (_plates[x,y].NearbyBobmAmount > 0 || _plates[x,y].IsBomb);
        
        _plates[x, y].Open();
        _plates[x, y].OpenedZeroIvent();
        FindetStartPosition?.Invoke(_plates[x, y].transform.position);

    }

    private void CheckWin()
    {
        int counter = 0;
        foreach (var plate in _plates)
        {
            if (plate.IsBomb && plate.IsBombMark)
                counter++;
            if (plate.IsBomb == false && plate.IsBombMark)
                counter--;
        }

        int bombsOnField = Mathf.Clamp(_settings.BombsAmount, 1, _with * _hight);

        if (counter == bombsOnField)
            SetWinInGame();
    }

    private void SetWinInGame()
    {
        BlockAllPlates();
        OpenAllNoBombs();
        InvokeGameOver();
    }

    private void InvokeGameOver()
    {
        StopAllCoroutines();
        GameOver?.Invoke();
    }

    private void OnPressedOnNumber(Vector2Int position)
    {
        if (IsAvalableToOpenAroundNuber(position))
        {
            TryOpenNearby(position.x, position.y);
        }
    }

    private bool IsAvalableToOpenAroundNuber( Vector2Int position)
    {
        return GetNearbyMarkAmount(position.x, position.y) == _plates[position.x, position.y].NearbyBobmAmount;
    }

    private void TryOpenNearby(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsNoMarked(i + x, j + y))
                    _plates[i + x, j + y].SimulatePressingLeft();
    }


   
    
    private void SetAllFalseBombMark()
    {
        foreach (var plate in _plates)
            if (plate.IsBombMark && plate.IsBomb == false)
                plate.SetFalseBombMark();
    }   
    private void BlockAllPlates()
    {
        foreach (var plate in _plates)        
            plate.SetEndOfGAme();        
    }
    private void OpenAllBombs()
    {
        foreach (var plate in _plates)
            if (plate.IsBomb && plate.IsBombMark == false)        
                plate.Open();                          
    }
    private void OnOpened(Vector2Int position)
    {
        int x, y;   
        x = position.x;
        y = position.y;

        StartCoroutine(OpenNeerbyZeros(x, y));            
    }
    private void OpenAllNoBombs()
    {
        foreach (var plate in _plates)        
            if (!plate.IsBomb)            
                plate.Open(); 
    }    
   
    private void ShowPlates(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsNoMarked(i + x, j + y))
                    _plates[i + x, j + y].Open();         
    }    

    private bool IsNoMarked(int x, int y) => IsInside(x, y) && _plates[x, y].IsBombMark == false;

    private int GetNearbyMarkAmount(int x, int y)
    {
        int counter = 0;

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].IsBombMark)
                        counter++;

        return counter;
    }

    private bool IsInside(int x, int y) =>
        x >= 0 && x < _plates.GetLength(0) &&
        y >= 0 && y < _plates.GetLength(1);
}
