using UnityEngine;

/// <summary>
/// Obstacle Data Stored
/// </summary>
[CreateAssetMenu(fileName ="Obstacle Data",menuName ="SO/Obstacle")]
public class ObstacleDataSO : ScriptableObject
{
    /// <summary>
    /// The Obstacles Used In The Game
    /// </summary>
    public bool[] obstacles= new bool[GameSettings.GameSizeX * GameSettings.GameSizeY];
}
