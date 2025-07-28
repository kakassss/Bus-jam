using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    private LevelData[] _allLevels;
    
    private void FindLevelData()
    {
        _allLevels = Resources.LoadAll<LevelData>("Data");
    }
    
    public override void InstallBindings()
    {
        FindLevelData();
        Container.Bind<LevelDataManager>().AsSingle().WithArguments(_allLevels).NonLazy();
    }
}
