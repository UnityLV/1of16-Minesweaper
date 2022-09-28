using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMarkAnimation : MonoBehaviour
{
    [SerializeField] private PlatesGrid _plateGrid;
    [SerializeField] private HoverAnimation _animationPrefab;
    private float _zOffset = -1f;
    private List<HoverAnimation> _animations = new();

    private void OnEnable()
    {
        _plateGrid.PlatesMarkChanged += OnPlatesMarkChanged;
        _plateGrid.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _plateGrid.PlatesMarkChanged -= OnPlatesMarkChanged;
        _plateGrid.GameOver -= OnGameOver;
    }

    private void OnPlatesMarkChanged(bool isMark, Vector2Int position)
    {
        if (isMark)
        {
            Create(position);
        }
        else
        {
            var alredyExistingAnimation = TryFindSamePosition(position);
            if (alredyExistingAnimation != null)
            {
                Destroy(alredyExistingAnimation.gameObject);
            }
            
        }
    }

    private void OnGameOver()
    {
        ClearAllAnimations();
    }

    private void ClearAllAnimations()
    {
        for (int i = 0; i < _animations.Count; i++)
            if (_animations[i].gameObject != null)
                Destroy(_animations[i].gameObject);

        _animations.Clear();
    }

    private HoverAnimation TryFindSamePosition(Vector2Int position)
    {
        return _animations.Find(a => a.transform.position.x == position.x && a.transform.position.y == position.y);
    }

    private void Create(Vector2Int position)
    {
        Vector3 vector3Position = new(position.x, position.y, _zOffset);
        var animation = Instantiate(_animationPrefab, vector3Position, Quaternion.identity);       
        _animations.Add(animation);
    }
}
