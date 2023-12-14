using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Dictionary<GameObject, bool> playerSkins = new Dictionary<GameObject, bool>();
    [SerializeField] private GameObject EmptyPlayer;
    [SerializeField] private GameObject BasicPlayer;
    public GameObject TextCountStars;
    public bool StopCount = false;
    public static bool FirstAddToPlayerBuy = true;
    public int CountStars;
    public GameObject[] ArrayPlayers;
    public GameObject selectedPlayer;
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

                if (PlayerPrefs.HasKey("SaveFalse" + player.name))
                {
                    bool Secondvalue = PlayerPrefs.GetString("SaveFalse" + player.name) != "False";
                    bool accurateValue = FirstValue == Secondvalue;
                    //Debug.Log("First" + player.name + FirstValue);
                    //Debug.Log("Second" + player.name + Secondvalue);
                    Debug.Log(player.name + accurateValue);
                    if (accurateValue)
                    {
                        if (!playerSkins.ContainsKey(player))
                        {
                            playerSkins.Add(player, accurateValue);
                        }
                    }
                    if (playerSkins.ContainsKey(player))
                    {
                        Debug.Log(player.name + "Has Key: " + accurateValue);
                        playerSkins[player] = accurateValue;
                    }
                    else
                    {
                        Debug.Log(player.name + "Not Has Key");
                        playerSkins.Add(player, accurateValue);
                    }
                }
                else
                {
                    bool accurateValue = FirstValue;
                    Debug.Log(player.name + accurateValue);
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
        }
        if (scene.name == "Scins")
        {
            //playerSkins.Clear();
            foreach (var SecondPlayer in ArrayPlayers)
            {
                if (playerSkins.ContainsKey(SecondPlayer) && playerSkins[SecondPlayer] == true)
                {
                    GameObject Scin = GameObject.Find(SecondPlayer.name);
                    Scin.GetComponent<MovePlayerScript>().checkScin = playerSkins[SecondPlayer];
                }

                //Debug.Log(player.name + playerBuy.ContainsKey(player));
                if (PlayerPrefs.HasKey("PlayerBuy_" + SecondPlayer.name))
                {
                    bool transition = PlayerPrefs.GetInt("PlayerBuy_" + SecondPlayer.name, 1) == 1;
                    GameObject transitionPlayer = GameObject.Find(SecondPlayer.name);
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
            foreach (var ThirdPlayer in ArrayPlayers)
            {
                if (playerSkins.ContainsKey(ThirdPlayer) && playerSkins[ThirdPlayer] == true)
                {
                    check = false;
                    GameObject newScin = Instantiate(ThirdPlayer, EmptyPlayer.transform.position, EmptyPlayer.transform.rotation);
                    newScin.tag = ThirdPlayer.tag;
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
        if (player == null)
        {
            Debug.LogError("Player object is null.");
            return;
        }

        List<KeyValuePair<GameObject, bool>> keysToUpdate = new List<KeyValuePair<GameObject, bool>>();

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
            if (objectInDict.Value == true && objectInDict.Key != null)
            {
                keysToUpdate.Add(objectInDict);
            }
        }

        // Обновляем значения после завершения итерации
        foreach (var keyValuePair in keysToUpdate)
        {
            bool transition = false;

            if (PlayerPrefs.HasKey("SaveFalse" + keyValuePair.Key.name))
            {
                if (player != null && keyValuePair.Key.name == player.name)
                {
                    Debug.Log("Change bool" + keyValuePair.Key.name);
                    playerSkins[keyValuePair.Key] = true;
                    PlayerPrefs.SetString("SaveFalse" + keyValuePair.Key.name, "True");
                }
                else
                {
                    playerSkins[keyValuePair.Key] = transition;
                    GameObject Scin = GameObject.Find(keyValuePair.Key.name);
                    Scin.GetComponent<MovePlayerScript>().checkScin = transition;
                    Debug.Log(keyValuePair.Key.name + " False");
                    PlayerPrefs.SetString("SaveFalse" + keyValuePair.Key.name, transition.ToString());
                }
            }
            else
            {
                playerSkins[keyValuePair.Key] = transition;
                GameObject Scin = GameObject.Find(keyValuePair.Key.name);
                Scin.GetComponent<MovePlayerScript>().checkScin = transition;
                Debug.Log(keyValuePair.Key.name + " False");
                PlayerPrefs.SetString("SaveFalse" + keyValuePair.Key.name, transition.ToString());
            }
        }

        PlayerPrefs.SetString(player.name, hasSkin.ToString());
        PlayerPrefs.Save();
    }




    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
 class CheckAmountStar
 {
    public GameObject[] stars = new GameObject[3];
    public void SaveStartData(bool isOneStar,bool isTwoStar, bool isThreeStar){
        if(isOneStar){
            PlayerPrefs.SetInt($"OneStar{SceneManager.GetActiveScene()}",1);
        }
        if(isTwoStar){
            PlayerPrefs.SetInt($"TwoStar{SceneManager.GetActiveScene()}",1);
        }
        if(isThreeStar){
            PlayerPrefs.SetInt($"ThreeStar{SceneManager.GetActiveScene()}",1);
        }
        PlayerPrefs.Save();
    }
    public void CheckStarsData(){
        if(PlayerPrefs.GetInt($"OneStar{SceneManager.GetActiveScene()}",0) == 1){
            stars[0].SetActive(true);
        }
        if(PlayerPrefs.GetInt($"TwoStar{SceneManager.GetActiveScene()}",0) == 1){
            stars[1].SetActive(true);
        }
        if(PlayerPrefs.GetInt($"ThreeStar{SceneManager.GetActiveScene()}",0) == 1){
            stars[2].SetActive(true);
        }
    }
 }
}

