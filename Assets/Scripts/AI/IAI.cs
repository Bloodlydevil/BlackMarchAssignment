using System.Collections.Generic;

/// <summary>
/// The AI Interface That All The AI Must Use To Drive The Object
/// </summary>
public interface IAI
{
    /// <summary>
    /// Set The Blocks so That It Can Be Used Without Assigning It Again And again
    /// </summary>
    /// <param name="grid"></param>
    public void SetUp(Block[,] grid);
    /// <summary>
    /// Get The Path Required To Go Towards The Target
    /// </summary>
    /// <param name="Source">The Place Of Origin</param>
    /// <param name="Target">The End Goal</param>
    /// <returns>The Path To Take</returns>
    public List<Block> GetPath(Block Source,Block Target);
}