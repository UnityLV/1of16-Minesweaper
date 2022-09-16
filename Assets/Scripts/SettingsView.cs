using UnityEngine;
using TMPro;

public class SettingsView : MonoBehaviour
{
	[SerializeField] private Settings _settings;
	[SerializeField] private TMP_Text _bombsAmountText;
	[SerializeField] private TMP_Text _mapSizeText;

    private void OnEnable()
    {
        _settings.BombsAmountChanged += OnBombsAmountChanged;
        _settings.MapSizeChanged += OnMapSizeChanged;
	}

    private void OnDisable()
    {
		_settings.BombsAmountChanged -= OnBombsAmountChanged;
		_settings.MapSizeChanged -= OnMapSizeChanged;
	}

    private void OnBombsAmountChanged(int value)
    {
		_bombsAmountText.text = value.ToString();

	}
    private void OnMapSizeChanged(int value)
    {
		_mapSizeText.text = value.ToString();

	}
}
