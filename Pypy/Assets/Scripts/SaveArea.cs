using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveArea : MonoBehaviour
{
    RectTransform rectTransform;
    Rect safeArea;
    Vector2 minAchor;
    Vector2 maxAchor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
