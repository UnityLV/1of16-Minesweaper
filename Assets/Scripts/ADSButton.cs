using UnityEngine;
using YG;

public sealed class ADSButton : MonoBehaviour
{
    private const float AdRate = 180f;
    private float _adTimer;


    private void OnEnable()
    {
        YandexGame.CloseFullAdEvent += OnCloseAd;
    }

    private void Start()
    {
        _adTimer = AdRate;
    }

    void Update()
    {
        _adTimer -= Time.deltaTime;
    }

    private void OnDisable()
    {
        YandexGame.CloseFullAdEvent -= OnCloseAd;
    }

    public void TryShowADS()
    {
        if (_adTimer <= 0)
        {
            ShowADS();
        }
    }

    private void ShowADS()
    {
        YandexGame.FullscreenShow(); 
    }

    private void OnCloseAd()
    {
        _adTimer = AdRate;
    }
    
   
}
