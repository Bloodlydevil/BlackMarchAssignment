using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy AI Used TO Drive Enemy
/// </summary>
public class EnemyAI : IAI
{
    private Block[,] block;
    public void SetUp(Block[,] grid)
    {
        block = grid;
    }
    public List<Block> GetPath(Block Enemy, Block Target)
    {
        // As Q requires Us To Move in one Of The four direction From Player We Just Calculate 
        // Any Path ToWards It 
        // Currently It Does Not Go To Any Newar Position If Immidiate Position Is Not Movable
        foreach(var Dir in AIUtils.Directions)
        {
            Vector2Int Pos = Target.GridPosition + Dir;
            if (Pos.x >= 0 && Pos.x < block.GetLength(0) &&
                Pos.y >= 0 && Pos.y < block.GetLength(1))
            {
                // If The Position Is Posible to Move Then Try TO Move Towards It

                if (block[Pos.x, Pos.y].IsOccupied)
                    continue;

                var path = AIUtils.GetPath(Enemy, block[Pos.x, Pos.y], block);
                if (path.Count != 0)
                    return path;
            }
        }
        return new List<Block>();
    }
}
