﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] GameObject[] ArrayPlayers;
    private GameObject selectedPlayer;
    private GameObject selectedSecondPlayer;
    private FindModel BasicPlayer;

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
        if (SceneManager.GetActiveScene().name != "Scins" & SceneManager.GetActiveScene().name != "MainMenu")
        {
            ArrayPlayers = GameObject.FindGameObjectsWithTag("Player");
            int sum = 0;
            foreach (var player in ArrayPlayers)
            {
                if (player.name != PlayerPrefs.GetString("SaveScin"))
                {
                    sum++;
                    player.SetActive(false);
                }
            }
            if(PlayerPrefs.GetString("SaveScin") == null)
            {
                Debug.Log("BasicModel");
                BasicPlayer = FindObjectOfType<FindModel>();
                BasicPlayer.gameObject.SetActive(true);
            }
            else if(sum == ArrayPlayers.Length - 1)
            {
                BasicPlayer = FindObjectOfType<FindModel>();
                BasicPlayer.gameObject.SetActive(true);
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

    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
}