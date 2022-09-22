using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text _bombAMountText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AnimationCurve _curve;
    private float _lifeTime = 2;
    private float _speed = 10f;
    private float _fateTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
        _fateTime += Time.deltaTime;
        _canvasGroup.alpha = _curve.Evaluate(_fateTime);
    }

    public void Init(int bombamount)
    {
        _bombAMountText.text = bombamount.ToString();
    }
}