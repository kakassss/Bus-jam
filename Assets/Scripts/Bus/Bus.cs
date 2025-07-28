using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Bus : MonoBehaviour
{
    public ColorIndex colorIndex;
    
    [SerializeField] private List<BusSeat> _seats;
    private BusObjectPoolEvent _busObjectPoolEvent;
    
    [Inject]
    private void Construct(BusObjectPoolEvent busObjectPoolEvent)
    {
        _busObjectPoolEvent = busObjectPoolEvent;
    }
    
    private void OnDisable()
    {
        _busObjectPoolEvent.FireDeactivatedEvent(this,(int)colorIndex);
    }

    public BusSeat GetAnyEmptySeat()
    {
        return _seats.FirstOrDefault(seat => seat.IsFull == false);
    }

    public BusSeat GetAnyEmptyMark()
    {
        return _seats.FirstOrDefault(seat => seat.IsMarked == false);
    }

    public bool IsAnyMarked()
    {
        return _seats.All(seat => seat.IsMarked != false);
    }

    public BusSeat GetNextSeat(int seatIndex)
    {
        return _seats[seatIndex];
    }
    
    public bool IsAllMarked()
    {
        return _seats.TrueForAll(seat => seat.IsMarked);
    }
    
    public bool IsFull()
    {
        return _seats.TrueForAll(seat => seat.IsFull);
    }
}


