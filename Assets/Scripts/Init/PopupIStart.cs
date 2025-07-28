using System.Collections;
using UnityEngine;

public class PopupIStart : IStart
{
    private PopupManager _popupManager;
    
    public PopupIStart(PopupManager popupManager)
    {
        _popupManager = popupManager;
    }
    
    public IEnumerator Start()
    {
        _popupManager.OpenPopupByName("P_Screen_TapToPlay_Variant");
        yield return new WaitUntil(() => _popupManager.ActivePopup == null);
    }
}