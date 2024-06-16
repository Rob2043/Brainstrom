using UnityEngine;

public class SaveArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect safeArea;
    private Vector2 minAchor;
    private Vector2 maxAchor;

    private void Awake()
    {
        safeArea = Screen.safeArea;
        minAchor = safeArea.position;
        maxAchor = minAchor + safeArea.size;

        minAchor.x /= Screen.width;
        minAchor.y /= Screen.height;
        maxAchor.x /= Screen.width;
        maxAchor.y /= Screen.height;

        rectTransform.anchorMin = minAchor;
        rectTransform.anchorMax = maxAchor;
    }
}
