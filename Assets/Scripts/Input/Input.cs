using UnityEngine;
using UnityEngine.InputSystem;

public class Input
{
    private Camera _camera;

    public Input(Camera camera)
    {
        _camera = camera;
    }
    
    public IStickMan GetMoveableStickMan()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return null;
    
        return hit.collider.TryGetComponent<IStickMan>(out var foundStickMan) ? foundStickMan : null;
    }
}