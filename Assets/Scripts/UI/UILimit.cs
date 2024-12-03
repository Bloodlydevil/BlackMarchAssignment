using UnityEngine;

/// <summary>
///  A Limiter To Get Good Position Of The Info Box
/// </summary>
public class UILimit : MonoBehaviour
{
    [SerializeField] private RectTransform m_MouseInfoBox;

    /// <summary>
    /// Get The Element Position Such That it Does Not Show Outside The View Area
    /// </summary>
    /// <param name="MousePos"></param>
    /// <param name="ElementSize"></param>
    /// <returns></returns>
    public Vector2 GetElementPos(Vector2 MousePos, Vector2 ElementSize)
    {
        Vector2 ElementPos;

        // Check If position X Can Go Outside Box

        if (MousePos.x + ElementSize.x > m_MouseInfoBox.rect.width/2)
            ElementPos.x = MousePos.x - ElementSize.x / 2;
        else
            ElementPos.x = MousePos.x + ElementSize.x / 2;

        // Check If position Y Can Go Outside Box

        if (MousePos.y + ElementSize.y > m_MouseInfoBox.rect.height/2)
            ElementPos.y = MousePos.y - ElementSize.y / 2;
        else
            ElementPos.y = MousePos.y + ElementSize.y / 2;


        return ElementPos;
    }
}