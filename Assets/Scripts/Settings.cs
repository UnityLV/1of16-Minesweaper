using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] private Slider _bombsAmountSlider;
	[SerializeField] private Slider _mapSizeSlider;
	[field: SerializeField] public int MapSize { get; private set; } = 10;
	[field: SerializeField] public int BombsAmount { get; private set; } = 10;

	public event UnityAction<int> BombsAmountChanged;
	public event UnityAction<int> MapSizeChanged;		

    private void Awake()
    {
		Application.targetFrameRate = 60;
	}

    private void OnEnable()
    {
		_bombsAmountSlider.onValueChanged.AddListener(delegate { OnBombAmountChanged(); });
		_mapSizeSlider.onValueChanged.AddListener(delegate { OnMapSizeChanged(); });
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
