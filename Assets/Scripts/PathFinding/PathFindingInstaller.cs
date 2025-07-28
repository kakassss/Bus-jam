using Zenject;

public class PathFindingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PathFinding>().AsSingle().NonLazy();
    }
}