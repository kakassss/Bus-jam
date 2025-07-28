using UnityEngine.InputSystem;

public class InputStickManReader : InputSystem_Actions.IInputClickActions
{
    private InputSystem_Actions _inputSystemActions = new();
    private StickManProvider _stickManProvider;
    private Input _input;
    
    public InputStickManReader(StickManProvider stickManProvider, Input input)
    {
        _stickManProvider = stickManProvider;
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
        
        _stickManProvider.StickMan = _input.GetMoveableStickMan();
        
        if(_stickManProvider.StickMan == null) return;
        _stickManProvider.StickMan.FindPath();
        _stickManProvider.StickMan.FirstAction();
    }
}
