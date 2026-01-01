using UnityEngine;

public class Reverse : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    private AudioSource _audioSource;
    private bool WasUsed = false;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (WasUsed == false)
            {
                CustomEventBus.EventBus.Reverseblocks?.Invoke();
                transform.position = new Vector3(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y, transform.position.z + moveDirection.z);
                _audioSource.Play();
                WasUsed = true;
            }
        }
    }
}
