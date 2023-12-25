using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckScin : MonoBehaviour
{
    public GameObject ScanObjct;
    [SerializeField] private  GameManager gameManager;
    [SerializeField] private MovePlayerScript movePlayerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Colision") && other.CompareTag("Player"))
        {
            ScanObjct = other.gameObject;
        }
    }

    public void ChooseClick()
    {
        if(ScanObjct != null && movePlayerScript.allowForBuy == true)
        {
            movePlayerScript.checkScin = true;
            gameManager.SetPlayerSkin(ScanObjct, movePlayerScript.checkScin);
        }
    }

    public void BuyButton()
    {

        if (gameManager.CountStars >= movePlayerScript.dataScins.Price && !movePlayerScript.allowForBuy)
        {
            gameManager.CountStars -= movePlayerScript.dataScins.Price;
            movePlayerScript.allowForBuy = true;
            Debug.Log(gameManager.CountStars);
            gameManager.CheckPlayerBuy(ScanObjct, movePlayerScript.allowForBuy);
        }
        else
        {
            gameManager.CheckPlayerBuy(ScanObjct, movePlayerScript.allowForBuy);
            movePlayerScript.allowForBuy = false;
        }
    }

    public void ExitClic()
    {
        SceneManager.LoadScene("MainMenu");
    }
}