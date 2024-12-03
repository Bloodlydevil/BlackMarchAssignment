using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldMapGenerator))]
public class WorldMapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (Application.isPlaying)
            return;

        WorldMapGenerator generator = (WorldMapGenerator)target;

        // Create A Button To Create World
        if (GUILayout.Button("Create World"))
        {
            DestroyWorld(generator);
            generator.SetUp();
        }

        // Create A Button To Destroy The World
        if (GUILayout.Button("Destroy World"))
        {
            DestroyWorld(generator);
        }
    }
    private void DestroyWorld(WorldMapGenerator generator)
    {
        // Destroying All The Child Of The Map(All Block Are Its Child)

        if (generator.World == null)
        {
            Debug.LogError("World Transform is not assigned in the inspector.");
            return;
        }
        while (generator.World.childCount > 0)
        {
            DestroyImmediate(generator.World.GetChild(0).gameObject);
        }
    }
}
