using UnityEngine.SceneManagement;
using Zenject;

public class NextLevelButtonListener : BaseButtonListener
{
    private PlayerDataManager _playerDataManager;
    private PlayerDataSave _playerDataSave;
    
    [Inject]
    private void Construct(PlayerDataManager playerDataManager, PlayerDataSave playerDataSave)
    {
        _playerDataManager = playerDataManager;
        _playerDataSave = playerDataSave;
    }
    
    private void Nextlevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        var data = _playerDataManager.GetPlayerData();
        data.CurrentLevel++;
        
        data.GamePlayData = new GamePlayData();
        _playerDataSave.Save(data);
    }
    
    protected override void OnClick()
    {
        Nextlevel();
    }
}
