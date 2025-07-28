using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StickManInstaller : MonoInstaller
{
    [SerializeField] private List<StickMan> _stickMen;
    [SerializeField] private Transform _stickManParent;
    
    public override void InstallBindings()
    {
        Container.Bind<StickManAnimator>().AsTransient().NonLazy();
        Container.Bind<StickManObjectManager>().AsSingle().WithArguments(_stickMen,_stickManParent).NonLazy();
        Container.Bind<StickManProvider>().AsSingle().NonLazy();
    }
}
