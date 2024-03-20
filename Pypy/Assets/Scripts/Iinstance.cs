using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iinstance : MonoBehaviour
{
    public static Iinstance instance;
    public float stars;
    public GameObject SelectScin;
    public Dictionary<string, int> DataScins = new Dictionary<string, int>();
    public Dictionary<string, GameObject> SpawnScin = new Dictionary<string, GameObject>();

    private void Awake()
    {
        stars = PlayerPrefs.GetFloat("CountStars", 0);
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
        Load();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("SelectScin"))
            SelectScin = SpawnScin[(PlayerPrefs.GetString("SelectScin"))];
        foreach (string Scin in DataScins.Keys)
        {
            if (PlayerPrefs.HasKey(Scin))
                DataScins.Add(Scin,PlayerPrefs.GetInt(Scin));
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("SelectScin", SelectScin.name);
        PlayerPrefs.SetFloat("CountStars", stars);
        foreach (string Scin in DataScins.Keys)
        {
            PlayerPrefs.SetInt(Scin, DataScins[Scin]);
        }
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
