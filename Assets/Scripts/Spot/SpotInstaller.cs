using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpotInstaller : MonoInstaller
{
    [SerializeField] private List<Spot> _holdingSpots;
    
    public override void InstallBindings()
    {
        Container.Bind<SpotsManager>().AsSingle().WithArguments(_holdingSpots).NonLazy();
        Container.Bind<LevelSpotEndManager>().AsSingle().NonLazy();
        Container.Bind<SpotsMovementManager>().AsTransient().NonLazy();
    }
}