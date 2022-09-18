using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BombsDeterminator : MonoBehaviour
{    
    public int[,] CreateBombsMap(Plates[,] plates, int bombAmount)
    {
        int[,] bombsMap = new int[plates.GetLength(0), plates.GetLength(1)];

        bombAmount = Mathf.Clamp(bombAmount, 0, bombsMap.GetLength(0) * bombsMap.GetLength(1));
        FillBombsMap(bombAmount, bombsMap);
        return bombsMap;
    }

    private void FillBombsMap(int bombAmount, int[,] bombsMap)
    {
        int x, y;
        for (int i = 0; i < bombAmount; i++)
        {
            do
            {
                x = Random.Range(0, bombsMap.GetLength(0));
                y = Random.Range(0, bombsMap.GetLength(1));
            }
            while (bombsMap[x, y] == -1);

            bombsMap[x, y] = -1;
        }
    }   
}
