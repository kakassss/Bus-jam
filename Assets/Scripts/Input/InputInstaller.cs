using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        Container.Bind<Input>().AsSingle().WithArguments(_camera).NonLazy();
        Container.Bind<InputStickManReader>().AsSingle().NonLazy();
    }
}