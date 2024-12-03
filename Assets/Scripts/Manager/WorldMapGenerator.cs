using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class WorldMapGenerator : MonoBehaviour
{
    [field:SerializeField] public Transform World {  get; private set; }
    [SerializeField] private BlockPlaceSettingSo m_BlocksSettings;
    [SerializeField] private ObstacleManager m_ObstacleManager;
    public Block[,] Blocks { get; private set; }
    private void Awake()
    {
        if (Blocks == null)
        {
            if(World.childCount == 0)
            {
                // No World Is Set Up So Create One
                SetUp();
            }
            else
            {
                // World Is Set Up but Data Shows Empty So Reassign Them 

                ReSetUpBlocks();
            }
        }
        m_ObstacleManager.SetUp(Blocks);
    }
    private void Update()
    {
        // Allow Simple Rotation

        transform.Rotate(3 * Time.deltaTime * Vector3.up);
    }
    private BlockSO[,] GenerateBlocks(BlockSO[,] blockSOs)
    {
        // Generate Blocks To Place Based On The Surroundings

        for (int i = 1; i < blockSOs.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < blockSOs.GetLength(1) - 1; j++)
            {
                blockSOs[i, j] = m_BlocksSettings.GetBlock(blockSOs[i - 1, j - 1], blockSOs[i - 1, j], blockSOs[i - 1, j + 1],      // LeftTop  Top  RightTop

                                                           blockSOs[i, j - 1],                          blockSOs[i, j + 1]          // Left          Right

                                                          , blockSOs[i + 1, j - 1], blockSOs[i + 1, j], blockSOs[i + 1, j + 1]);    // LeftDown  Down RightDown
            }
        }
        return blockSOs;
    }
    private void ReSetUpBlocks()
    {
        // All The Blocks Are Present But Its Data Are Not Stored After Starting Game (Sometimes Happens With Properites)

        Blocks= new Block[GameSettings.GameSizeX, GameSettings.GameSizeY];
        for (int i=0;i< World.childCount;i++)
        {
            if(World.GetChild(i).gameObject.TryGetComponent(out Block block))
            {
                Vector2 pos=new Vector2(block.transform.position.x, block.transform.position.z);
                pos/=block.transform.localScale.x;
                Blocks[(int)pos.x + GameSettings.GameSizeX / 2, (int)pos.y + GameSettings.GameSizeY / 2] = block;
            }
        }
    }
    public void SetUp()
    {
        m_BlocksSettings.SetUp();
        
        // To Avoid Doing Edge Cases I Have MAde Is Size Increase By 2 But Only Take Into Consideration The Middle One

        BlockSO[,] blockSOs = new BlockSO[GameSettings.GameSizeX + 2, GameSettings.GameSizeY + 2];

        // Randomising The Blocks To Create New Base Map 

        for (int i = 1; i < blockSOs.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < blockSOs.GetLength(1) - 1; j++)
            {
                blockSOs[i, j] = m_BlocksSettings.GetRandom();
            }
        }

        // Iterate The Map Over And Over Again To Filter Out The Blocks And Make
        // Map Less Random And More Following of The Settings

        // It Currently does The Changing On Live Map And Not Previous MAp So Data Can Be Not Properly Right

        for (int i = 0; i < 3; i++)
        {
            blockSOs = GenerateBlocks(blockSOs);
        }
        Blocks = new Block[GameSettings.GameSizeX, GameSettings.GameSizeY];
        
        // All the Data Is Set Up So Now We Can Spawn The Objects 

        for (int i = 1; i < blockSOs.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < blockSOs.GetLength(1) - 1; j++)
            {
                float scale = blockSOs[i, j].PreFabBlock.transform.localScale.x;
                var gameObject = Instantiate(blockSOs[i, j].PreFabBlock,
                    new Vector3((i-1 - GameSettings.GameSizeX / 2) * scale, 0, (j-1 - GameSettings.GameSizeY / 2) * scale),
                    Quaternion.identity,
                    World);
                Blocks[(i - 1), j - 1] = gameObject.GetComponent<Block>();
                Blocks[(i - 1) ,  j - 1].SetUp(new(i-1,j-1));
            }
        }
        // After Spawning The Object We Should Update The Map

        UpdateWorld();
    }
    public void UpdateWorld()
    {
        // Update The World 

        if (Blocks == null)
            ReSetUpBlocks();
        m_ObstacleManager?.SetUp(Blocks);
    }
}
