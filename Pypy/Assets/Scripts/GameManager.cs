using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] GameObject[] ArrayPlayers;
    private GameObject selectedPlayer;
    private GameObject selectedSecondPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Подпишем метод на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string SkinName = "";
        if (scene.name == "Scins")
        {
            ArrayPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (PlayerPrefs.HasKey("SaveScin"))
            {
                SkinName = PlayerPrefs.GetString("SaveScin");
            }
            // Перебираем всех игроков и обновляем или добавляем их скины
            foreach (var player in ArrayPlayers)
            {
                if (playerSkins.ContainsKey(player))
                {
                    playerSkins[player] = player.name == SkinName;
                }
                else
                {
                    playerSkins.Add(player, player.name == SkinName);
                }
            }
        }

        if (SceneManager.GetActiveScene().name != "Scins" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            ArrayPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in ArrayPlayers)
            {
                if (playerSkins.ContainsKey(player) && !playerSkins[player])
                {
                    player.SetActive(false);
                }
            }
        }
        if (SceneManager.GetActiveScene().name != "Scins" & SceneManager.GetActiveScene().name != "MainMenu")
        {
            ArrayPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in ArrayPlayers)
            {
                if (player.name != PlayerPrefs.GetString("SaveScin"))
                {
                    player.SetActive(false);
                }
            }
        }
    }

    public void SetPlayerSkin(GameObject player, bool hasSkin)
    {
        if (playerSkins.ContainsKey(player))
        {
            playerSkins[player] = hasSkin;
            Debug.Log(hasSkin);
            PlayerPrefs.SetString("SaveScin", player.name);
            PlayerPrefs.Save();
        }
        else
        {
            playerSkins.Add(player, hasSkin);
        }
    }

    public bool GetPlayerSkin(GameObject player)
    {
        if (playerSkins.ContainsKey(player))
        {
            return playerSkins[player];
        }
        return false;
    }

    public GameObject GetSelectedPlayer()
    {
        return selectedPlayer;
    }

    public GameObject GetSelectedSecondPlayer()
    {
        return selectedSecondPlayer;
    }

    public void SetSelectedPlayer(GameObject player)
    {
        selectedPlayer = player;
    }

    public void SetSelectedSecondPlayer(GameObject secondPlayer)
    {
        selectedSecondPlayer = secondPlayer;
    }
}