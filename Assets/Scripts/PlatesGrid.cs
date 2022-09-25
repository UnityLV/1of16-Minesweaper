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
    private Plates[,] _plates;
    private int _markedBombs;

    public event UnityAction GameOver;
    public event UnityAction StartedGame;
    public event UnityAction<Vector3> FindetStartPosition;    


    private void PreGenerateField()
    {

    }

    public void SpawnGrid()
    {
        SetSize();       
        _plates = _generatePlatesField.SpawnPlates(_settings.BombsAmount, _with, _hight);
        _markedBombs = 0;
        Subscribe();
        StartedGame?.Invoke();
        StartCoroutine(WaitAndOpenRandomZeros(0.2f));
    }

    private void SetSize()
    {
        _with = _settings.MapSize;
        _hight = _settings.MapSize;        
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
            if (child.TryGetComponent(out Plates plate))
            {
                plate.Deactivate();
            }           
        }
        UnSubscribe();
    }

    private void Subscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero += OnOpened;
            plate.PressedOnBomb += OnPressedOnBomb;
            plate.MarkChanged += OnMarkChanged;
            plate.PressedOnNumber += OnPressedOnNumber;
        }
    }


    private void UnSubscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero -= OnOpened;
            plate.PressedOnBomb -= OnPressedOnBomb;           
            plate.MarkChanged -= OnMarkChanged;
            plate.PressedOnNumber -= OnPressedOnNumber;
        }
    }

    private void OnPressedOnBomb()
    {
        OpenAllBombs();
        SetAllFalseBombMark();
        InvokeGameOver();
        SetGameOverOnAllPlates();        
    }

    private IEnumerator WaitAndOpenRandomZeros(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        TryOpenRandomZeros();        
    }

    private IEnumerator OpenNeerbyZerosSlowly(int x, int y)
    {
        yield return null;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].NearbyBobmAmount == 0)
                        if (_plates[i + x, j + y].IsCheked == false && _plates[i + x, j + y].IsBombMark == false)
                        {
                            _plates[i + x, j + y].MarkToCheked();
                            ShowNeerbyPlates(i + x, j + y);
                            yield return OpenNeerbyZerosSlowly(i + x, j + y);
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
        
        
        _plates[x, y].SimulatePressingLeft();
        FindetStartPosition?.Invoke(_plates[x, y].transform.position);

    }

    private void SetWinInGame()
    {       
        InvokeGameOver();
        OpenAllNumber();
    }

    private void InvokeGameOver()
    {
        SetGameOverOnAllPlates();
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


    private void OnMarkChanged(bool isBombMark)
    {        
        _markedBombs += isBombMark ? 1 : -1;
        CheckWin();
    }

    private void CheckWin()
    {
        if (_markedBombs == _settings.BombsAmount)
            SetWinInGame();
    }

    private void SetAllFalseBombMark()
    {
        foreach (var plate in _plates)
            if (plate.IsBombMark && plate.IsBomb == false)
                plate.SetFalseBombMark();
    }

    private void SetGameOverOnAllPlates()
    {
        foreach (var plate in _plates)
            plate.SetGameOver();
    }
    private void OpenAllNumber()
    {
        foreach (var plate in _plates)
            plate.ShowNumber();
    }

    private void OpenAllBombs()
    {
        foreach (var plate in _plates)
            if (plate.IsBomb && plate.IsBombMark == false)
                plate.ShowBomb();
    }
    private void OnOpened(Vector2Int position)
    {
        int x, y;   
        x = position.x;
        y = position.y;

        StartCoroutine(OpenNeerbyZerosSlowly(x, y));            
    }
      
   
    private void ShowNeerbyPlates(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsNoMarked(i + x, j + y))
                    _plates[i + x, j + y].SimulatePressingLeft();         
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
