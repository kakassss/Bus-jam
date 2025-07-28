using Zenject;

public class PlayerDataManager 
{
    private PlayerDataSave _playerDataSave;
    private PlayerData _playerData;
    
    [Inject]
    private void Construct(PlayerDataSave playerDataSave)
    {
        _playerDataSave = playerDataSave;
        _playerData = _playerDataSave.Load();
    }
    
    public int GetCurrentLevel()
    {
        return _playerData.CurrentLevel;
    }

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }
}
