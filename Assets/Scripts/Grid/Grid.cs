using System;
using UnityEngine;

[Serializable]
public class Grid
{
    [HideInInspector] public int X;
    [HideInInspector] public int Z;
    [HideInInspector] public Vector3 Position;
    [HideInInspector] public bool IsFull;
    [HideInInspector] public ColorIndex ColorIndex;
    [HideInInspector] public StickMan StickMan;
    
    public Grid(int x, int z, bool isFull)
    {
        X = x;
        Z = z;
        Position = new Vector3(x, 0, z);
        IsFull = isFull;
    }
}
