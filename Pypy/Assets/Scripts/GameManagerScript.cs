using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject[] ArrayPlayers;
    private GameObject selectedPlayer;
    private GameObject selectedSecondPlayer;
    [SerializeField] private GameObject EmptyPlayer;
    public int CountStars;
    public GameObject TextCountStars;
    [SerializeField] private GameObject BasicPlayer;
    public bool StopCount = false;

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
        try
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
                            TextMeshPro textMeshProComponent = TextCountStars.GetComponent<TextMeshPro>();
                            if (textMeshProComponent != null)
                            {
                                CountStars++;
                                textMeshProComponent.text = CountStars.ToString();
                                // Add a debug statement to print the updated CountStars
                                Debug.Log("CountStars: " + CountStars);
                            }
                        }
                        StopCount = true;
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Catch and log any exceptions that occur
            Debug.LogError("Exception caught: " + e.Message);

            // Add debug statements to print information for debugging
            Debug.Log("PlayerForStar: " );
            Debug.Log("movePlayerScript: ");
            Debug.Log("TextCountStars: " + TextCountStars);
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
