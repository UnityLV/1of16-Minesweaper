<<<<<<< HEAD
﻿using System.Collections.Generic;
using UnityEngine;

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9
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

<<<<<<< HEAD
=======

>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9
    public IPooleable GetPooleable()
    {
        if (_pool.TryPop(out IPooleable pooleable))
        {
            pooleable.Deactivation += OnDeactivation;
            return pooleable;
        }

        IPooleable poolable = _instantiete(_template);
<<<<<<< HEAD
        poolable.Deactivation += OnDeactivation;        

=======
        poolable.Deactivation += OnDeactivation;
>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9
        return poolable;
    }

    private void OnDeactivation(IPooleable pooleable)
    {
        pooleable.Deactivation -= OnDeactivation;

        _pool.Push(pooleable);
    }

}
<<<<<<< HEAD
=======

>>>>>>> 7de605bb5e59d3b72c66221bbf3290c747d24cb9
