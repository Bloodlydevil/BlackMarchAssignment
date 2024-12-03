using System.Collections.Generic;

/// <summary>
/// Player AI Used TO Drive Player
/// </summary>
public class PlayerAI :  IAI
{
    private Block[,] block;
    public void SetUp(Block[,] grid)
    {
        block = grid;
    }
    public List<Block> GetPath(Block Character, Block Target)
    {
        // Basic => Just Get The Path Of Block From Player Position
        return AIUtils.GetPath(Character, Target, block);
    }
}
