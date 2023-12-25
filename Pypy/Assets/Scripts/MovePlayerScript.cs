using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float speed;
    [SerializeField] private Text TextCountStars;
    [SerializeField] private  GameObject panel;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Cup[] Box;
    [SerializeField] private TextStarScript Time;
    [SerializeField] private CheckAmountStar checkAmountStar;
    [SerializeField] private InterstitialAdExample Ads;
    public bool checkScin = false;
    public bool allowForBuy = false;
    public DataItems dataScins;

    private void Awake()
    {
        SwipeScript.SwipeEvent += HandleSwipePlayer;

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Scins")
        {
            panel.SetActive(false);
        }
    }
    private void HandleSwipePlayer(Vector2 direction)
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

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player") && other.CompareTag("Finish"))
        {
            float Level = (SceneManager.GetActiveScene().buildIndex - 1) / 3;
            float EndLevelValue = Level - (int)Level;
            Debug.Log(EndLevelValue);
            if (EndLevelValue == 0)
            {
                Ads.ShowAd();
            }
            panel.SetActive(true);
            speed = 0;
            foreach (var b in Box)
            {
                b.checkLevel = false;
            }
            if (!PlayerPrefs.HasKey($"OneStar{SceneManager.GetActiveScene().name}"))
            {
                gameManager.CountStars += 1;
                checkAmountStar.SaveStartData(true, false, false);
            }
            if (Time != null && gameManager != null)
            {
                if (Time.DieTime > 0)
                {
                    gameManager.ThirdStar.SetActive(true);
                    if (!PlayerPrefs.HasKey($"ThreeStar{SceneManager.GetActiveScene().name}"))
                    {
                        gameManager.CountStars += 1;
                        checkAmountStar.SaveStartData(false, false, true);
                    }
                }
                Time.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            TextCountStars.text = $"{gameManager.CountStars}";
            PlayerPrefs.SetInt("CountStars", gameManager.CountStars);
            PlayerPrefs.Save();
        }
    }
}
