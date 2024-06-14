using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public int stars;
    public GameObject SelectScin { get; set; }
    private void Awake()
    {
        EventBus.AddStars = AddStars;
        EventBus.GetStars = GetStars;
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
    }
    private void Start()
    {
        stars = PlayerPrefs.GetInt("CountStars", 0);
    }
    private void AddStars(int CountStars) => stars += CountStars;
    private int GetStars()
    {
        return stars;
    } 
    public void SaveData()
    {
        PlayerPrefs.SetInt("CountStars", stars);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
