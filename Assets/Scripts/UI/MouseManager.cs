using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class To Manage Mouse Related Things
/// </summary>
public class MouseManager : MonoBehaviour
{

    [SerializeField] private Camera m_camera;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private UIInfoDisplay m_InfoUI;
    [SerializeField] private UILimit m_Limit;

    /// <summary>
    /// The Block On Which Mouse Is Currently
    /// </summary>
    public Block HoveredBlock {  get; private set; }
    private void Update()
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();

        // Convert Mouse Position To Screen Space

        Ray MouseToWorldRay=m_camera.ScreenPointToRay(MousePos);

        // Check If Any Object Is In The Line Of Mouse And World Position
        if (Physics.Raycast(MouseToWorldRay, out RaycastHit hitInfo, float.MaxValue, m_GroundLayer))
        {
            // Some Object Is Found Of The Layer Ground

            if (hitInfo.collider.gameObject.TryGetComponent<Block>(out Block block))
            {
                // Object Has A Block So It Is A Block

                m_InfoUI.gameObject.SetActive(true);
                MousePos -= m_camera.pixelRect.size/2;

                // Get The Appropriet Position Of The Info Box
                m_InfoUI.rectTranform.anchoredPosition = m_Limit.GetElementPos(MousePos, m_InfoUI.rectTranform.rect.size);
                m_InfoUI.Display(block);

                // Hide The Last Hovered Block
                HoveredBlock?.HideHover();
                block.ShowHover();
                HoveredBlock = block;
            }
        }
        else
        {
            // No Object Has Collided

            m_InfoUI.gameObject.SetActive(false);
            HoveredBlock?.HideHover();
            HoveredBlock = null;
        }
    }
}
