using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckScin : MonoBehaviour
{
    public GameObject ScanObjct;
    public GameManager gameManager;

    // Пример логирования в методе Start() класса CheckScin
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Colision") && other.CompareTag("Player"))
        {
            ScanObjct = other.gameObject;
        }
    }

    public void ChooseClick()
    {
        if(ScanObjct != null && ScanObjct.GetComponent<MovePlayerScript>().allowForBuy == true)
        {
            MovePlayerScript movePlayerScript = ScanObjct.GetComponent<MovePlayerScript>();
            movePlayerScript.checkScin = true;
            gameManager.SetPlayerSkin(ScanObjct, movePlayerScript.checkScin);
        }
    }

    public void BuyButton()
    {
        MovePlayerScript Scin = ScanObjct.GetComponent<MovePlayerScript>();

        if (gameManager.CountStars >= Scin.dataScins.Price && !Scin.allowForBuy)
        {
            gameManager.CountStars -= Scin.dataScins.Price;
            Scin.allowForBuy = true;
            int sum = PlayerPrefs.GetInt("CountStars", gameManager.CountStars);
            gameManager.CountStars = sum;
            Debug.Log(gameManager.CountStars);
            gameManager.CheckPlayerBuy(ScanObjct, Scin.allowForBuy);
        }
        else
        {
            gameManager.CheckPlayerBuy(ScanObjct, Scin.allowForBuy);
            Scin.allowForBuy = false;
        }
    }

    public void ExitClic()
    {
        SceneManager.LoadScene("MainMenu");
    }
}