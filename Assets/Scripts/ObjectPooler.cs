using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler<T> where T : MonoBehaviour
{
    private T _template;
    private System.Func<T, IPooleable> _instantiete;
    private readonly Stack<IPooleable> _pool = new();

    public ObjectPooler(System.Func<T, IPooleable> instantiete, T template)
    {
        _instantiete = instantiete;
        _template = template;
    }


    public IPooleable GetPooleable()
    {
        if (_pool.TryPop(out IPooleable pooleable))
        {
            pooleable.Deactivation += OnDeactivation;
            return pooleable;
        }

        IPooleable poolable = _instantiete(_template);
        poolable.Deactivation += OnDeactivation;
        return poolable;
    }

    private void OnDeactivation(IPooleable pooleable)
    {
        pooleable.Deactivation -= OnDeactivation;

        _pool.Push(pooleable);
    }

}

