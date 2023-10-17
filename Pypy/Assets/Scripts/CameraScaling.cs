using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    public Camera cam;

    public float initialWidth;
    public float initialSize;

    private void Start()
    {
        float currentWidth = cam.pixelWidth; 
        float ratio = currentWidth / initialWidth; 
        float newSize = initialSize / ratio; 

        cam.orthographicSize = newSize;
    }
}