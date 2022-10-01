using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;
    [SerializeField] private PlatesGrid _platesGrid;

    private void OnEnable()
    {
        _platesGrid.PreGenerationComleated += OnPreGenerationComleated;
        _platesGrid.ClearingComleated += OnClearingComleated;
        _platesGrid.StartedGame += OnStartedGame;
    }

    private void OnDisable()
    {
        _platesGrid.PreGenerationComleated -= OnPreGenerationComleated;
        _platesGrid.ClearingComleated -= OnClearingComleated;
        _platesGrid.StartedGame -= OnStartedGame;
    }

    private void OnStartedGame() => Hide();

    private void OnClearingComleated() => Hide();

    private void OnPreGenerationComleated() => Hide();

    public void Show()
    {
        _loadingImage.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _loadingImage.gameObject.SetActive(false);
    }
}
