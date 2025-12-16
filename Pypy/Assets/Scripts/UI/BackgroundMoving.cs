using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField, Range(0.1f, 1f)] private float _timeToCheck;
    private bool BackIsReady = false;
    private Rigidbody rb;
    private int _countOfCollisions = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (BackIsReady)
        {
            rb.velocity += moveDirection * speed;
            Debug.Log("Moving Background");
        }
        else
        {
            Debug.Log("Moving Background Back");
            rb.velocity -= moveDirection * speed;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && _countOfCollisions < 1)
        {
            _countOfCollisions++;
            BackIsReady = !BackIsReady;
            StartCoroutine(CheckCollision());
        }
    }
    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _countOfCollisions = 0;
        }
    }

    IEnumerator CheckCollision()
    {
        yield return new WaitForSeconds(_timeToCheck);
    }
}
