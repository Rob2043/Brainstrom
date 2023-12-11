using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckScin : MonoBehaviour
{
    public GameObject ScanObjct;
    public  GameObject player;
    private  GameObject secondPlayer;
    private int check = 0;
    public GameManager gameManager;

    // Пример логирования в методе Start() класса CheckScin
    //private void Start()
    //{
    //    gameManager = FindObjectOfType<GameManager>();

    //    if (gameManager.selectedPlayer != null)
    //    {
    //        InitializePlayer(gameManager.selectedPlayer);
    //    }
    //}

    //private void InitializePlayer(GameObject playerObject)
    //{
    //    MovePlayerScript movePlayerScript = playerObject.GetComponent<MovePlayerScript>();
    //    bool hasSkin = gameManager.GetPlayerSkin(playerObject);
    //    movePlayerScript.checkScin = hasSkin;
    //    Debug.Log("Player not null: " + playerObject.name);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Colision") && other.CompareTag("Player"))
        {
            ScanObjct = other.gameObject;
        }
    }

    public void ChooseClick()
    {
        //if (player != null)
        //{
        //    MovePlayerScript movePlayerScript = player.GetComponent<MovePlayerScript>();
        //    movePlayerScript.checkScin = true;
        //    gameManager.SetPlayerSkin(player, movePlayerScript.checkScin);
        //    player = gameManager.selectedPlayer;
        //    if (secondPlayer != null && check == 1)
        //    {
        //        check = 0;
        //        MovePlayerScript movePlayerScriptSecond = secondPlayer.GetComponent<MovePlayerScript>();
        //        movePlayerScriptSecond.checkScin = false;
        //        gameManager.SetPlayerSkin(player, movePlayerScript.checkScin);
        //        secondPlayer = null;
        //    }
        //}

        //if (secondPlayer != null && check == 0)
        //{
        //    MovePlayerScript movePlayerScriptSecond = secondPlayer.GetComponent<MovePlayerScript>();
        //    movePlayerScriptSecond.checkScin = true;
        //    gameManager.SetPlayerSkin(secondPlayer, movePlayerScriptSecond.checkScin);
        //    secondPlayer = gameManager.selectedSecondPlayer;
        //    if (player != null)
        //    {
        //        check = 1;
        //        MovePlayerScript movePlayerScript = player.GetComponent<MovePlayerScript>();
        //        movePlayerScript.checkScin = false;
        //        gameManager.SetPlayerSkin(secondPlayer, movePlayerScriptSecond.checkScin);
        //        player = null;
        //    }
        //}

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