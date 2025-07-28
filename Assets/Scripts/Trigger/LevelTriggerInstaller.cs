using Zenject;

public class LevelTriggerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelFailTrigger>().AsSingle().NonLazy();
        Container.Bind<LevelWinTrigger>().AsSingle().NonLazy();
    }
}
