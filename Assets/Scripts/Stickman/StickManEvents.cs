using System;
using UnityEngine;

public class StickManEvents
{
    public IStickMan StickMan;
    
    private Action OnSelected;
    private Action<Vector3> OnNextPosition;

    public void AddSelectedAction(Action action)
    {
        OnSelected += action;
    }

    public void RemoveSelectedAction(Action action)
    {
        OnSelected -= action;
    }

    public void FireSelectedAction()
    {
        OnSelected?.Invoke();
    }
    
    public void AddNextGridAction(Action<Vector3> action)
    {
        OnNextPosition += action;
    }

    public void RemoveNextGridAction(Action<Vector3> action)
    {
        OnNextPosition -= action;
    }

    public void FireNextPositionAction(Vector3 position)
    {
        OnNextPosition?.Invoke(position);
    }
}