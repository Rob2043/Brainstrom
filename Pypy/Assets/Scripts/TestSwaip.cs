using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwaip : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 tempPosition;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwiping = false;
    public float speed;

    public float minSwipeDistance = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
                isSwiping = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (isSwiping)
        {
            Vector2 swipeDelta = fingerUpPosition - fingerDownPosition;

            if (swipeDelta.magnitude > minSwipeDistance)
            {
                // Определите направление свайпа и выполните действие, например, перемещение объекта
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    // Горизонтальный свайп
                    if (swipeDelta.x > 0)
                    {
                        // Свайп вправо
                        // Выполните действие, например, переместите объект вправо
                        Vector3 positionZ = new Vector3(0f, 0f, 3f);
                        rb.MovePosition(positionZ);
                    }
                    else
                    {
                        // Свайп влево
                        // Выполните действие, например, переместите объект влево
                    }
                }
                else
                {
                    // Вертикальный свайп
                    if (swipeDelta.y > 0)
                    {
                        // Свайп вверх
                        // Выполните действие, например, переместите объект вверх
                    }
                    else
                    {
                        // Свайп вниз
                        // Выполните действие, например, переместите объект вниз
                    }
                }
            }

            isSwiping = false;
        }
    }
}
