﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] public GameObject[] ArrayPlayers;
    public GameObject selectedPlayer;
    public GameObject selectedSecondPlayer;
    [SerializeField] private GameObject EmptyPlayer;
    public int CountStars;
    public GameObject TextCountStars;
    [SerializeField] private GameObject BasicPlayer;
    public bool StopCount = false;
    public static bool FirstAddToPlayerBuy = true;
    public GameObject ThirdStar;

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
            return;
        }
        //PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scins")
        {
            foreach (var player in ArrayPlayers)
            {
                //Debug.Log(player.name + playerBuy.ContainsKey(player));
                if (PlayerPrefs.HasKey("PlayerBuy_" + player.name))
                {
                    Debug.Log(player.name + PlayerPrefs.HasKey("PlayerBuy_" + player.name));
                    bool transition = PlayerPrefs.GetInt("PlayerBuy_" + player.name, 1) == 1;
                    Debug.Log(transition);
                    GameObject transitionPlayer = GameObject.Find(player.name);
                    transitionPlayer.GetComponent<MovePlayerScript>().allowForBuy = transition;
                }
            }

            if (PlayerPrefs.HasKey("SaveScin"))
            {
                string SkinName = PlayerPrefs.GetString("SaveScin");
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
        }
        else if (scene.name != "MainMenu")
        {
            bool check = true;
            EmptyPlayer = GameObject.FindGameObjectWithTag("SpawnEmpty");
            ThirdStar = GameObject.FindGameObjectWithTag("ThirdStar");
            ThirdStar.SetActive(false);
            foreach (var player in ArrayPlayers)
            {
                if (player.name == PlayerPrefs.GetString("SaveScin"))
                {
                    check = false;
                    GameObject newScin = Instantiate(player, EmptyPlayer.transform.position, EmptyPlayer.transform.rotation);
                    newScin.tag = player.tag;
                    Destroy(EmptyPlayer);
                }
            }
            if (check)
            {
                Vector3 newPosition = new Vector3(EmptyPlayer.transform.position.x, EmptyPlayer.transform.position.y + 0.5f, EmptyPlayer.transform.position.z);
                Instantiate(BasicPlayer, newPosition, EmptyPlayer.transform.rotation);
            }
            TextCountStars = GameObject.FindGameObjectWithTag("CountStar");
        }
    }

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Scins" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            GameObject PlayerForStar = GameObject.FindGameObjectWithTag("Player");

            if (PlayerForStar != null && PlayerForStar.GetComponent<MovePlayerScript>() != null)
            {
                MovePlayerScript movePlayerScript = PlayerForStar.GetComponent<MovePlayerScript>();

                if (movePlayerScript.checkStar && !StopCount)
                {
                    if (TextCountStars != null)
                    {
                        Text text = TextCountStars.GetComponent<Text>();
                        CountStars++;
                        text.text = CountStars.ToString();
                        // Add a debug statement to print the updated CountStars
                        Debug.Log("CountStars: " + CountStars);
                    }
                    StopCount = true;
                }
            }
        }
    }

    public void CheckPlayerBuy(GameObject player, bool buy)
    {
        PlayerPrefs.SetInt("PlayerBuy_" + player.name, 1);
        PlayerPrefs.Save();
    }

    public void SetPlayerSkin(GameObject player, bool hasSkin)
    {
        if (playerSkins.ContainsKey(player))
        {
            playerSkins[player] = hasSkin;
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

    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }

}

