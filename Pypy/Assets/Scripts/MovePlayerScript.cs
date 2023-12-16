using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float speed;
    public GameObject panel;
    public bool checkScin = false;
    private Rigidbody rb;
    public bool checkStar;
    public bool allowForBuy = false;
    public DataItems dataScins;
    private GameObject[] Box = { };


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SwipeScript.SwipeEvent += HandleSwipePlayer;

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Scins")
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            panel = GameObject.FindGameObjectWithTag("localPanel");
            panel.SetActive(false);
        }
    }

    private void Update()
    {
        GameObject BoxInScene = GameObject.FindGameObjectWithTag("Cube");
        Box.Append(BoxInScene);
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
            checkStar = true;
            PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex + 1); // Save the current level to PlayerPrefs
            PlayerPrefs.Save(); // Save the PlayerPrefs data

            foreach (GameObject b in Box)
            {
                b.GetComponent<Cup>().checkLevel = false;
            }

            GameObject Time = GameObject.FindGameObjectWithTag("Time");
            GameObject gameManager = GameObject.FindGameObjectWithTag("GlobalManager");
            GameObject ThirdStar = gameManager.GetComponent<GameManager>().ThirdStar;

            if (Time != null && gameManager != null)
            {
                float dieTime = Time.GetComponent<TextStarScript>().DieTime;

                if (dieTime > 0)
                {
                    ThirdStar.SetActive(true);
                    gameManager.GetComponent<GameManager>().CountStars += 1;
                }
                else
                {
                    gameManager.GetComponent<GameManager>().CountStars += 0;
                }

                Time.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
