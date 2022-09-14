using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBombs : MonoBehaviour
{
    
    public int[,] CreateBombsMap(Plates[,] plates, int _bombAmount)
    {
        int [,] bombsMap= new int[plates.GetLength(0), plates.GetLength(1)];
        _bombAmount = Mathf.Clamp(_bombAmount, 0, bombsMap.GetLength(0) * bombsMap.GetLength(1));

        int x, y;
        for (int i = 0; i < _bombAmount; i++)
        {
            do
            {
                x = Random.Range(0, bombsMap.GetLength(0));
                y = Random.Range(0, bombsMap.GetLength(1));
            }
            while (bombsMap[x, y] == -1);

            bombsMap[x, y] = -1;
        }
        return bombsMap;
    }
    
}
