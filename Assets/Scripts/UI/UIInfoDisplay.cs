using TMPro;
using UnityEngine;

/// <summary>
/// Text To Show When Mouse Over That Block
/// </summary>
public class UIInfoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI BlockHealth;
    [SerializeField] private TextMeshProUGUI Position;
    [SerializeField] private TextMeshProUGUI Obstracle;
    // More Info Could Be Showed

    public RectTransform rectTranform {  get;private set; }

    private void Awake()
    {
        rectTranform = (RectTransform)transform;
    }

    public void Display(Block block)
    {
        // This Block Is Shown

        Name.text =$"Name => {block.BlockSO.Name}";
        BlockHealth.text=$"Max Health =>{block.BlockSO.Health}";
        Position.text=$"Block Position =>{block.GridPosition.x} , {block.GridPosition.y}";
        Obstracle.text = $"Obstracle State=>{block.IsOccupied}"; 
    }
}
