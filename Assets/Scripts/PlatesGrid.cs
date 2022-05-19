using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesGrid : MonoBehaviour
{
    [SerializeField] int with = 10;
    [SerializeField] int hight = 10;        
    [SerializeField] private int _bombAmount=20;
    [SerializeField] private GeneratePlatesField _generatePlatesField;
    private Plates[,] _plates;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenRandomZeros();
        }
    }

    void Awake()
    {        
        _plates = _generatePlatesField.SpawnPlates(_bombAmount, with, hight);
    }
    private void OnEnable()
    {
        foreach (var plate in _plates)
        {
            plate.Opened += OnOpened;
            plate.GameOver += OpenAllBombs;
            plate.GameOver += BlockAllPlates;
            plate.GameOver += FindAllFalseBombMark;
            plate.Winable += CheckWin;
        }

    }
    private void OnDisable()
    {
        foreach (var plate in _plates)
        {
            plate.Opened -= OnOpened;
            plate.GameOver -= OpenAllBombs;
            plate.GameOver -= BlockAllPlates;
            plate.GameOver -= FindAllFalseBombMark;
            plate.Winable -= CheckWin;
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

    private void OpenRandomZeros()
    {
        int x, y;
        do
        {
           x = Random.Range(0, with);
           y = Random.Range(0, hight);

        } while (_plates[x,y].NearbyBobmAmount>0 && _plates[x,y].IsBomb);
        _plates[x, y].Open();
        _plates[x, y].OpenedIvent();

    }

    private void SetWinInGame()
    {
        BlockAllPlates();
        OpenAllNoBombs();
        Debug.Log("Win In Game");
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
            
        if (counter == _bombAmount)
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

        OpenNeerbyZeros(x, y);      
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
    private bool IsInside(int x, int y) =>
        x >= 0 && x < _plates.GetLength(0) &&
        y >= 0 && y < _plates.GetLength(1);
}
