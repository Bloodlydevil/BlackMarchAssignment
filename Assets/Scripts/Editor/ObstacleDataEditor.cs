using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleDataSO))]
public class ObstacleDataEditor : Editor
{
    private ObstacleDataSO obstacleData;

    public override void OnInspectorGUI()
    {

        obstacleData = (ObstacleDataSO)target;

        for (int x = 0; x < GameSettings.GameSizeX; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < GameSettings.GameSizeY; y++)
            {
                // Create A Color Based On If Thier IS a Block Or Not
                Color originalColor = GUI.backgroundColor; 
                GUI.backgroundColor = obstacleData.obstacles[x+ y * GameSettings.GameSizeX] ? Color.red : Color.white;

                // Create A Button On That Place
                if (GUILayout.Button("", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    // Update The Block Based On The Button
                    obstacleData.obstacles[x + y * GameSettings.GameSizeX] = !obstacleData.obstacles[x + y * GameSettings.GameSizeX];

                    // Save The Changes
                    EditorUtility.SetDirty(obstacleData) ;
                    AssetDatabase.SaveAssets();
                    Repaint();
                    AssetDatabase.Refresh();

                    // Find The Generator And Update The World Map
                    FindAnyObjectByType<WorldMapGenerator>().UpdateWorld();

                }
                // Revert Back To The Original Color
                GUI.backgroundColor = originalColor;
            }
            GUILayout.EndHorizontal();
        }
    }
}
