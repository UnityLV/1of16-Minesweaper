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
        _plate.FalseBombMarkFinded += ShowFalseBombMark;
        _plate.ShowedBombs += OnOpenedBombs;
        _plate.Deactivation += OnDeactivation;
    }
    

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
        if (isBomb)
        {
            _spriteRenderer.sprite = isBomb ? _openImageBobm : _openImage;
        }
        else
        {
            _spriteRenderer.sprite = _bin[nearbyBobmAmount];
        }       
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