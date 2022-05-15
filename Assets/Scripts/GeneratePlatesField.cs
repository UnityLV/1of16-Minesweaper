using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatesField : MonoBehaviour
{
    [SerializeField] private Plates _platePrefab;
    [SerializeField] private GenerateBombs _GenerateBombs;
    [SerializeField] private GeneratorNumbers _GenerateNumbers;
    

    private Plates SpawnSinglePlate(int x, int y)
    {
        Vector3 position = new Vector3(x, y, 0);
        Plates plate = Instantiate(_platePrefab, position, Quaternion.identity, transform);
        return plate;
    }
    public Plates[,] SpawnPlates(int _bombAmount,int hight = 10, int with = 10)
    {
        Plates[,] plates = new Plates[hight, with];
        for (int x = 0; x < with; x++)
            for (int y = 0; y < hight; y++)
                plates[x, y] = SpawnSinglePlate(x, y);

        int[,] bombMap = _GenerateBombs.CreateBombsMap(plates, _bombAmount);
        int[,] numberMap = _GenerateNumbers.CreateNumbersMap(bombMap);

        InitAllPlates(bombMap, numberMap, plates);

        return plates;
    }
    private void InitAllPlates(int [,] bombMap,int [,] numberMap, Plates[,] plates)
    {
        for (int i = 0; i < plates.GetLength(0); i++)
            for (int j = 0; j < plates.GetLength(1); j++)
                plates[i, j].Init(bombMap[i, j] == -1, numberMap[i, j], new Vector2Int(i, j));
    } 

}
