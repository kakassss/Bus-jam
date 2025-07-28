using System;
using UnityEngine;

[Serializable]
public class Spot : Grid
{
    public Transform Transform;
    public Vector3 WorldPosition;
    
    public Spot(int x, int z, bool isFull, Transform transform) : base(x, z, isFull)
    {
        Transform = transform;
    }
    
}