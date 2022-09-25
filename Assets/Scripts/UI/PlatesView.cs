using UnityEngine;
using UnityEngine.UI;
using TMPro;


public sealed class PlatesView : MonoBehaviour
{
    [SerializeField] private Plates _plate;    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openImageBobm;
    [SerializeField] private Sprite _openImage;
    [SerializeField] private Sprite _BombMark;
    [SerializeField] private Sprite _DefaultSprite;
    [SerializeField] private Sprite _firstBomb;
    [SerializeField] private Sprite _falseBombMark;
    [SerializeField] private SpriteBin _bin;
    [SerializeField] private int _color;
   

    
    private void OnEnable()
    {
        _plate.LeftClick += OnClickLeft;
        _plate.FirstBombPressed += FirstBompPaintToRed;
        _plate.RightClick += OnClickRight;
<<<<<<< HEAD
        _plate.FalseBombMarkFinded += ShowFalseBombMark;
        _plate.ShowedBombs += OnOpenedBombs;
        _plate.Deactivation += OnDeactivation;
    }
    
=======
        _plate.FalseBombMark += ShowFalseBombMark;
        _plate.Deactivation += OnDeactivation;
    }

    private void OnDeactivation(IPooleable arg0)
    {
        _image.sprite = _DefaultSprite;
        _text.text = string.Empty;
    }
>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9

    private void OnDisable()
    {
        _plate.LeftClick -= OnClickLeft;
        _plate.FirstBombPressed -= FirstBompPaintToRed;
        _plate.RightClick -= OnClickRight;
        _plate.FalseBombMarkFinded -= ShowFalseBombMark;
        _plate.ShowedBombs -= OnOpenedBombs;
        _plate.Deactivation -= OnDeactivation;

    }

    private void OnDeactivation(IPooleable plate)
    {
        _spriteRenderer.sprite = _DefaultSprite;        
    }

    private void FirstBompPaintToRed()
    {
        _spriteRenderer.sprite = _firstBomb;
    }
    private void ShowFalseBombMark()
    {
        _spriteRenderer.sprite = _falseBombMark;
    }

    private void OnClickLeft(bool isBomb, int nearbyBobmAmount)
    {
<<<<<<< HEAD
        if (isBomb)
        {
            _spriteRenderer.sprite = isBomb ? _openImageBobm : _openImage;
        }
        else
        {
            _spriteRenderer.sprite = _bin[nearbyBobmAmount];
        }       
=======
        _text.text = isBomb ? string.Empty : nearbyBobmAmount.ToString();
        _text.color = _bin[nearbyBobmAmount];
        _image.sprite = isBomb ? _openImageBobm : _openImage;
>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9
    }
    private void OnClickRight(bool BombMark)
    {
        _spriteRenderer.sprite = BombMark ? _spriteRenderer.sprite = _BombMark : _spriteRenderer.sprite = _DefaultSprite;
    }

    private void OnOpenedBombs()
    {
        if (_plate.IsBomb)
        {
            _spriteRenderer.sprite = _openImageBobm;
        }        
    }
}