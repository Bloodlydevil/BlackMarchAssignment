using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleDataSO obstacleData;
    public void SetUp(Block[,] GeneratedBlocks)
    {
        // Set Up All The Obstacle Based On The Data Set By User

        for (int i = 0;i< GameSettings.GameSizeX; i++)
        {
            for (int j = 0; j < GameSettings.GameSizeY; j++)
            {
                GeneratedBlocks[i,j ].UpdateObstacle(obstacleData.obstacles[i + j * GameSettings.GameSizeX]);
            }
        }
    }
}
