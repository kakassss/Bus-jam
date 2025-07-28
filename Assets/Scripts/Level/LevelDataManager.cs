using UnityEngine;

public class LevelDataManager 
{
    private GridManager _gridManager;
    private GridObjectManager _gridObjectManager;
    private PlayerDataManager _playerDataManager;
    
    private LevelData _levelData;
    private LevelData[] _allLevels;
    private int _currentLevel;
    private float _cellSize = 1.5f;
    
    public LevelDataManager( GridManager gridManager,GridObjectManager gridObjectManager, PlayerDataManager playerDataManager,
        LevelData[] allLevels)
    {
        _gridManager = gridManager;
        _gridObjectManager = gridObjectManager;
        _playerDataManager = playerDataManager;
        
        _allLevels = allLevels;
        _currentLevel = _playerDataManager.GetCurrentLevel();
        _levelData = _allLevels[2];
        
        Vector3 center = new Vector3(0, 0, 0);
        Vector3 gridOffset = center - new Vector3(_levelData.Width, 0, _levelData.Height - 3.5f) * (_cellSize * 0.5f);
        
        _gridManager.CreateGrid(_levelData.Width, _levelData.Height + 1, _cellSize, gridOffset);
        _gridObjectManager.InitializeObjects();
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public LevelData GetCurrentLevelData()
    {
        return _levelData;
    }

    public int GetCurrentLevelTime()
    {
        return _levelData.LevelTime;
    }
}
