using Unity.VisualScripting;
using UnityEngine;


public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 moveDirection;
    private bool BackIsReady = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (BackIsReady == false)
        {
            rb.velocity += moveDirection * speed;
        }
        else
        {
            rb.velocity -= moveDirection * speed;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            BackIsReady = !BackIsReady;
        }
    }
}
