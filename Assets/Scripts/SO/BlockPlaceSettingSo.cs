using NUnit.Framework.Constraints;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Settings To Make Block Placement Follow Some Rule
/// </summary>
[CreateAssetMenu(fileName = "Block", menuName = "SO/Setting")]
public class BlockPlaceSettingSo : ScriptableObject
{
    [field: SerializeField] public SingleBlockSetting[] Blocks { get; private set; }
    private void OnValidate()
    {
        // Just Making Sure Everything Is Ok

        for (int i = 0; i < Blocks.Length; i++)
        {
            var SingleblockSetting = Blocks[i];

            // Finding All Duplicate blocks In Setting

            var duplicateBlock = SingleblockSetting.Probabilitys
                .GroupBy(p => p.Block)
                .FirstOrDefault(g => g.Count() > 1)?.Key;

            if (duplicateBlock != null)
            {
                Debug.LogWarning($"{SingleblockSetting.Block.name} has duplicate blocks: {duplicateBlock.name}");
                continue; 
            }

            // Total Blocks Expected Exceeds 8 (Thier Can Only Be 8 Direction In This Grid)

            int probSum = SingleblockSetting.Probabilitys.Sum(p => p.ActivationCost);
            if (probSum > 8)
            {
                Debug.LogWarning($"{SingleblockSetting.Block.name} has more than 8 blocks for probability. May not work correctly.");
            }
        }
    }
    public void SetUp()
    {
        // Set Up All The Settings To Reduce Same Calculation Again

        foreach (var block in Blocks)
        {
            block.SetUp();
        }
    }
    public BlockSO GetRandom()
    {
        // Get A Random Block From All Blocks
        return Blocks[UnityEngine.Random.Range(0, Blocks.Length)].Block;
    }
    public BlockSO GetBlock(params BlockSO[] SurroundingBlocks)
    {
        // Get A Block Based On Calculating Its Probability Based On Its Surroundings

        foreach (var block in Blocks)
        {
            block.CalculateProb(SurroundingBlocks);
        }
        // Select A Block Out Of All The Blocks

        return Blocks.OrderByDescending(i => i.Probability).First().Block;
    }
}
/// <summary>
/// A class To Store The Setting Of A Single Block And Its Requirements
/// </summary>
[Serializable]
public class SingleBlockSetting
{
    public BlockSO Block;
    public SingleBlockProbability[] Probabilitys;
    [HideInInspector] public float Probability;
    private int m_TotalBlocks;
    public void SetUp()
    {
        m_TotalBlocks= Probabilitys.Sum(p => p.ActivationCost);
    }
    public void CalculateProb(BlockSO[] Surrounding)
    {
        // Check How Many of Its Requirement Is Fulfilled And Then Calculate Its Probability
        Probability = 0;
        float totalAvailable =  Probabilitys.Sum(p =>
        {
            int matchingCount = Surrounding.Count(s => s == p.Block);
            return Mathf.Min(matchingCount, p.ActivationCost);
        });
        Probability= totalAvailable / m_TotalBlocks;
    }
}
/// <summary>
/// A Container To Store Block And Its Cost
/// </summary>
[Serializable]
public class SingleBlockProbability
{
    public BlockSO Block;
    [Range(0,8)]public int ActivationCost;
}