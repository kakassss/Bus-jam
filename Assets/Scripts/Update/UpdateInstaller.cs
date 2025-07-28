using Zenject;

public class UpdateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UpdateProvider>().AsSingle().NonLazy();
    }
}
