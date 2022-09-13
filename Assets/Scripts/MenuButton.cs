using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private PlatesGrid _platesGrid;
    [SerializeField] private RectTransform[] _menu;
    [SerializeField] private RectTransform[] _noMenu;
    public void LoadMenu()
    {
        _platesGrid.Clear();

        foreach (var uiElement in _menu)
        {
            uiElement.gameObject.SetActive(true);            
        }
        foreach (var uiElement in _noMenu)
        {
            uiElement.gameObject.SetActive(false);            
        }
    }

}
