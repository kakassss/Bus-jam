using Zenject;

public class BusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BusEvents>().AsSingle().NonLazy();
    }
}
