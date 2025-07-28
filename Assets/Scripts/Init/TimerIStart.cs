using System.Collections;

public class TimerIStart : IStart
{
    public bool TimerStop = false;
    
    public IEnumerator Start()
    {
        TimerStop = true;
        yield return null;
    }
}
