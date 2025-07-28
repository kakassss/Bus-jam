using System.Collections;

public class FirstIStart : IStart
{
    private StickManObjectManager _stickManObjectManager;

    public FirstIStart(StickManObjectManager stickManObjectManager)
    {
        _stickManObjectManager = stickManObjectManager;
    }
    
    public IEnumerator Start()
    {
        _stickManObjectManager.SpawnStickManAtGrid();
        yield return null;
    }
}
