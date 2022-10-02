using UnityEngine;

public sealed class MenuView : MonoBehaviour
{
    [SerializeField] private PlatesGrid _grid;    
    [SerializeField] private RectTransform _restartButton;
    [SerializeField] private RectTransform _startButton;
    [SerializeField] private RectTransform _settingsMenu;
    [SerializeField] private RectTransform _menuButton;
    

    private void OnEnable()
    {
        _grid.GameOver += OnGameOver;
        _grid.StartedGame += OnGameStart;
    }

    private void OnDisable()
    {
        _grid.GameOver -= OnGameOver;
        _grid.StartedGame -= OnGameStart;
    }

    private void OnGameStart()
    {
        _menuButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _settingsMenu.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        _menuButton.gameObject.SetActive(true);        
        _restartButton.gameObject.SetActive(true);        
    }
    
}
