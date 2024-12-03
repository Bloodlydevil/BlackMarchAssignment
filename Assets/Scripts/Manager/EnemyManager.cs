using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private WorldMapGenerator m_WorldMapGenerator;
    public Enemy Enemy { get; private set; }

    public void EnemyTurn(Block playerLoc)
    {
        // It Is Enemy Turn So Move Enemy

        Enemy.OnMove(playerLoc);
    }
    public void Setup(IAI AIUsed)
    {
        var Blocks = m_WorldMapGenerator.Blocks;

        // Finding The first Postion Enemy Can Be Placed And Placing It

        for (int i = Blocks.GetLength(0)-1; i >-1; i--)
        {
            for (int j = Blocks.GetLength(1)-1; j>-1; j--)
            {
                if (!Blocks[i, j].IsOccupied)
                {
                    // Enemy Is Place At this Positon

                    Enemy = Instantiate(m_PlayerPrefab, Blocks[i, j].CharacterPos.position, Quaternion.identity,m_WorldMapGenerator.transform).GetComponent<Enemy>();
                    Enemy.transform.localScale *= 2;
                    Enemy.SetUp(AIUsed, Blocks[i, j]);
                    
                    return;
                }
            }
        }
        Debug.Log("No Position Found To Spawn The Enemy");
    }
}
