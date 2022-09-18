using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class Settings : MonoBehaviour
{
	[SerializeField] private Slider _bombsAmountSlider;
	[SerializeField] private Slider _mapSizeSlider;

	private int _targetFPS = 60;

	public event UnityAction<int> BombsAmountChanged;
	public event UnityAction<int> MapSizeChanged;
	[field: SerializeField] public int MapSize { get; private set; } = 10;
	[field: SerializeField] public int BombsAmount { get; private set; } = 10;


    private void Awake() => Application.targetFrameRate = _targetFPS;

    private void OnEnable()
    {
		_bombsAmountSlider.onValueChanged.AddListener(delegate { OnBombAmountChanged(); });
		_mapSizeSlider.onValueChanged.AddListener(delegate { OnMapSizeChanged(); });
	}

    private void OnDisable()
    {
		_bombsAmountSlider.onValueChanged.RemoveAllListeners();
		_mapSizeSlider.onValueChanged.RemoveAllListeners();
	}

    public void OnBombAmountChanged()
	{
		BombsAmount = Mathf.RoundToInt(_bombsAmountSlider.value);
		BombsAmountChanged?.Invoke(BombsAmount);		
	}

	public void OnMapSizeChanged()
	{
		MapSize = Mathf.RoundToInt(_mapSizeSlider.value);
		MapSizeChanged?.Invoke(MapSize);
	}

}
