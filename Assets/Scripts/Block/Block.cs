using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Block Which Stores All The Info Of The Block Placed On Grid
/// </summary>
public class Block : MonoBehaviour, IEqualityComparer<Block>
{
    public BlockSO BlockSO;
    [SerializeField] private GameObject m_Hover;
    [SerializeField] private GameObject m_Obstacle;
    private bool m_Occupied;
    public Vector2Int GridPosition;
    [field: SerializeField] public Transform CharacterPos {  get; private set; }
    public bool IsOccupied => m_Occupied;
    public void SetUp(Vector2Int pos)
    {
        // Set Its Grid Position for Easy Use Later On

        m_Hover.SetActive(false);
        m_Obstacle.SetActive(false);
        GridPosition = pos;
    }
    public void ChangeOccupied(bool Value)
    {
        // If Its Occupied Or Not(Player ,Enemy Or other Things Are Placed Over IT)
        m_Occupied= Value;
    }
    public void UpdateObstacle(bool Value)
    {
        //Update The Obstacle State Of This Block
        m_Obstacle.SetActive(Value);
        m_Occupied = Value;
    }
    public void ShowHover()
    {
        // When Mouse Hovers It Then Show It

        m_Hover.SetActive(true);
    }
    public void HideHover()
    {
        // When Mosue Is Not Over It Then Hide The Hover Show

        m_Hover.SetActive(false);
    }

    public bool Equals(Block x, Block y)
    {
        return x.GridPosition == y.GridPosition;
    }

    public int GetHashCode(Block obj)
    {
        if (obj == null) return 0;
        return (obj.GridPosition).GetHashCode();
    }
}