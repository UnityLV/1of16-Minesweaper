using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class UILoader : MonoBehaviour
{
    [SerializeField] private RectTransform[] _menu;
    [SerializeField] private RectTransform[] _field;

    public void LoadMenu()
    {
        ShowMenu();
        HideField();
    }

    private void HideField()
    {
        foreach (var uiElement in _field)
            uiElement.gameObject.SetActive(false);
    }

    private void ShowMenu()
    {
        foreach (var uiElement in _menu)
            uiElement.gameObject.SetActive(true);
    }
}
