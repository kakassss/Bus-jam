using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPoolInstaller : MonoInstaller
{
    [Header("Multiple Bus Pool Settings")]
    [SerializeField] private List<Bus> _busPrefabs;
    [SerializeField] private Transform _busParent;
    [SerializeField] private List<int> _busPoolsSizes;
    private List<int> _poolSizeIndexes;
    
    public override void InstallBindings()
    {
        BindBusPool();
    }

    private void BindBusPool()
    {
        SetPoolSizesIndexes();
        Container.Bind<BusMultipleObjectPool>().AsSingle().WithArguments(_busPrefabs, _busParent, _busPoolsSizes, _poolSizeIndexes).NonLazy();
        Container.Bind<BusObjectPoolEvent>().AsSingle().NonLazy();
    }
    
    private void SetPoolSizesIndexes()
    {
        _poolSizeIndexes = new List<int>();
        for (int i = 0; i < _busPoolsSizes.Count; i++)
        {
            _poolSizeIndexes.Add(i);
        }        
    }
    
}