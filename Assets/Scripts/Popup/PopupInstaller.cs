using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupInstaller : MonoInstaller
{
    [SerializeField] private List<PopupData> _popups = new List<PopupData>();
    [SerializeField] private Transform _popupContainerParent;
    
    public override void InstallBindings()
    {
        Container.Bind<PopupManager>().AsSingle().WithArguments(_popups,_popupContainerParent).NonLazy();
    }
}
