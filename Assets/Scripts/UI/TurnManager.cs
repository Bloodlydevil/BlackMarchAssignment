using TMPro;
using UnityEngine;

/// <summary>
///  Manage The Turn With Is Shown On Screen
/// </summary>
public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI m_Turn;

    private void Awake()
    {
        // Subsribe To Turn Change Events

        gameManager.OnEnemyTurn += GameManager_OnEnemyTurn;
        gameManager.OnPlayerTurn += GameManager_OnPlayerTurn;
        GameManager_OnPlayerTurn();
    }

    private void GameManager_OnPlayerTurn()
    {
        // Player Turn

        m_Turn.text = "Player Turn";
        m_Turn.color = new Color(0.5f, 1, 0.5f);
    }

    private void GameManager_OnEnemyTurn()
    {
        //Enemy Turn

        m_Turn.text = "Enemy Turn";
        m_Turn.color = new Color(1, 0.5f, 0.5f);
    }
}
