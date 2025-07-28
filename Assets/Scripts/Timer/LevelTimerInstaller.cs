using Zenject;

public class LevelTimerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelTimerManager>().AsSingle().NonLazy();
        Container.Bind<LevelEndEvents>().AsSingle().NonLazy();
    }
}
