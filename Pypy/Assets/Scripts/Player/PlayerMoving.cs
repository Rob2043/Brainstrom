using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private float speed;
    public AudioSource MainAudio;
    private int star = 0;
    public AudioSource Audio;
    private Rigidbody rb;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        EventBus.WasMoving = HandleSwipePlayer;
    }
    private void Start()
    {
        if (gameObject != Iinstance.instance.SelectScin)
        {
            gameObject.SetActive(false);
            Instantiate(gameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EventBus.CheckStars += OnStar;
    }
    private void OnDisable()
    {
        EventBus.CheckStars -= OnStar;
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
        if (other.CompareTag("Finish"))
        {
            EventBus.CheckEnd.Invoke();
            Time.timeScale = 0f;
            Audio.Play();
            MainAudio.enabled = false;
            endPanel.SetActive(true);
            float localScene = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("MaxLevel") <= localScene + 1)
            {
                PlayerPrefs.SetInt("MaxLevel", (int)(localScene + 1));

                PlayerPrefs.Save();
            }
            //float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
            //float EndLevelValue = Level - (int)Level;
            //if (EndLevelValue == 0)
            //{
            //    InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
            //    Ads.ShowAd();
            //}
        }
        else if (other.CompareTag("star"))
        {
            star = 1;
        }
    }
    private (int, int) OnStar(int stars)
    {
        return (SceneManager.GetActiveScene().buildIndex + 1, stars += star);
    }

}
