using UnityEngine;

public struct PassengerData
{
    public Bus Bus;
    public BusSeat Seat;
    public Transform BusSeatTransform;
    public Transform BusTransform;
    public Transform SpotTransform;
}

public class SpotsMovementManager
{
    private BusEvents _busEvents;
    private StickManProvider _stickManProvider;
    private SpotsManager _spotsManager;
    
    private PassengerData _passengerData;
    private BusSeat _busSeat;
    
    public SpotsMovementManager(BusEvents busEvents, SpotsManager spotsManager)
    {
        _busEvents = busEvents;
        _spotsManager = spotsManager;
    }
     
    public Vector3 SetTarget(StickMan stickMan,Transform transform,BusSeat seat)
    {
        var bus = _busEvents.ActiveBus;
        _passengerData = new PassengerData();
        
        if (bus != null && seat != null)
        {
            _busSeat = seat;
            _passengerData.BusSeatTransform = seat.SeatTransform;
            _passengerData.BusTransform = bus.transform;
            return bus.transform.position;
        }
        
        var spot = _spotsManager.GetFirstEmptySpot(transform,stickMan);
        _passengerData.SpotTransform = spot.Transform;
        return spot.Transform.position;
    }

    public void SetSelectedSeatFull()
    {
        _busSeat.IsFull = true;
    }

    public BusSeat SetMarkSeat()
    {
        var currentBus = _busEvents.ActiveBus;
        if(currentBus == null ) return null;
        
        var markedSeat = currentBus.GetAnyEmptyMark();
        markedSeat.IsMarked = true;
        return markedSeat;
    }

    public void FireBus()
    {
        if(_busEvents.ActiveBus != null && _busEvents.ActiveBus.IsFull() == true)
            _busEvents.FireBusFullTick();
    }
    
    public bool CanColorEqual(StickMan stickMan)
    {
        return _busEvents.ActiveBus != null && stickMan.ColorIndex == _busEvents.ActiveBus.colorIndex;
    }
    
    public bool CanSeatMarked()
    {
        return _busEvents.ActiveBus.IsAllMarked() == false && _busEvents.ActiveBus != null;
    }
    
    public PassengerData GetPassengerData()
    {
        return _passengerData;
    }
}