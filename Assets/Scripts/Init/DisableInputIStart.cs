
using System.Collections;

public class DisableInputIStart : IStart
{
    private InputStickManReader _stickManReader;

    public DisableInputIStart(InputStickManReader stickManReader)
    {
        _stickManReader = stickManReader;
    }
    
    public IEnumerator Start()
    {
        _stickManReader.Disable();
        yield return null;
    }
}
