using System;

public class LevelFailTrigger : IDisposable
{
    private LevelEndEvents _levelEndEvents;
    private PopupManager _popupManager;
    private InputStickManReader _inputStickManReader;
    
    public LevelFailTrigger(LevelEndEvents levelEndEvents, PopupManager popupManager, InputStickManReader inputStickManReader)
    {
        _levelEndEvents = levelEndEvents;
        _popupManager = popupManager;
        _inputStickManReader = inputStickManReader;
        
        _levelEndEvents.AddOnAllSeatsFullEndListener(OnLevelFailEnd);
        _levelEndEvents.AddOnTimerEndListener(OnLevelFailEnd);
    }

    public void Dispose()
    {
        _levelEndEvents.RemoveOnTimerEndListener(OnLevelFailEnd);
        _levelEndEvents.RemoveOnAllSeatsFullEndListener(OnLevelFailEnd);
    }
    private void OnLevelFailEnd()
    {
        _inputStickManReader.Disable();
        _popupManager.OpenPopupByName("P_Screen_Lose_Level_Variant");
    }
}
