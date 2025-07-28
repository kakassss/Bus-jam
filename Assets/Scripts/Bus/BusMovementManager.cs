using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class BusMovementManager : MonoBehaviour
{
    [Header("Bus Position")]
    [SerializeField] private Transform _exitPos;
    [SerializeField] private Transform _centerPos;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _behindPos;
    
    [Header("Settings")] 
    [SerializeField] private MovementSO _movementSo;
    
    private LevelEndEvents _levelEndEvents;
    private BusMultipleObjectPool _busMultipleObjectPool;
    private MovementUtils _movementUtils;
    private LevelDataManager _levelDataManager;
    private BusEvents _busEvents;
    
    private Bus _exitBus;
    private Bus _middleBus;
    private Bus _backBus;
    private Bus _spawnBus;
    
    private List<ColorIndex> _busColors;
    private int _totalBuses;
    private int _colorCounter = 0;
    
    [Inject]
    private void Construct(BusMultipleObjectPool busMultipleObjectPool, MovementUtils movementUtils, LevelDataManager levelDataManager
    , BusEvents busEvents, LevelEndEvents levelEndEvents)
    {
        _busMultipleObjectPool = busMultipleObjectPool;
        _movementUtils = movementUtils;
        _levelDataManager = levelDataManager;
        _levelEndEvents = levelEndEvents;
        
        _busEvents = busEvents;
        _busEvents.AddStartBusTick(FirstSequenceMovement);
        _busEvents.AddBusFullTick(MainSequenceMovement);
        
        _movementUtils.SetMovement(_movementSo);
        _busColors = _levelDataManager.GetCurrentLevelData().BusColors;
        _totalBuses = _busColors.Count;
    }

    private void OnDisable()
    {
        _busEvents.RemoveStartBusTick(FirstSequenceMovement);
        _busEvents.RemoveBusFullTick(MainSequenceMovement);
    }
    
    private ColorIndex GetBusColor()
    {
        var color = _busColors[_colorCounter]; 
        _colorCounter++;
        _totalBuses--;
        return color;
    }
    
    private void FirstSequenceMovement()
    {
        _middleBus = _busMultipleObjectPool.GetObjectFromPool((int)GetBusColor()-1, _behindPos.position);
        _backBus = _busMultipleObjectPool.GetObjectFromPool((int)GetBusColor()-1, _spawnPos.position);
        
        Sequence sequence = DOTween.Sequence();
        sequence.SetAutoKill(true);
        
        sequence.Join(_middleBus.transform.DOMove(_centerPos.position, 0.5f)).OnComplete((() =>
        {
            SetActiveBus();
            _busEvents.FireBusSpotTick();
        }));
        sequence.Join(_backBus.transform.DOMove(_behindPos.position, 0.5f));
    }

    private void MainSequenceMovement()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetAutoKill(true);
        
        if (_spawnBus == null && _totalBuses > 0)
        {
            _spawnBus = _busMultipleObjectPool.GetObjectFromPool((int)GetBusColor() - 1, _spawnPos.position);
        }
        
        sequence.Join(_middleBus.transform.DOMove(_exitPos.position, 0.6f));
        if(_backBus != null) sequence.Join(_backBus.transform.DOMove(_centerPos.position, 0.6f));
        if(_spawnBus != null) sequence.Join(_spawnBus.transform.DOMove(_behindPos.position, 0.6f));

        sequence.OnComplete(SpawnBusCallBack);
    }
    
    private void SpawnBusCallBack()
    {
        _exitBus = _middleBus;
        _middleBus = _backBus;
        _backBus = _spawnBus;
        _spawnBus = null;
        _exitBus.gameObject.SetActive(false);
        
        SetActiveBus();
        
        if (_middleBus == null)
        {
            _levelEndEvents.FireOnLevelWin();
        }
        
        _busEvents.FireBusSpotTick();
    }
    
    private void SetActiveBus()
    {
        _busEvents.ActiveBus = _middleBus;
    }
}
