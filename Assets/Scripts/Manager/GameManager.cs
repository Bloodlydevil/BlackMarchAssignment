using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    [SerializeField] private PlayerManager m_playerManager;
    [SerializeField] private EnemyManager m_enemyManager;
    [SerializeField] private WorldMapGenerator m_worldMapGenerator;
    private PlayerAI m_PlayerAI;
    private EnemyAI  m_EnemyAI;

    public event Action OnPlayerTurn;
    public event Action OnEnemyTurn;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // Setting Up All The Player And Enemy And Givng Them Respective AI To Use To Navigate To A Position

        m_PlayerAI=new PlayerAI();
        m_PlayerAI.SetUp(m_worldMapGenerator.Blocks);
        m_EnemyAI = new EnemyAI();
        m_EnemyAI.SetUp(m_worldMapGenerator.Blocks);
        m_playerManager.Setup(m_PlayerAI);
        m_enemyManager.Setup(m_EnemyAI);
    }
    public void OnPlayerTurnFinnish(Block PlayerPos)
    {
        // Player Has Finifhed Turn So Now Enemy Turn

        m_enemyManager.EnemyTurn(PlayerPos);
        OnEnemyTurn?.Invoke();
    }
    public void OnEnemyTurnFinnish()
    {
        // Enemy HAs finished Turn So Now It Is Player Turn

        m_playerManager.AllowPlayerMove();
        OnPlayerTurn?.Invoke();
    }
}
