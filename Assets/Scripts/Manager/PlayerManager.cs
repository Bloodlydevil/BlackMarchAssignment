using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private WorldMapGenerator m_WorldMapGenerator;
    [SerializeField] private MouseManager m_MouseManager;
    public Player Player { get; private set; }

    public void AllowPlayerMove()
    {
        // PLayer Can Move Now

        Player.AllowPlayerMove();
    }


    public void Setup(IAI AIUsed)
    {
        var Blocks = m_WorldMapGenerator.Blocks;

        // Finding The First Positon PLayer Can Be Placed And PLacing It

        for (int i = 0; i < Blocks.GetLength(0); i++)
        {
            for (int j = 0; j < Blocks.GetLength(1); j++)
            {
                if (!Blocks[i, j].IsOccupied)
                {
                    Player = Instantiate(m_PlayerPrefab, Blocks[i, j].CharacterPos.position, Quaternion.identity,m_WorldMapGenerator.transform).GetComponent<Player>();
                    Player.transform.localScale *= 2;
                    Player.SetUp(AIUsed, m_MouseManager, Blocks[i, j]);
                    return;
                }
            }
        }
        Debug.Log("No Position Found To Spawn The Player");
    }
}
