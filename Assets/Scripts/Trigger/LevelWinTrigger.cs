using System;

public class LevelWinTrigger : IDisposable
{
    private LevelEndEvents _levelEndEvents;
    private PopupManager _popupManager;
    private InputStickManReader _inputStickManReader;
    private TimerIStart _timerIStart;
    
    public LevelWinTrigger(LevelEndEvents levelEndEvents, PopupManager popupManager, InputStickManReader inputStickManReader,
        TimerIStart timerIStart)
    {
        _levelEndEvents = levelEndEvents;
        _popupManager = popupManager;
        _inputStickManReader = inputStickManReader;
        _timerIStart = timerIStart;
        
        _levelEndEvents.AddOnLevelWin(OnLevelWinEnd);
    }

    public void Dispose()
    {
        _levelEndEvents.RemoveOnLevelWin(OnLevelWinEnd);
    }
    
    private void OnLevelWinEnd()
    {
        _inputStickManReader.Disable();
        _timerIStart.TimerStop = false;
        _popupManager.OpenPopupByName("P_Screen_Win_Level_Variant");
    }
}
