using UnityEngine;
using UnityEngine.UI;

public sealed class RandomShadow: MonoBehaviour
{
    [Range(0,1)] [SerializeField] private float _maxShadow;
    [SerializeField] private Color _shadowColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Color color = new Color(_shadowColor.r, _shadowColor.g, _shadowColor.b);
        color.a = Random.Range(0f, _maxShadow);
        _spriteRenderer.color = color;
    }
}
