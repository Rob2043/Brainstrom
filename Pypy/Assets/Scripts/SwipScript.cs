using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeScript : MonoBehaviour
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

        if (swipeDelta.magnitude > deadZone)
        {
            if (SwipeEvent != null) // Проверка наличия подписчиков
            {
                if (swipeDelta.y > 0 && swipeDelta.x > 0)
                    SwipeEvent(Vector2.right);
                if (swipeDelta.y < 0 && swipeDelta.x > 0)
                    SwipeEvent(Vector2.down);
                if (swipeDelta.y > 0 && swipeDelta.x < 0)
                    SwipeEvent(Vector2.up);
                if (swipeDelta.y < 0 && swipeDelta.x < 0)
                    SwipeEvent(Vector2.left);
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
