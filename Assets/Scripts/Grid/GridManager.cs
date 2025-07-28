using UnityEngine;

public class GridManager
{
    public Grid[,] Grid => _grid;
    public float CellSize => _cellSize;
    public int Width => _width;
    public int Height => _height;
    
    private Grid[,] _grid;
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _cellOffSet;
    
    public void CreateGrid(int width, int height, float cellSize, Vector3 cellOffSet)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _cellOffSet = cellOffSet;
        _grid = new Grid[_width, _height];
        
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (j == _height-1)
                {
                    _grid[i, j] = new Grid(i,j,false);    
                }
                else
                {
                    _grid[i, j] = new Grid(i,j,true);
                }
            }
        }
    }

    public Vector3 GetWorldPosition(float x, float z)
    {
        return new Vector3(x, 0, z) * _cellSize + _cellOffSet;
    }
    
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize + _cellOffSet;
    }
    
    public void GetXZ(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPos - _cellOffSet).x / _cellSize);
        z = Mathf.FloorToInt((worldPos - _cellOffSet).z / _cellSize);
    }

    public Grid GetGridAtIndex(int x, int z)
    {
        if ( x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _grid[x, z];
        }
        
        return null;
    }
    
    public Grid GetGridAtWorldPos(Vector3 worldPos)
    {
        GetXZ(worldPos,out var x, out var z);
        if ( x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _grid[x, z];
        }
        
        return null;
    }
}