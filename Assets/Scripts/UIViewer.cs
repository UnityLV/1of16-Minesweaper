using UnityEngine;
using System.Linq;

public class UIViewer : MonoBehaviour
{
    [SerializeField] private RectTransform[] _allUiElements;

    public void DeactivateAll()
    {
        foreach (var uiElement in _allUiElements)
        {
            uiElement.gameObject.SetActive(false);
        }
    }

	public void LoadUI(RectTransform uiToActivate)
    {
        foreach (var uiElement in _allUiElements)
        {
            if (uiElement == uiToActivate)
            {
                uiElement.gameObject.SetActive(true);
            }
        }
    }
}
