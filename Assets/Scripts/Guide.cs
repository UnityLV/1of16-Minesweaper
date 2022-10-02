using UnityEngine;
using YG;

public abstract class Guide : MonoBehaviour
{
    [SerializeField] protected PlatesGrid PlatesGrid;
    [SerializeField] protected RectTransform UI;
    protected virtual void OnEnable()
    {
        YandexGame.GetDataEvent += OnGetData;
    }

    protected virtual void OnDisable()
    {
        YandexGame.GetDataEvent -= OnGetData;
    }    

    private void OnGetData()
    {
        if (YandexGame.savesData.isFirstSession == false)
        {
            Destroy(gameObject);
        }
    }    
}
