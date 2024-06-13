using CustomEventBus;
using Pypy;
using UnityEngine;


public class PlayerMoving : MonoBehaviour, IInstansePlayer
{
    private Vector3 moveDirection  = new Vector3(1,0,1);
    private float speed = 10;
    [SerializeField] private DataOfPlayer _dataOfPlayer;
    public DataOfPlayer DataOfPlayer { get => _dataOfPlayer; set => _dataOfPlayer = value; }
    private int star = 0;
    private Rigidbody rb;

    private void Awake()
    {
        DataOfPlayer = _dataOfPlayer;
        rb = GetComponent<Rigidbody>();
        EventBus.WasMoving = HandleSwipePlayer;
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
        if(other.CompareTag("Wall"))
        {
            rb.AddForce(Vector3.zero,ForceMode.VelocityChange);
        }
    }
}
