using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class ChooseScinScript : MonoBehaviour
{
    private bool isRotating = false;
    private GameObject playerCheck;
    private MovePlayerScript CheckBuy;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonChoose;
    [SerializeField] private TMP_Text TextForPrice;
    [SerializeField] private Text TextForCountStars;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (PlayerPrefs.HasKey("DataBuy"))
        {
            string transition = PlayerPrefs.GetString("DataBuy");
            foreach (var player in gameManager.ArrayPlayers)
            {
                if (gameManager.playerBuy.ContainsKey(player))
                {
                    player.GetComponent<MovePlayerScript>().allowForBuy = player.name == transition;
                    gameManager.playerBuy[player] = player.name == transition;
                    Debug.Log(player.GetComponent<MovePlayerScript>().allowForBuy);
                }
                else
                {
                    gameManager.playerBuy.Add(player, player.name == transition);
                }

            }
        }
    }

    private void LateUpdate()
    {
        TextForCountStars.text = gameManager.CountStars.ToString();
        CheckScin find = FindObjectOfType<CheckScin>();
        if (find != null)
        {
            playerCheck = find.ScanObjct;

            if (playerCheck != null)
            {
                CheckBuy = playerCheck.GetComponent<MovePlayerScript>();

                if (CheckBuy != null)
                {
                    if (CheckBuy.allowForBuy == false)
                    {
                        ButtonBuy.SetActive(true);
                        TextForPrice.text = "Buy " + CheckBuy.dataScins.Price.ToString();
                        ButtonChoose.SetActive(false);
                    }
                    else
                    {
                        ButtonBuy.SetActive(false);
                        ButtonChoose.SetActive(true);
                    }
                }
            }
        }
    }

    public void LeftChoose()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y - 72, 0)));
        }
    }

    public void RightChoose()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y + 72, 0)));
        }
    }

    private IEnumerator RotateTo(Vector3 newRotation)
    {
        isRotating = true;
        float duration = 0.5f; // Продолжительность анимации в секундах
        float elapsedTime = 0f;
        Vector3 initialRotation = transform.eulerAngles;

        while (elapsedTime < duration)
        {
            transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = newRotation; // Устанавливаем окончательный угол
        isRotating = false;
    }
}
