using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] GameObject[] ArrayPlayers;
    private GameObject selectedPlayer;
    private GameObject selectedSecondPlayer;
    [SerializeField] private GameObject EmptyPlayer;

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

        // Подписываем метод на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписываем метод при уничтожении объекта
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string SkinName = "";
        if (scene.name == "Scins")
        {
            if (PlayerPrefs.HasKey("SaveScin"))
            {
                SkinName = PlayerPrefs.GetString("SaveScin");
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
        if (SceneManager.GetActiveScene().name != "Scins" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            EmptyPlayer = GameObject.FindGameObjectWithTag("SpawnEmpty");
            foreach (var player in ArrayPlayers)
            {
                if (player.name == PlayerPrefs.GetString("SaveScin"))
                {
                    GameObject newScin = Instantiate(player, EmptyPlayer.transform.position, EmptyPlayer.transform.rotation);
                    newScin.tag = player.tag;
                    Destroy(EmptyPlayer);
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

    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }

}
