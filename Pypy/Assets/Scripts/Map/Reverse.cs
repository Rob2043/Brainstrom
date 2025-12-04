using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    private bool WasUsed = false;
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (WasUsed == false)
            {
                CustomEventBus.EventBus.Reverseblocks?.Invoke();
                transform.position = new Vector3(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y, transform.position.z + moveDirection.z);
                WasUsed = true;
            }
        }
    }
}
