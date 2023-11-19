using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckScin : MonoBehaviour
{
    public static GameObject player;
    public static GameObject secondPlayer;
    private int check = 0;
    public GameManager gameManager;
    private bool IsClickPlayer;
    private bool IsClickSecondPlayer;

    // Пример логирования в методе Start() класса CheckScin
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager.GetSelectedPlayer() != null)
        {
            InitializePlayer(gameManager.GetSelectedPlayer());
        }

        if (gameManager.GetSelectedSecondPlayer() != null)
        {
            InitializePlayer(gameManager.GetSelectedSecondPlayer());
        }
    }
    private void InitializePlayer(GameObject playerObject)
    {
        MovePlayerScript movePlayerScript = playerObject.GetComponent<MovePlayerScript>();
        bool hasSkin = gameManager.GetPlayerSkin(playerObject);
        movePlayerScript.checkScin = hasSkin;
        Debug.Log("Player not null: " + playerObject.name);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Player"))
        {
            if (!IsClickPlayer)
            {
                player = other.gameObject;
            }
            else if (!IsClickSecondPlayer)
            {
                secondPlayer = other.gameObject;
            }
        }
    }
    public void ChooseClick()
    {
        if (player != null)
        {
            gameManager.SetSelectedPlayer(player);
            IsClickPlayer = true;

            // Set the skin state for the selected player immediately
            MovePlayerScript movePlayerScript = player.GetComponent<MovePlayerScript>();
            movePlayerScript.checkScin = true;
            gameManager.SetPlayerSkin(player, movePlayerScript.checkScin);

            if (secondPlayer != null && check == 1)
            {
                check = 0;
                MovePlayerScript movePlayerScriptSecond = secondPlayer.GetComponent<MovePlayerScript>();
                movePlayerScriptSecond.checkScin = false;
                gameManager.SetPlayerSkin(player, movePlayerScript.checkScin);
                secondPlayer = null;
                IsClickSecondPlayer = false;
            }
        }

        if (secondPlayer != null && check == 0)
        {
            gameManager.SetSelectedSecondPlayer(secondPlayer);
            IsClickSecondPlayer = true;

            // Set the skin state for the selected second player immediately
            MovePlayerScript movePlayerScriptSecond = secondPlayer.GetComponent<MovePlayerScript>();
            movePlayerScriptSecond.checkScin = true;
            gameManager.SetPlayerSkin(secondPlayer, movePlayerScriptSecond.checkScin);

            if (player != null)
            {
                check = 1;
                MovePlayerScript movePlayerScript = player.GetComponent<MovePlayerScript>();
                movePlayerScript.checkScin = false;
                gameManager.SetPlayerSkin(secondPlayer, movePlayerScriptSecond.checkScin);
                player = null;
                IsClickPlayer = false;
            }
        }
    }



    public void ExitClic()
    {
        if (player != null)
        {
            PlayerPrefs.SetString("SavePlayer", player.name);
            PlayerPrefs.SetInt("Player1_checkScin", player.GetComponent<MovePlayerScript>().checkScin ? 1 : 0);
            PlayerPrefs.Save();
        }
        if (secondPlayer != null)
        {
            PlayerPrefs.SetString("SavePlayerSecond", secondPlayer.name);
            PlayerPrefs.SetInt("Player2_checkScin", secondPlayer.GetComponent<MovePlayerScript>().checkScin ? 1 : 0);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("MainMenu");
    }

}
