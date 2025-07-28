using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StickManObjectManager
{
    private LevelDataManager _levelDataManager;
    private PlayerDataManager _playerDataManager;
    
    private List<StickMan> _stickmen;
    private StickMan _stickManEntity;
    private GridManager _gridManager;
    private IInstantiator _instantiator;
    private Transform _parentTransform;

    public StickManObjectManager(List<StickMan> stickmen, Transform parentTransform, GridManager gridManager,
        IInstantiator instantiator, LevelDataManager levelDataManager)
    {
        _gridManager = gridManager;
        _instantiator = instantiator;
        _parentTransform = parentTransform;
        _stickmen = stickmen;
        _levelDataManager = levelDataManager;
    }
    
    public void SpawnStickManAtGrid()
    {
        for (int i = 0; i < _gridManager.Width; i++)
        {
            for (int j = 0; j < _gridManager.Height -1; j++)
            {
                var data = _levelDataManager.GetCurrentLevelData().GetGridAt(i,j);
                _instantiator.InstantiatePrefab(GetStickMan(data.ColorIndex), 
                    _gridManager.GetWorldPosition(i, j) + new Vector3(1.5f,0,1.5f) * 0.5f,
                    Quaternion.identity,_parentTransform);
            }
        }
    }

    private StickMan GetStickMan(ColorIndex colorIndex)
    {
        return _stickmen.FirstOrDefault(stickMan => stickMan.ColorIndex == colorIndex);
    }
}