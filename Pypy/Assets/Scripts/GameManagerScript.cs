using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] public GameObject[] ArrayPlayers;
    public GameObject selectedPlayer;
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
        //playerSkins.Clear();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var player in ArrayPlayers)
        {
            if (PlayerPrefs.HasKey(player.name))
            {
                bool FirstValue = PlayerPrefs.GetString(player.name) == "True";
                bool Secondvalue = PlayerPrefs.GetInt(player.name) == 0;
                bool accurateValue = FirstValue == Secondvalue;
                Debug.Log(player.name +  accurateValue);
                Debug.Log("Second: " + player.name + Secondvalue);
                if (accurateValue)
                {
                    if (!playerSkins.ContainsKey(player))
                    {                        
                        playerSkins.Add(player, accurateValue);
                    }
                }
                if (playerSkins.ContainsKey(player))
                {
                    Debug.Log(player.name + "Has Key");
                    playerSkins[player] = accurateValue;
                }
                else
                {
                    Debug.Log(player.name + "Not Has Key");
                    playerSkins.Add(player, accurateValue);
                }
            }
        }
        if (scene.name == "Scins")
        {
            //playerSkins.Clear();
            foreach (var player in ArrayPlayers)
            {
                if(playerSkins.ContainsKey(player) && playerSkins[player] == true)
                {
                    GameObject Scin = GameObject.Find(player.name);
                    Scin.GetComponent<MovePlayerScript>().checkScin = playerSkins[player];
                    Debug.Log("Test if");
                }
                
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
        }
        else if (scene.name != "MainMenu")
        {
            bool check = true;
            EmptyPlayer = GameObject.FindGameObjectWithTag("SpawnEmpty");
            ThirdStar = GameObject.FindGameObjectWithTag("ThirdStar");
            ThirdStar.SetActive(false);
            foreach (var player in ArrayPlayers)
            {
                if (playerSkins.ContainsKey(player) && playerSkins.ContainsValue(player) == true)
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
        List<GameObject> keysToUpdate = new List<GameObject>();

        if (playerSkins.ContainsKey(player))
        {
            playerSkins[player] = hasSkin;
        }
        else
        {
            playerSkins.Add(player, hasSkin);
        }

        // Создаем список ключей для обхода словаря
        foreach (var objectInDict in playerSkins)
        {
            if (objectInDict.Value == true && objectInDict.Key.name != player.name)
            {
                keysToUpdate.Add(objectInDict.Key);
            }
        }

        // Обновляем значения после завершения итерации
        foreach (var key in keysToUpdate)
        {
            playerSkins[key] = false;
            Debug.Log(key.name + " false");
            PlayerPrefs.SetInt(player.name, 1);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetString(player.name, hasSkin.ToString());
        PlayerPrefs.Save();
    }

    //public bool GetPlayerSkin(GameObject player)
    //{
    //    if (playerSkins.ContainsKey(player))
    //    {
    //        return playerSkins[player];
    //    }
    //    return false;
    //}

    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
}

