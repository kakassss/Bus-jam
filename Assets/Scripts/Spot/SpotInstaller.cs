using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SpotInstaller : MonoInstaller
{
    [FormerlySerializedAs("_holdingSpots")] [SerializeField] private List<Spot> _spots;
    
    public override void InstallBindings()
    {
        Container.Bind<SpotsManager>().AsSingle().WithArguments(_spots).NonLazy();
        Container.Bind<LevelSpotEndManager>().AsSingle().NonLazy();
        Container.Bind<SpotsMovementManager>().AsTransient().NonLazy();
    }
}