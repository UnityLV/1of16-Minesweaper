using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorNumbers : MonoBehaviour
{
    public int[,] CreateNumbersMap(int[,] bombMap)
    {
        for (int i = 0; i < bombMap.GetLength(0); i++)
            for (int j = 0; j < bombMap.GetLength(1); j++)
                bombMap[i, j] = GetNeerBombAmount(i, j, bombMap);
        return bombMap;
    }
    private int GetNeerBombAmount(int x, int y, int[,] bomb)
    {
        if (bomb[x, y] == -1)
            return -1;

        int couner = 0;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y, bomb))
                    if (bomb[i + x, j + y] == -1) 
                        couner++;
        return couner;
    }
    private bool IsInside(int x, int y, int[,] bomb) =>
        x >= 0 && x < bomb.GetLength(0) &&
        y >= 0 && y < bomb.GetLength(1);
}
