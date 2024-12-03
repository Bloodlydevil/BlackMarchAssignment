using UnityEngine;

/// <summary>
/// A Block SO To Store Basic Block Data
/// </summary>
[CreateAssetMenu(fileName ="Block",menuName ="SO/Block")]
public class BlockSO : ScriptableObject
{
    /// <summary>
    /// The Prefab Block Used To Create In The Game ( Every Block Can Be Made Unique )
    /// </summary>
    [field: SerializeField] public GameObject PreFabBlock { get; private set; }
    [field: SerializeField] public string Name {  get; private set; }
    [Min(1)]
    [field: SerializeField] public int Health { get; private set; }

    // More Data Can Be Placed Here
}
