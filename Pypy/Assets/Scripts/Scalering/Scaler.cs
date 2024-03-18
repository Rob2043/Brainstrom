using UnityEngine;
using UnityEngine.UI;

public class Scaler : MonoBehaviour
{
    ScreenOrientation screenOrientation;
    
    private void Awake()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        screenOrientation = Screen.orientation;

        CanvasScaler canvasScaler = gameObject.GetComponent<CanvasScaler>();
       /*  canvasScaler.referenceResolution = new Vector2(screenWidth, screenHeight); */
    }
}
