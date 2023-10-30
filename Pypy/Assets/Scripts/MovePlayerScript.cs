using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float speed;
    public GameObject panel;

    public bool checkScin = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SwipeScript.SwipeEvent += HandleSwipePlayer;
        if (SceneManager.GetActiveScene().name != "Scins")
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            panel = GameObject.FindGameObjectWithTag("localPanel");
            panel.SetActive(false);
        }
    }

    private void HandleSwipePlayer(Vector2 direction)
    {
        if (rb != null)
        {

            Vector3 force = Vector3.zero;

            if (direction == Vector2.left)
            {
                force = new Vector3(0, 0, -moveDirection.z * speed);
            }
            else if (direction == Vector2.right)
            {
                force = new Vector3(0, 0, moveDirection.z * speed);
            }
            else if (direction == Vector2.up)
            {
                force = new Vector3(-moveDirection.x * speed, 0, 0);
            }
            else if (direction == Vector2.down)
            {
                force = new Vector3(moveDirection.x * speed, 0, 0);
            }

            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player") && other.CompareTag("Finish"))
        {
            panel.SetActive(true);
            speed = 0f;
        }
        else if (gameObject.CompareTag("SpawnEmpty") && other.CompareTag("Finish"))
        {
            panel.SetActive(true);
            speed = 0f;
        }
    }
}
