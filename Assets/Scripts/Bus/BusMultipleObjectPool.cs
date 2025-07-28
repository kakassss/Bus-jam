using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BusMultipleObjectPool : MultipleBaseObjectPool<Bus>, IDisposable
{
    private BusObjectPoolEvent _busObjectPoolEvent;
    
    protected BusMultipleObjectPool(IInstantiator instantiator, List<Bus> prefabs, Transform spawnParent, List<int> poolSize, List<int> indexes, BusObjectPoolEvent busObjectPoolEvent) 
        : base(instantiator, prefabs, spawnParent, poolSize, indexes)
    {
        _busObjectPoolEvent = busObjectPoolEvent;
        _busObjectPoolEvent.AddDeactivatedEvent(ReturnObjectToPool);
    }
    
    public void Dispose()
    {
        _busObjectPoolEvent.RemoveDeactivatedEvent(ReturnObjectToPool);
    }
}