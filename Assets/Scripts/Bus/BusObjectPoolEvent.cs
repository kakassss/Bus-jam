using System;

public class BusObjectPoolEvent
{
    private Action<Bus,int> OnBusDeactivated;

    private Action<Bus> OnBusDeActivated;
    
    public void FireDeactivatedEvent(Bus busController)
    {
        OnBusDeActivated?.Invoke(busController);
    }

    public void AddDeactivatedEvent(Action<Bus> action)
    {
        OnBusDeActivated += action;
    }

    public void RemoveDeactivatedEvent(Action<Bus> action)
    {
        OnBusDeActivated -= action;
    }
    
    public void FireDeactivatedEvent(Bus bus,int index)
    {
        OnBusDeactivated?.Invoke(bus,index);
    }

    public void AddDeactivatedEvent(Action<Bus, int> action)
    {
        OnBusDeactivated += action;
    }

    public void RemoveDeactivatedEvent(Action<Bus, int> action)
    {
        OnBusDeactivated -= action;
    }
}