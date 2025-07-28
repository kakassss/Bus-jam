using Zenject;

public class StartInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PopupIStart>().AsSingle().NonLazy();
        Container.Bind<DisableInputIStart>().AsSingle().NonLazy();
        Container.Bind<EnableInputIIStart>().AsSingle().NonLazy();
        Container.Bind<TutorialIStart>().AsSingle().NonLazy();
        Container.Bind<TimerIStart>().AsSingle().NonLazy();
        Container.Bind<FirstIStart>().AsSingle().NonLazy();
        Container.Bind<BusIStart>().AsSingle().NonLazy();
        Container.Bind<GameStartIStart>().AsSingle().NonLazy();
    }
}
