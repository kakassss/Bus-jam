using Zenject;

public class UtilsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MovementUtils>().AsSingle().NonLazy();
        Container.Bind<QuaternionUtils>().AsSingle().NonLazy();
    }
}
