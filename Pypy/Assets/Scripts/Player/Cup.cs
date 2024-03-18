﻿using UnityEngine;
public class Cup : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection;
    [SerializeField] private float speed;

    private Rigidbody rb;
    
    public bool checkLevel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        checkLevel = true;
    }
    private void HandleSwipe(Vector2 direction)
    {
        if (rb != null && checkLevel)
        {
            bool isPositionXFrozen = (rb.constraints & RigidbodyConstraints.FreezePositionX) != 0;
            bool isPositionYFrozen = (rb.constraints & RigidbodyConstraints.FreezePositionY) != 0;
            bool isPositionZFrozen = (rb.constraints & RigidbodyConstraints.FreezePositionZ) != 0;
            // Здесь вы можете выполнить необходимые действия в зависимости от направления свайпа.
            if (direction == Vector2.left && isPositionXFrozen & isPositionYFrozen)
            {
                rb.velocity -= moveDirection * speed;
            }
            else if (direction == Vector2.right && isPositionXFrozen & isPositionYFrozen)
            {
                rb.velocity += moveDirection * speed;

            }
            else if (direction == Vector2.up && isPositionZFrozen & isPositionYFrozen)
            {
                rb.velocity -= moveDirection * speed;

            }
            else if (direction == Vector2.down && isPositionZFrozen & isPositionYFrozen)
            {
                rb.velocity += moveDirection * speed;
            }
        }

    }
    private void OnEnable() {
        SwipeScript.SwipeEvent += HandleSwipe;
        SwipeScript1.SwipeEvent += HandleSwipe;
    }
    private void OnDisable() {
        SwipeScript.SwipeEvent -= HandleSwipe;
        SwipeScript1.SwipeEvent -= HandleSwipe;
    }
}