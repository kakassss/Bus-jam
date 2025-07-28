using System;

public class LevelSpotEndManager : IUpdate, IDisposable
{
    private UpdateProvider _updateProvider;
    private SpotsManager _spotsManager;
    private LevelEndEvents _levelEndEvents;
    private PopupManager _popupManager;
    
    private bool _gameOver = false;
    
    public LevelSpotEndManager(UpdateProvider updateProvider, SpotsManager spotsManager, LevelEndEvents levelEndEvents
    , PopupManager popupManager)
    {
        _updateProvider = updateProvider;
        _spotsManager = spotsManager;
        _levelEndEvents = levelEndEvents;
        _popupManager = popupManager;
        
        _updateProvider.AddListener(this);
    }
    
    public void Dispose()
    {
        _updateProvider.RemoveListener(this);   
    }
    
    public void UpdateBehavior()
    {
        if (_spotsManager.IsAllSpotsFull() && _gameOver == false)
        {
            _popupManager.OpenPopupByName("P_Screen_Lose_Level_Variant");
            _levelEndEvents.FireOnAllSeatsFullEnd();
            _gameOver = true;
        }
    }
}
