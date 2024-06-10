using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public float stars;
    public GameObject SelectScin {get; set;}
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
    private void Start() {
        stars = PlayerPrefs.GetFloat("CountStars",0);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){

    }
    public void SaveData(){
        PlayerPrefs.SetFloat("CountStars", stars);
        PlayerPrefs.Save();
    } 
    private void OnApplicationQuit() {
        SaveData();
    }
}
