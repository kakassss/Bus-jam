using Zenject;

public class PlayerDataInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerDataSave>().AsSingle().NonLazy();
        Container.Bind<PlayerDataManager>().AsSingle().NonLazy();
    }
}
