using System.Collections;

public class BusIStart : IStart
{
    private BusEvents _busEvents;

    public BusIStart(BusEvents busEvents)
    {
        _busEvents = busEvents;
    }
    
    public IEnumerator Start()
    {
        _busEvents.FireStartBusTick();
        yield return null;
    }
}