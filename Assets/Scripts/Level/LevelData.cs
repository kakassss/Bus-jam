using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataSO")]
public class LevelData : ScriptableObject
{
    public int Width;
    public int Height;
    public int LevelTime;
    public List<ColorIndex> BusColors;
    
    [HideInInspector] public List<Grid> grids = new List<Grid>();

    public void CreateGrid(int width, int height)
    {
        Width = width;
        Height = height;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                grids.Add(new Grid(x, z, false));
            }
        }
    }
    
    [Button]
    private void Save()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    public Grid GetGridAt(int x, int z)
    {
        return grids.Find(g => g.X == x && g.Z == z);
    }
}