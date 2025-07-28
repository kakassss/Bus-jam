using System;

public class BusEvents
{
    public Bus ActiveBus;
    private Action OnStartBusTick;
    private Action OnBusFull;
    private Action OnSpot;
    
    public void AddBusSpotTick(Action action)
    {
        OnSpot += action;
    }

    public void RemoveBusSpotTick(Action action)
    {
        OnSpot -= action;
    }

    public void FireBusSpotTick()
    {
        OnSpot?.Invoke();
    }
    
    public void AddBusFullTick(Action action)
    {
        OnBusFull += action;
    }

    public void RemoveBusFullTick(Action action)
    {
        OnBusFull -= action;
    }

    public void FireBusFullTick()
    {
        OnBusFull?.Invoke();
    }
    
    public void AddStartBusTick(Action action)
    {
        OnStartBusTick += action;
    }

    public void RemoveStartBusTick(Action action)
    {
        OnStartBusTick -= action;
    }

    public void FireStartBusTick()
    {
        OnStartBusTick?.Invoke();
    }
    
}