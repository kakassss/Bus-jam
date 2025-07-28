using System.Collections;

public class EnableInputIIStart : IStart
{
    private InputStickManReader _stickManReader;

    public EnableInputIIStart(InputStickManReader stickManReader)
    {
        _stickManReader = stickManReader;
        
        _stickManReader.Enable();
    }
    
    public IEnumerator Start()
    {
        _stickManReader.Enable();
        yield return null;
    }
}
