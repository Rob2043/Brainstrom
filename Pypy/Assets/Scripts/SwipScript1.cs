﻿using UnityEngine;


public class SwipeScript1 : MonoBehaviour
{
    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    [SerializeField] private float deadZone = 10f;
    private bool isSwiping;
    private readonly bool isMobile = Application.isMobilePlatform;
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        swipeDelta = Vector2.zero;

        if (isSwiping)
        {
            if (!isMobile && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            else if (Input.touchCount > 0)
                swipeDelta = Input.GetTouch(0).position - tapPosition;
        }

        if (Mathf.Abs(swipeDelta.x) > deadZone || Mathf.Abs(swipeDelta.y) > deadZone)
        {
            if (SwipeEvent != null) // Проверка наличия подписчиков
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    // Горизонтальный свайп
                    SwipeEvent(swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                }
                else
                {
                    // Вертикальный свайп
                    SwipeEvent(swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                }
            }

            ResetSwipe();
        }
    }



    private void ResetSwipe()
    {
        isSwiping = false;
        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
}
