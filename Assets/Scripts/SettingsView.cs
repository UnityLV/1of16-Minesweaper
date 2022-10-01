using UnityEngine;
using TMPro;

public sealed class SettingsView : MonoBehaviour
{
	[SerializeField] private Settings _settings;
	[SerializeField] private TMP_Text _bombsAmountText;
	[SerializeField] private TMP_Text _mapSizeText;

    private void OnEnable()
    {
        _settings.BombsPercentChanged += OnBombsPercentChanged;
        _settings.MapSizeChanged += OnMapSizeChanged;
	}

    private void OnDisable()
    {
		_settings.BombsPercentChanged -= OnBombsPercentChanged;
		_settings.MapSizeChanged -= OnMapSizeChanged;
	}

    private void OnBombsPercentChanged(int value) => _bombsAmountText.text = value.ToString() + "%";
    private void OnMapSizeChanged(int value)
    {
        _mapSizeText.text =$"{value}X{value}";
    }
}
