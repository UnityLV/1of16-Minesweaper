using UnityEngine;

public sealed class PlatesFiller 
{
    public FillingPlate[,] GetFillingMap(int[,] bombMap)
    {
        FillingPlate[,] fillingPlates = new FillingPlate[bombMap.GetLength(0), bombMap.GetLength(1)];

        for (int i = 0; i < bombMap.GetLength(0); i++)
            for (int j = 0; j < bombMap.GetLength(1); j++)
                fillingPlates[i, j] = GetFillingPlate(i, j, bombMap);
        return fillingPlates;
    }
    private FillingPlate GetFillingPlate(int x, int y, int[,] bombs)
    {
        int couner = 0;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (IsInside(i + x, j + y, bombs))
                    if (bombs[i + x, j + y] == -1) 
                        couner++;
        return new FillingPlate(couner, bombs[x, y] == -1);
    }
    private bool IsInside(int x, int y, int[,] bomb) =>
        x >= 0 && x < bomb.GetLength(0) &&
        y >= 0 && y < bomb.GetLength(1);
}
