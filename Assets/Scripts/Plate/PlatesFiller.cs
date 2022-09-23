using UnityEngine;

public sealed class PlatesFiller 
{
    public FillingPlates[,] GetFillingMap(int[,] bombMap)
    {
        FillingPlates[,] fillingPlates = new FillingPlates[bombMap.GetLength(0), bombMap.GetLength(1)];
        for (int i = 0; i < bombMap.GetLength(0); i++)
            for (int j = 0; j < bombMap.GetLength(1); j++)
                fillingPlates[i, j] = GetFillingPlate(i, j, bombMap);
        return fillingPlates;
    }
    private FillingPlates GetFillingPlate(int x, int y, int[,] bomb)
    {
        int couner = 0;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y, bomb))
                    if (bomb[i + x, j + y] == -1) 
                        couner++;
        return new FillingPlates(couner, bomb[x, y] == -1);
    }
    private bool IsInside(int x, int y, int[,] bomb) =>
        x >= 0 && x < bomb.GetLength(0) &&
        y >= 0 && y < bomb.GetLength(1);
}
