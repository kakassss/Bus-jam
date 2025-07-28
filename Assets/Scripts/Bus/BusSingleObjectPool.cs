using System;
using UnityEngine;
using Zenject;

public class BusSingleObjectPool : SingleBaseObjectPool<Bus>, IDisposable
{
    private BusObjectPoolEvent _busObjectPoolEvent;
    
    protected BusSingleObjectPool(IInstantiator instantiator, GameObject prefab, Transform spawnParent, int poolSize, BusObjectPoolEvent busObjectPoolEvent)
        : base(instantiator, prefab, spawnParent, poolSize)
    {
        _busObjectPoolEvent = busObjectPoolEvent;
        
        _busObjectPoolEvent.AddDeactivatedEvent(ReturnObjectsToPool);
    }
    
    public void Dispose()
    {
        _busObjectPoolEvent.RemoveDeactivatedEvent(ReturnObjectsToPool);
    }
}