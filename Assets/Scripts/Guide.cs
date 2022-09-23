using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Guide : MonoBehaviour
{
    [SerializeField] private GuideView _guidePrefab;
    private GuideView _guide;

        
    public void TryShow()
    {
        if (YandexGame.savesData.isGuideCompleated == false)
        {
            _guide = Instantiate(_guidePrefab, transform);
            _guide.Closed += Close;
        }
    }

    public void Close()
    {
        _guide.Closed -= Close;
        Save();
    }

    private void Save()
    {
        YandexGame.savesData.isGuideCompleated = true;
    }
}
