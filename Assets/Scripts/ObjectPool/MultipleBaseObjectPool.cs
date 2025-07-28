using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MultipleBaseObjectPool<T> : BaseObjectPool where T : Component
{
    private List<MultipleObjectPoolData<T>> _multiplePoolDataList;
    
    private readonly List<T> _prefabs;
    private readonly List<int> _indexes; 
    private readonly List<int> _poolSize;
    
    protected MultipleBaseObjectPool(IInstantiator instantiator,  List<T> prefabs, Transform spawnParent, List<int> poolSize,List<int> indexes)
        : base(instantiator)
    {
        _instantiator = instantiator;
        _spawnParent = spawnParent;
        _prefabs = prefabs;
        _poolSize = poolSize;
        _indexes = indexes;
        
        MultiplePrefabObjectPool(_prefabs, _spawnParent, _poolSize, _indexes);
    }
    
    private void MultiplePrefabObjectPool(List<T> prefabs,Transform parent, List<int> poolSize, List<int> index)
    {
        _multiplePoolDataList = new List<MultipleObjectPoolData<T>>();
        
        for (int i = 0; i < index.Count; i++)
        {
            for (int j = 0; j < poolSize[i]; j++)
            { 
                MultipleObjectPoolData<T> poolData = 
                    new MultipleObjectPoolData<T>(_instantiator.InstantiatePrefabForComponent<T>(prefabs[i], parent),i);
                
                _multiplePoolDataList.Add(poolData);
            }    
        }
        
        SetMultiplePoolObjects();
    }
    
    private void SetMultiplePoolObjects()
    {
        foreach (var poolData in _multiplePoolDataList)
        {
            poolData.Prefab.gameObject.SetActive(false);
        }
    }
    
    public T GetObjectFromPool(int poolID, Vector3 position)
    {
        var poolList = _multiplePoolDataList.Where(data => data.ID == poolID);
        foreach (var data in poolList)
        {
            _multiplePoolDataList.Remove(data);
            data.Prefab.gameObject.SetActive(true);
            data.Prefab.transform.position = position;
            return data.Prefab;
        }
        
        Debug.LogError("Insufficient pool data for this pool");
        return null;
    }
    
    protected void ReturnObjectToPool(T poolData, int poolID)
    {
        poolData.gameObject.SetActive(false);
        poolData.transform.position = Vector3.zero;
        
        MultipleObjectPoolData<T> oldData = new MultipleObjectPoolData<T>(poolData, poolID);
        _multiplePoolDataList.Add(oldData);
    }
}
