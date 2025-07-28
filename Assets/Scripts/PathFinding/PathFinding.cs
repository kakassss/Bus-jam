using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private GridManager _gridManager;
    private PlayerData _playerData;
    
    private Grid _gridUp;
    private Grid _gridLeft;
    private Grid _gridRight;
    
    public PathFinding(GridManager gridManager)
    {
        _gridManager = gridManager;
    }
    
    public List<Grid> FindPathForward(Vector3 startPos)
    {
        int width = _gridManager.Width;
        int height = _gridManager.Height;

        _gridManager.GetXZ(startPos, out var x, out var z);
        var startGrid = new Vector3(x, 0, z);
        
        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> visited = new HashSet<Vector3>();
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
        
        var currentGrid = _gridManager.GetGridAtIndex(x, z);
        
        queue.Enqueue(startGrid);
        visited.Add(startGrid);

        Vector3[] directions = new Vector3[]
        {
            new Vector3(0,0, 1),
            new Vector3(1,0, 0),
            new Vector3(-1,0, 0) 
        };

        while (queue.Count > 0)
        {
            Vector3 current = queue.Dequeue();

            if (current.z == height - 1)
            {
                currentGrid.IsFull = false;
                return ReconstructPath(_gridManager.Grid, cameFrom, startGrid, current);
            }

            foreach (var dir in directions)
            {
                Vector3 next = current + dir;

                if (next.x < 0 || next.x >= width || next.z < 0 || next.z >= height)
                    continue;

                if (visited.Contains(next))
                    continue;

                if (_gridManager.Grid[(int)next.x, (int)next.z].IsFull)
                    continue;

                visited.Add(next);
                queue.Enqueue(next);
                cameFrom[next] = current;
            }
        }
        
        return null; 
    }

    private List<Grid> ReconstructPath(Grid[,] grid, Dictionary<Vector3, Vector3> cameFrom, Vector3 start, Vector3 end)
    {
        List<Grid> path = new List<Grid>();
        Vector3 current = end;

        while (current != start)
        {
            path.Insert(0, grid[(int)current.x, (int)current.z]);
            current = cameFrom[current];
        }

        return path;
    }
}