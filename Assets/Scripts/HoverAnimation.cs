using UnityEngine;
using UnityEngine.Events;

public class HoverAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite1;
    [SerializeField] private SpriteRenderer _sprite2;
    [SerializeField] private AnimationCurve _alphaColor;
    [SerializeField] private float _fateTimeSpeed = 0.2f;
    [SerializeField] private Color _color1;      
    private float _fateTime;
    private bool _isAlredyDesapierd;

    public event UnityAction<HoverAnimation> Desapierd;   

    private void Update()
    {
        SmoothDisapier();
    }

    private void SmoothDisapier()
    {
        _fateTime += Time.deltaTime * _fateTimeSpeed;

        float alpha = _alphaColor.Evaluate(_fateTime);

        _sprite1.color = new Color(_color1.r, _color1.g, _color1.b, alpha);
        _sprite2.color = new Color(1, 1, 1, alpha);

        if (_isAlredyDesapierd == false && alpha <= 0)
        {
            _isAlredyDesapierd = true;
            Desapierd?.Invoke(this);
        }
    } 
}
