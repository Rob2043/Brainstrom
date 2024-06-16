using CustomEventBus;
using UnityEngine;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public int stars;
    [SerializeField] public DataOfPlayer[] _dataOfPlayer;
    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        EventBus.AddStars = AddStars;
        EventBus.GetStars = GetStars;
        EventBus.Save = SaveData;
        EventBus.GetSave = GetData;
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
    private void AddStars(int CountStars) => stars += CountStars;
    private int GetStars()
    {
        return stars;
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("CountStars", stars);
        for (int i = 0; i < _dataOfPlayer.Length; i++)
        {
            PlayerPrefs.SetString($"{_dataOfPlayer[i].NameOfScins} WasChousing", $"{_dataOfPlayer[i].WasChousing}");
            PlayerPrefs.SetString($"{_dataOfPlayer[i].NameOfScins} WasBuying", $"{_dataOfPlayer[i].WasBuying}");
        }
        PlayerPrefs.Save();
    }
    private void GetData()
    {
        stars = PlayerPrefs.GetInt("CountStars", 0);
        for (int i = 0; i < _dataOfPlayer.Length; i++)
        {
            if (PlayerPrefs.GetString($"{_dataOfPlayer[i].NameOfScins} WasChousing", $"{_dataOfPlayer[i].WasChousing}") == "False")
                _dataOfPlayer[i].WasChousing = false;
            else _dataOfPlayer[i].WasChousing = true;
            if (PlayerPrefs.GetString($"{_dataOfPlayer[i].NameOfScins} WasBuying", $"{_dataOfPlayer[i].WasBuying}") == "False")
                _dataOfPlayer[i].WasBuying = false;
            else _dataOfPlayer[i].WasBuying = true;
        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
