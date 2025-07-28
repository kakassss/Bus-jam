using UnityEngine;
using Zenject;

public class GridInstaller : MonoInstaller
{
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private Transform gridParent;
    
    public override void InstallBindings()
    {
        Container.Bind<GridManager>().AsSingle().NonLazy();
        Container.Bind<GridObjectManager>().AsSingle().WithArguments(gridPrefab, gridParent).NonLazy();
    }
}
