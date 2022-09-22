using UnityEngine;
using UnityEngine.UI;
using TMPro;


public sealed class PlatesView : MonoBehaviour
{
    [SerializeField] private Plates _plate;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _openImageBobm;
    [SerializeField] private Sprite _openImage;
    [SerializeField] private Sprite _BombMark;
    [SerializeField] private Sprite _DefaultSprite;
    [SerializeField] private Sprite _firstBomb;
    [SerializeField] private Sprite _falseBombMark;
    [SerializeField] private ColorBin _bin;

    
    private void OnEnable()
    {
        _plate.LeftClick += OnClickLeft;
        _plate.FirstBomb += FirstBompPaintToRed;
        _plate.RightClick += OnClickRight;
        _plate.FalseBombMark += ShowFalseBombMark;
    }
    private void OnDisable()
    {
        _plate.LeftClick -= OnClickLeft;
        _plate.FirstBomb -= FirstBompPaintToRed;
        _plate.RightClick -= OnClickRight;
        _plate.FalseBombMark -= ShowFalseBombMark;

    }
    private void FirstBompPaintToRed()
    {
        _image.sprite = _firstBomb;
    }
    private void ShowFalseBombMark()
    {
        _image.sprite = _falseBombMark;
    }

    private void OnClickLeft(bool isBomb, int nearbyBobmAmount)
    {        
        _text.text = isBomb ? string.Empty:nearbyBobmAmount.ToString();
        _text.color = _bin[nearbyBobmAmount];
        _image.sprite = isBomb ? _openImageBobm : _openImage;
    }
    private void OnClickRight(bool BombMark)
    {
        _image.sprite = BombMark ? _image.sprite = _DefaultSprite : _image.sprite = _BombMark;
    }
}