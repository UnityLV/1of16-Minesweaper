using UnityEngine;

public sealed class GeneratePlatesField : MonoBehaviour
{
    [SerializeField] private Plate _platePrefab;
    [SerializeField] private BombsDeterminator _bombsDeterminator = new();
    [SerializeField] private PlatesFiller _platesFiller = new();

    public Plate[,] SpawnPlates(int _bombAmount, int hight, int with)
    {
        Plate[,] plates = SpawnPlates(hight, with);

        int[,] bombMap = _bombsDeterminator.CreateBombsMap(plates, _bombAmount);
        FillingPlate[,] numberMap = _platesFiller.GetFillingMap(bombMap);

        InitAllPlates(numberMap, plates);

        return plates;
    }

    private Plate[,] SpawnPlates(int hight, int with)
    {
        Plate[,] plates = new Plate[hight, with];

        for (int x = 0; x < with; x++)
            for (int y = 0; y < hight; y++)
                plates[x, y] = SpawnSinglePlate(x, y);
        return plates;
    }

    private Plate SpawnSinglePlate(int x, int y)
    {
        Vector3 position = new Vector3(x, y, 0);
        Plate plate = Instantiate(_platePrefab, position, Quaternion.identity, transform);
        return plate;
    }

    private void InitAllPlates(FillingPlate[,] numberMap, Plate[,] plates)
    {
        for (int i = 0; i < plates.GetLength(0); i++)
            for (int j = 0; j < plates.GetLength(1); j++)
                plates[i, j].Init(numberMap[i, j], new Vector2Int(i, j));
    } 

}
