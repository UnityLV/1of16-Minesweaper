using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class Settings : MonoBehaviour
{
	[SerializeField] private Slider _bombsPercentSlider;
	[SerializeField] private Slider _mapSizeSlider;	

	private int _targetFPS = 60;

	public event UnityAction<int> BombsPercentChanged;
	public event UnityAction<int> MapSizeChanged;
	[field: SerializeField] public int MapSize { get; private set; } = 10;
	[field: SerializeField] public int MaxMapSize { get; private set; } = 200;
	[field: SerializeField] public int BombsPercent { get; private set; } = 10;
	public int BombsAmount => Mathf.Clamp((MapSize * MapSize) / (100 /BombsPercent),1, MaxMapSize * MaxMapSize);


    private void Awake() => Application.targetFrameRate = _targetFPS;

    private void OnEnable()
    {
		_bombsPercentSlider.onValueChanged.AddListener(delegate { OnBombPercentChanged(); });
		_mapSizeSlider.onValueChanged.AddListener(delegate { OnMapSizeChanged(); });
	}

    private void OnDisable()
    {
		_bombsPercentSlider.onValueChanged.RemoveAllListeners();
		_mapSizeSlider.onValueChanged.RemoveAllListeners();
	}

    public void OnBombPercentChanged()
	{
		BombsPercent = Mathf.RoundToInt(_bombsPercentSlider.value);
		BombsPercentChanged?.Invoke(BombsPercent);		
	}

	public void OnMapSizeChanged()
	{
		MapSize = Mathf.RoundToInt(_mapSizeSlider.value);
		MapSizeChanged?.Invoke(MapSize);
	}

}
