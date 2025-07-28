using UnityEngine;
using Zenject;

public class BaseObjectPool 
{
    protected IInstantiator _instantiator;
    protected Transform _spawnParent;

    protected BaseObjectPool(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }
}
