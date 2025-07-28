using System;
using UnityEngine;

public class LevelTimerManager : IUpdate, IDisposable
{
    public float LevelTime => _levelTime;
    public string CurrentTime => _currentTime;
    
    private float _levelTime;
    private string _currentTime = "00:00";
    
    private UpdateProvider _updateProvider;
    private LevelDataManager _levelDataManager;
    private TimerIStart _timerIStart;
    private LevelEndEvents _levelEndEvents;
    
    public LevelTimerManager(UpdateProvider updateProvider, LevelDataManager levelDataManager, TimerIStart timerIStart
    , LevelEndEvents levelEndEvents)
    {
        _updateProvider = updateProvider;
        _levelDataManager = levelDataManager;
        _timerIStart = timerIStart;
        _levelEndEvents = levelEndEvents;
        
        _updateProvider.AddListener(this);

        _levelTime = _levelDataManager.GetCurrentLevelTime();

        SetTime();
    }
    
    public void Dispose()
    {
        _updateProvider.RemoveListener(this);
    }
    
    public void UpdateBehavior()
    {
        if(_timerIStart.TimerStop == false) return;

        if (_levelTime <= 0)
        {
            _levelTime = 0;
            SetTime();
            
            _levelEndEvents.FireOnTimerEnd();
            _updateProvider.RemoveListener(this);
            return;
        }
        _levelTime -= Time.deltaTime;
        SetTime();
    }

    private void SetTime()
    {
        var minutes = Mathf.FloorToInt(_levelTime / 60);
        var seconds = Mathf.FloorToInt(_levelTime % 60);
        _currentTime = $"{minutes:00}:{seconds:00}";
    }

}
