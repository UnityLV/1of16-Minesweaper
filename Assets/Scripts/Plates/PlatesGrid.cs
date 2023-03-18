using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public sealed class PlatesGrid : MonoBehaviour
{
    [SerializeField] private int _with;
    [SerializeField] private int _hight;
    [SerializeField] private Settings _settings;
    [SerializeField] private GeneratePlatesField _generatePlatesField;    
    private Plates[,] _plates;          

    public int MarkedBombs { get; private set; }
    public int FalseMarkedBombs { get; private set; }

    public event UnityAction PreGenerationComleated;   
    public event UnityAction ClearingComleated;       
    public event UnityAction GameOver;
    public event UnityAction LoseInGame;
    public event UnityAction WinInGame;
    public event UnityAction StartedGame;    
    public event UnityAction<Vector3> FindetStartPosition;
    public event UnityAction OpenNeerbyZerosCoroutineStarted;
    public event UnityAction OpenNeerbyZerosCoroutineStoped;
    public event UnityAction<bool,Vector2Int> PlatesMarkChanged;

    private void Start()
    {
        //PreGenerateField();
        PreGenerationComleated?.Invoke();
    }

    private void PreGenerateField()
    {
        _plates = _generatePlatesField.SpawnPlates(_settings.BombsAmount,_settings.MaxMapSize, _settings.MaxMapSize);
        Clear();
    }

    public void SpawnGrid()
    {
        SetSize();       
        _plates = _generatePlatesField.SpawnPlates(_settings.BombsAmount, _with, _hight);
       
        Subscribe();
        StartedGame?.Invoke();
        StartCoroutine(WaitAndOpenRandomZeros(0.1f));
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
        foreach (var plate in _plates)
        {            
            plate.Deactivate();                       
        }
        MarkedBombs = 0;
        FalseMarkedBombs = 0;
        UnSubscribe();
        ClearingComleated?.Invoke();
    }

    private void Subscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero += OnOpenedZeros;
            plate.PressedOnBomb += OnPressedOnBomb;
            plate.MarkChanged += OnMarkChanged;
            plate.PressedOnNumber += OnPressedOnNumber;            
        }
    }    

    private void UnSubscribe()
    {
        foreach (var plate in _plates)
        {
            plate.OpenedZero -= OnOpenedZeros;
            plate.PressedOnBomb -= OnPressedOnBomb;           
            plate.MarkChanged -= OnMarkChanged;
            plate.PressedOnNumber -= OnPressedOnNumber;
        }
    }

    private void OnPressedOnBomb()
    {
        OpenAllBombs();
        SetAllFalseBombMark();
        InvokeLoseInGame();
        SetGameOverOnAllPlates();        
    }

    private void OpenAllBombs()
    {
        foreach (var plate in _plates)
            if (plate.IsBomb && plate.IsBombMark == false)
                plate.ShowBomb();
    }

    private void SetAllFalseBombMark()
    {
        foreach (var plate in _plates)
            if (plate.IsBombMark && plate.IsBomb == false)
                plate.SetFalseBombMark();
    }

    private void InvokeLoseInGame()
    {
        SetGameOver();
        LoseInGame?.Invoke();
    }

    private IEnumerator WaitAndOpenRandomZeros(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        TryOpenRandomZeros();        
    }

    private IEnumerator OpenNeerbyZerosSlowly(int x, int y)
    {
        OpenNeerbyZerosCoroutineStarted?.Invoke();
        
        yield return null;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    if (_plates[i + x, j + y].NearbyBobmAmount == 0)
                        if (_plates[i + x, j + y].IsBombMark == false && _plates[i + x, j + y].IsCheked == false)
                        {                            
                            ApplyToNeighbors(i + x, j + y, OpenPlates);
                            _plates[i + x, j + y].Chek();
                            yield return OpenNeerbyZerosSlowly(i + x, j + y);
                        }
        OpenNeerbyZerosCoroutineStoped?.Invoke();
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
        
        _plates[x, y].PlayerPressingLeftMouseButton();
        FindetStartPosition?.Invoke(_plates[x, y].transform.position);
    }    

    private void OnMarkChanged(bool isBombMark, Vector2Int position)
    {
        if (_plates[position.x, position.y].IsBomb)
            MarkedBombs += isBombMark ? 1 : -1;
        else
            FalseMarkedBombs += isBombMark ? 1 : -1;
        PlatesMarkChanged?.Invoke(isBombMark, position);
        CheckWin();
    }

    private void OnPressedOnNumber(Vector2Int position)
    {
        if (IsAvalableToOpenAroundNuber(position))        
            ApplyToNeighbors(position.x, position.y, OpenPlateAsPlayer); 
    }

    private bool IsAvalableToOpenAroundNuber(Vector2Int position) => 
        GetNearbyMarkAmount(position.x, position.y) == _plates[position.x, position.y].NearbyBobmAmount;

    private void OpenPlates(Plates plate) => plate.SimulatePressingLeftMouseButton();

    private void OpenPlateAsPlayer(Plates plate) => plate.PlayerPressingLeftMouseButton();

    private void ApplyToNeighbors(int x, int y,Action<Plates> method)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y))
                    method(_plates[i + x, j + y]);
    }       

    private void CheckWin()
    {
        if (MarkedBombs == _settings.BombsAmount && FalseMarkedBombs == 0)
            SetWinInGame();
    }

    private void SetWinInGame()
    {
        InvokeWinInGame();
        OpenAllNumber();
    }

    private void InvokeWinInGame()
    {
        SetGameOver();
        WinInGame?.Invoke();
    }

    private void SetGameOver()
    {
        SetGameOverOnAllPlates();
        StopAllCoroutines();
        GameOver?.Invoke();
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
    
    private void OnOpenedZeros(Vector2Int position)
    {
        StartCoroutine(OpenNeerbyZerosSlowly(position.x, position.y));
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
