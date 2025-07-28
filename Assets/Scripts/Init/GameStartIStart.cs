using System.Collections;
using UnityEngine;

public class GameStartIStart : IStart
{
    public bool GameStarted;
    
    private BusEvents _busEvents;

    public GameStartIStart(BusEvents busEvents)
    {
        _busEvents = busEvents;
    }
    
    public IEnumerator Start()
    {
        yield return new WaitUntil((() => _busEvents.ActiveBus != null));
        GameStarted = true;
    }
}
