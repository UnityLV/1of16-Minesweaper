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
        _plateGrid.StartedGame += OnStartedGame;
    }


    private void OnDisable()
    {
        _plateGrid.PlatesMarkChanged -= OnPlatesMarkChanged;
        _plateGrid.GameOver -= OnGameOver;
        _plateGrid.StartedGame -= OnStartedGame;
    }

    private void OnPlatesMarkChanged(bool isMark, Vector2Int position)
    {
        if (TryFindSamePosition(position, out HoverAnimation sameAnimation))
        {
            DeleteAnimation(sameAnimation);
            _animations.Remove(sameAnimation);
            return;
        }

        if (isMark)        
            Create(position);               
    }

    private void OnStartedGame()
    {
        ClearAllAnimations();
    }

    private void OnGameOver()
    {
        ClearAllAnimations();
    }

    private void ClearAllAnimations()
    {
        for (int i = 0; i < _animations.Count; i++)
        {
            DeleteAnimation(_animations[i]);            
        }
        _animations.Clear();
    }

    private bool TryFindSamePosition(Vector2Int position,out HoverAnimation sameAnimation)
    {
        foreach (var animation in _animations)
        {
            if (animation.transform.position.x == position.x && animation.transform.position.y == position.y)
            {
                sameAnimation = animation;
                return true;
            }
        }
        sameAnimation = default;
        return false;
    }

    private void Create(Vector2Int position)
    {
        Vector3 vector3Position = new(position.x, position.y, _zOffset);
        var animation = Instantiate(_animationPrefab, vector3Position, Quaternion.identity);
        animation.Desapierd += OnAnimationDisapierd;
        _animations.Add(animation);
    }

    private void OnAnimationDisapierd(HoverAnimation animation)
    {
        DeleteAnimation(animation);
        _animations.Remove(animation);
    }

    private void DeleteAnimation(HoverAnimation animation)
    {
        animation.Desapierd -= OnAnimationDisapierd;        
        Destroy(animation.gameObject);
    }
}
