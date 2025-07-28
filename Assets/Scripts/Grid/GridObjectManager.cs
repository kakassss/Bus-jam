using TMPro;
using UnityEngine;
using Zenject;

public class GridObjectManager
{
    private GameObject _gridObject;
    private Transform _parentTransform;
    private IInstantiator _instantiator;
    private GridManager _gridManager;

    public GridObjectManager(GameObject gridObject,Transform parentTransform,IInstantiator instantiator, GridManager gridManager)
    {
        _gridObject = gridObject;
        _parentTransform = parentTransform;
        _instantiator = instantiator;
        _gridManager = gridManager;
    }
    
    public void InitializeObjects()
    {
        for (int i = 0; i < _gridManager.Grid.GetLength(0); i++)
        {
            for (int j = 0; j < _gridManager.Grid.GetLength(1); j++)
            {
                var grid = _instantiator.InstantiatePrefab(_gridObject, 
                    _gridManager.GetWorldPosition(i, j)+ new Vector3(1.5f,0,1.5f) * 0.5f, 
                    Quaternion.identity,_parentTransform);
                
                grid.GetComponent<TextMeshProUGUI>().text = i + " " + j;
                grid.transform.position += new Vector3(0, 3, 0);
                grid.transform.rotation = Quaternion.Euler(60,0,0);
            }
        }
    }
}