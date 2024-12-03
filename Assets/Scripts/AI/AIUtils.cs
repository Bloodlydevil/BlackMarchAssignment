using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Impliments A* Algorithem To Get The Positon
/// </summary>
public static class AIUtils
{
    /// <summary>
    /// The Direction Allowed To Move
    /// </summary>
    public static Vector2Int[] Directions = {new (0, 1),new (0, -1),new (-1, 0),new (1, 0)};
    private static int Heuristic(Block Current,Block Target)
    {
        // Basic Function Used To Calculate HValue

        Vector2Int left = Current.GridPosition;
        Vector2Int right = Target.GridPosition;
        return  Mathf.Abs(left.x - right.x) + Mathf.Abs(left.y - right.y);
    }
    private static List<Block> GetNeighbors(Block block, Block[,] grid)
    {
        // Get All The Blocks In The Neighbors From The Current One 

        List<Block> neighbors = new List<Block>();
        Vector2Int Current = block.GridPosition;
        foreach (var dir in Directions)
        {
            Vector2Int Pos = Current + dir;
            if (Pos.x >= 0 && Pos.x < grid.GetLength(0) &&
                Pos.y >= 0 && Pos.y < grid.GetLength(1))
            {
                neighbors.Add(grid[Pos.x, Pos.y]);
            }
        }

        return neighbors;
    }
    private static List<Block> GetPath(Dictionary<Block, Block> PastBlock, Block Target)
    {
        // Calculating The Path After Final Target Is Reached

        var path = new List<Block> ();
        path.Add(Target);
        while (PastBlock.ContainsKey(Target))
        {
            Target = PastBlock[Target];
            path.Add(Target);
        }
        path.Reverse();
        return path;
    }

    public static List<Block> GetPath(Block Source, Block Target, Block[,] grid)
    {
        var openList = new PriorityQueue<Block>();
        var cameFrom = new Dictionary<Block, Block>();
        var gScore = new Dictionary<Block, int>();
        var fScore = new Dictionary<Block, int>();
        
        foreach (var block in grid)
        {
            gScore[block] = int.MaxValue;
            fScore[block] = int.MaxValue;
        }

        gScore[Source] = 0;
        fScore[Source] = Heuristic(Source, Target);
        openList.Enqueue(Source, fScore[Source]);

        while (openList.Count > 0)
        {
            Block current = openList.Dequeue();
            if (current == Target)
            {
                return GetPath(cameFrom, current);
            }

            foreach (var neighbor in GetNeighbors(current, grid))
            {
                if (neighbor.IsOccupied) continue;
                int tentativeGScore = gScore[current] + 1;
                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + Heuristic(neighbor, Target);
                    if (!openList.Contains(neighbor))
                    {
                        openList.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        // No Path Is Found

        return new List<Block>();
    }

}
