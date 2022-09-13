using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class PlatesGrid : MonoBehaviour
{
    [SerializeField] private int with;
    [SerializeField] private int hight;
    [SerializeField] private Settings _settings;
    [SerializeField] private GeneratePlatesField _generatePlatesField;
    [SerializeField] private GridLayoutGroup _gridLayout;
    private Plates[,] _plates;   

    public event UnityAction GameOver;
    public event UnityAction StartedGame;
    public event UnityAction<Vector3> FindetStartPosition;    

    private void SetSize()
    {
        with = _settings.MapSize;
        hight = _settings.MapSize;
        _gridLayout.constraintCount = _settings.MapSize;
    }

    public void SpawnGrid()
    {
        SetSize();
        _plates = _generatePlatesField.SpawnPlates(_settings.BombsAmount, with, hight);
        Subscribe();
        StartedGame?.Invoke();
        StartCoroutine(WaitAndOpenRandomZeros(0.2f));
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
            Destroy(child.gameObject);
        }
        UnSubscribe();
    }

    private void Subscribe()
    {
        foreach (var plate in _plates)
        {
            plate.Opened += OnOpened;
            plate.GameOver += OpenAllBombs;
            plate.GameOver += BlockAllPlates;
            plate.GameOver += FindAllFalseBombMark;
            plate.GameOver += OnGameOver;      
            plate.Winable += CheckWin;
            plate.PressedOnNumber += OnPressedOnNumber;
        }
    }
    

    private void UnSubscribe()
    {
        foreach (var plate in _plates)
        {
            plate.Opened -= OnOpened;
            plate.GameOver -= OpenAllBombs;
            plate.GameOver -= BlockAllPlates;
            plate.GameOver -= FindAllFalseBombMark;
            plate.GameOver -= OnGameOver;
            plate.Winable -= CheckWin;
            plate.PressedOnNumber -= OnPressedOnNumber;
        }
    }

    private IEnumerator WaitAndOpenRandomZeros(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        TryOpenRandomZeros();        
    }

    private IEnumerator OpenNeerbyZerosSlow(int x, int y)
    {
        yield return null;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].NearbyBobmAmount == 0)
                        if (_plates[i + x, j + y].IsCheked == false && _plates[i + x, j + y].IsBombMark == false)
                        {                                                       
                            _plates[i + x, j + y].MarkToOpen();
                            ShowPlates(i + x, j + y);
                            yield return OpenNeerbyZerosSlow(i + x, j + y);
                        }        
        
    }

    private void OpenNeerbyZeros(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].NearbyBobmAmount == 0)
                        if (_plates[i + x, j + y].IsCheked == false && _plates[i + x, j + y].IsBombMark == false)
                        {
                            _plates[i + x, j + y].MarkToOpen();
                            ShowPlates(i + x, j + y);
                            OpenNeerbyZeros(i + x, j + y);
                        }
    }

    private void TryOpenRandomZeros()
    {
        int x, y;
        int maxTryAmount = with * hight;
        do
        {
           x = Random.Range(0, with);
           y = Random.Range(0, hight);
            maxTryAmount--;

            if (maxTryAmount < 0)
            {
                return;
            }

        } while (_plates[x,y].NearbyBobmAmount > 0 || _plates[x,y].IsBomb);
        
        _plates[x, y].Open();
        _plates[x, y].OpenedIvent();
        FindetStartPosition?.Invoke(_plates[x, y].transform.position);

    }

    private void SetWinInGame()
    {
        BlockAllPlates();
        OpenAllNoBombs();
        OnGameOver();
    }

    private void OnGameOver()
    {
        GameOver?.Invoke();
    }

    private void OnPressedOnNumber(Vector2Int position)
    {
        if (GetNearbyMarkAmount(position.x, position.y) ==
            _plates[position.x, position.y].NearbyBobmAmount)
        {
            TryOpenNearby(position.x, position.y);
        }
    }

    private void TryOpenNearby(int x, int y)
    {        
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y) && _plates[i + x, j + y].IsBombMark == false)
                    _plates[i + x, j + y].SimulatePressingLeft();        
    }

    private void CheckWin()
    {
        int counter = 0;
        foreach (var plate in _plates)
        {
            if (plate.IsBomb && plate.IsBombMark)
                counter++;
            if (!plate.IsBomb && plate.IsBombMark)
                counter--;
        }  
            
        if (counter == _settings.BombsAmount)
            SetWinInGame();
    }
    
    private void FindAllFalseBombMark()
    {
        foreach (var plate in _plates)
            if (plate.IsBombMark && !plate.IsBomb)
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
            if (plate.IsBomb && !plate.IsBombMark)        
                plate.Open();                          
    }
    private void OnOpened(Vector2Int position)
    {
        int x, y;   
        x = position.x;
        y = position.y;

        StartCoroutine(OpenNeerbyZerosSlow(x, y));            
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
                if (IsInside(i + x, j + y) && _plates[i + x, j + y].IsBombMark == false)
                    _plates[i + x, j + y].Open();         
    }    

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
