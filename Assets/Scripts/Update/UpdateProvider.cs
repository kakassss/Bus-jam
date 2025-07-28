using System.Collections.Generic;

public class UpdateProvider
{
    private List<IUpdate> Updates = new List<IUpdate>();

    public void UpdateBehavior()
    {
        for (int i = 0; i < Updates.Count; i++)
        {
            if(Updates[i] != null)
                Updates[i].UpdateBehavior();
        }
    }

    public void AddListener(IUpdate listener)
    {
        Updates.Add(listener);
    }

    public void RemoveListener(IUpdate listener)
    {
        Updates.Remove(listener);
    }
}
