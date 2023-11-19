using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public float t = 0;
    public float Amp = 0.25f;
    public float Freq = 2;
    public float Offset = 0;
    public Vector3 StartPos;
    public bool checkAnimation = true;

    void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        if (checkAnimation)
        {
            t = t + Time.deltaTime;
            Offset = Amp * Mathf.Sin(t * Freq);

            transform.position = StartPos + new Vector3(0, Offset, 0);
        }
    }
}