using UnityEngine;

public sealed class Confinder : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygon;
    [SerializeField] private Settings _settings;
    [SerializeField] private PlatesGrid _platesGrid;
    public int Size { get; private set; } = 5001;

    
    private void OnEnable()
    {
        _settings.MapSizeChanged += OnMapSizeChanged;
        _platesGrid.StartedGame += OnGameStarted;
    }

    

    private void OnDisable()
    {
        _settings.MapSizeChanged -= OnMapSizeChanged;
        _platesGrid.StartedGame -= OnGameStarted;

    }

    private void OnGameStarted()
    {
        if (_settings.MapSize > 10)
        {
            SetCollidersPoints(Size);
        }
    }

    private void OnMapSizeChanged(int value)
    {
        Size = ((value - 5) * 1000) + 1;        
    }        

    public void SetDefault()
    {
        SetCollidersPoints(5001);
    }

    private void SetCollidersPoints(int value)
    {
        var point1 = new Vector2(value, 5001);
        var point2 = new Vector2(-5001, 5001);
        var point3 = new Vector2(-5001, -value);
        var point4 = new Vector2(value, -value);        


        _polygon.points = new[] { point1, point2, point3, point4};
        _polygon.SetPath(0, new[] { point1, point2, point3 , point4});       

    }
}
