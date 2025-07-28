using UnityEngine.InputSystem;

public class InputStickManReader : InputSystem_Actions.IInputClickActions
{
    private InputSystem_Actions _inputSystemActions = new();
    private StickManEvents _stickManEvents;
    private Input _input;
    
    public InputStickManReader(StickManEvents stickManEvents, Input input)
    {
        _stickManEvents = stickManEvents;
        _input = input;
        
        _inputSystemActions.InputClick.SetCallbacks(this);
        _inputSystemActions.InputClick.Enable();
    }
    
    public void Enable()
    {
        _inputSystemActions.InputClick.Enable();
    }

    public void Disable()
    {
        _inputSystemActions.InputClick.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        
        _stickManEvents.StickMan = _input.GetMoveableStickMan();
        
        if(_stickManEvents.StickMan == null) return;
        _stickManEvents.StickMan.FindPath();
        _stickManEvents.StickMan.FirstAction();
    }
}
