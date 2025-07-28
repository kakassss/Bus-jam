﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupManager
{
    public GameObject ActivePopup => _activePopup;
    
    private readonly IInstantiator _instantiator;
    private readonly Transform _popupContainerParent;
    
    private Dictionary<string, GameObject> _allPopups = new Dictionary<string, GameObject>();
    private List<PopupData> _popups = new List<PopupData>();

    private List<Popup> _activePopups = new List<Popup>();
    private GameObject _activePopup;
    
    public PopupManager(IInstantiator instantiator, Transform popupContainerParent, List<PopupData> popups)
    {
        _instantiator = instantiator;
        _popupContainerParent = popupContainerParent;
        _popups = popups;
        
        foreach (var popup in _popups)
        {
            _allPopups.Add(popup.Prefab.name, popup.Prefab);
        }
    }
    
    public void OpenPopupByName(string name)
    {
        if(_activePopup != null) return;
        
        _activePopup = _instantiator.InstantiatePrefab(GetPopupByName(name), _popupContainerParent);
    }
    
    public void ClosePopupByName()
    {
        if (_activePopup == null) return;
        
        _activePopup.gameObject.SetActive(false);
        Object.Destroy(_activePopup.gameObject);
        _activePopup = null;
    }
    
    public GameObject GetPopupByName(string name)
    {
        return _allPopups[name];
    }
}