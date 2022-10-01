using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text _bombAmountText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _lifeTime = 2;
    [SerializeField] private float _speed = 10f;
    private float _fateTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {       
        _fateTime += Time.deltaTime * _lifeTime;
        _canvasGroup.alpha = _curve.Evaluate(_fateTime);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void Init(int bombAmount)
    {
        _bombAmountText.text = bombAmount.ToString();
    }
}