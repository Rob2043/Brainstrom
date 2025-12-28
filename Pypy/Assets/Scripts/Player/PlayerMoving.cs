using CustomEventBus;
using Pypy;
using UnityEngine;
using Pypy.Consts;


public class PlayerMoving : MonoBehaviour, IInstansePlayer
{
    private Vector3 moveDirection  = new(1,0,1);
    private float speed = 12;
    [SerializeField] private DataOfPlayer _dataOfPlayer;
    public DataOfPlayer DataOfPlayer { get => _dataOfPlayer; set => _dataOfPlayer = value; }
    private Rigidbody rb;
    private bool isMoving = true;

    private void Awake()
    {
        DataOfPlayer = _dataOfPlayer;
        rb = GetComponent<Rigidbody>();
        EventBus.WasMoving += HandleSwipePlayer;
        EventBus.CallOffMoving += OffMoving;
    }
    private void OffMoving()
    {
       isMoving = false; 
       rb.velocity = Vector3.zero;
       rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void HandleSwipePlayer(Vector2 direction)
    {
        if (rb != null && isMoving)
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
            rb.AddForce(force*Time.fixedDeltaTime * 40, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
      if(collision.gameObject.CompareTag(StringConstants.WALL_TAG))
        {
            rb.velocity = Vector3.zero;
        }  
    }
}
