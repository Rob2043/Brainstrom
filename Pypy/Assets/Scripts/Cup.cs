﻿using UnityEngine;
public class Cup : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection;
    private Rigidbody rb;
    [SerializeField] private float speed;
    public bool checkLevel;

    private void Start()
    {
        SwipeScript.SwipeEvent += HandleSwipe;
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
                GetComponent<Rigidbody>().velocity -= moveDirection * speed;
            }
            else if (direction == Vector2.right && isPositionXFrozen & isPositionYFrozen)
            {
                GetComponent<Rigidbody>().velocity += moveDirection * speed;

            }
            else if (direction == Vector2.up && isPositionZFrozen & isPositionYFrozen)
            {
                GetComponent<Rigidbody>().velocity -= moveDirection * speed;

            }
            else if (direction == Vector2.down && isPositionZFrozen & isPositionYFrozen)
            {
                GetComponent<Rigidbody>().velocity += moveDirection * speed;
            }
        }

    }
}