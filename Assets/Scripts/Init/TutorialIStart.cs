using System.Collections;
using UnityEngine;

public class TutorialIStart : IStart
{
    private PopupManager _popupManager;
    
    public TutorialIStart(PopupManager popupManager)
    {
        _popupManager = popupManager;
    }
    
    public IEnumerator Start()
    {
        _popupManager.OpenPopupByName("P_Screen_Timer_Tutorial_Variant");
        
        yield return new WaitUntil(() => _popupManager.ActivePopup == null);
    }
}
