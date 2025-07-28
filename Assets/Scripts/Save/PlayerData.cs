using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int CurrentLevel;
    public GamePlayData GamePlayData = new GamePlayData();
}

[Serializable]
public class GamePlayData
{
    public List<Spot> Spots = new List<Spot>();
    public Grid[,] Grids;
    public List<Grid> GridList = new List<Grid>();
    public List<Bus> Buses;
    public float Timer;
}
