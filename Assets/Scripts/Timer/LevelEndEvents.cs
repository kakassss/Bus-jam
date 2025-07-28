using System;

public class LevelEndEvents 
{
    private Action OnTimerFailEnd;
    private Action OnAllSeatsFullFailEnd;
    private Action OnLevelWin;
    
    public void AddOnLevelWin(Action action)
    {
        OnLevelWin += action;
    }

    public void RemoveOnLevelWin(Action action)
    {
        OnLevelWin -= action;
    }

    public void FireOnLevelWin()
    {
        OnLevelWin?.Invoke();
    }
    
    public void AddOnAllSeatsFullEndListener(Action action)
    {
        OnAllSeatsFullFailEnd += action;
    }

    public void RemoveOnAllSeatsFullEndListener(Action action)
    {
        OnAllSeatsFullFailEnd -= action;
    }

    public void FireOnAllSeatsFullEnd()
    {
        OnAllSeatsFullFailEnd?.Invoke();
    }
    
    public void AddOnTimerEndListener(Action action)
    {
        OnTimerFailEnd += action;
    }

    public void RemoveOnTimerEndListener(Action action)
    {
        OnTimerFailEnd -= action;
    }

    public void FireOnTimerEnd()
    {
        OnTimerFailEnd?.Invoke();
    }
}
