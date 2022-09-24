using UnityEngine;

public sealed class GeneratePlatesField : MonoBehaviour
{
    [SerializeField] private Plates _platePrefab;
    [SerializeField] private BombsDeterminator _bombsDeterminator = new();
    [SerializeField] private PlatesFiller _platesFiller = new();
    private ObjectPooler<Plates> objectPooler;

    private void Awake()
    {
        objectPooler = new ObjectPooler<Plates>(Instantiate, _platePrefab);
    }

    public Plates[,] SpawnPlates(int _bombAmount,int hight = 10, int with = 10)
    {
        Plates[,] plates = SpawnPlates(hight, with);

        int[,] bombMap = _bombsDeterminator.CreateBombsMap(plates, _bombAmount);
        FillingPlates[,] numberMap = _platesFiller.GetFillingMap(bombMap);

        InitAllPlates(numberMap, plates);

        return plates;
    }

    private Plates[,] SpawnPlates(int hight, int with)
    {
        Plates[,] plates = new Plates[hight, with];
        for (int x = 0; x < with; x++)
            for (int y = 0; y < hight; y++)
                plates[x, y] = SpawnSinglePlate(x, y);
        return plates;
    }

    private Plates SpawnSinglePlate(int x, int y)
    {
        Plates plate = objectPooler.GetPooleable() as Plates;

        Vector3 position = new Vector3(x, y, 0);        
        plate.transform.position = position;
        plate.transform.SetParent(transform);
        plate.transform.localScale = Vector3.one;
        plate.Activate();
        return plate;
    }

    private void InitAllPlates(FillingPlates[,] numberMap, Plates[,] plates)
    {
        for (int i = 0; i < plates.GetLength(0); i++)
            for (int j = 0; j < plates.GetLength(1); j++)
                plates[i, j].Init(numberMap[i, j], new Vector2Int(i, j));
    } 

}
