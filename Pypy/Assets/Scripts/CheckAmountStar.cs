using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckAmountStar : MonoBehaviour
{
    public GameObject[] stars = new GameObject[3];

    private void Start()
    {
        CheckStarsData();
    }
    public void SaveStartData(bool isOneStar, bool isTwoStar, bool isThreeStar)
    {
        if (isOneStar)
        {
            PlayerPrefs.SetInt($"OneStar{SceneManager.GetActiveScene()}", 1);
        }
        if (isTwoStar)
        {
            PlayerPrefs.SetInt($"TwoStar{SceneManager.GetActiveScene()}", 1);
        }
        if (isThreeStar)
        {
            PlayerPrefs.SetInt($"ThreeStar{SceneManager.GetActiveScene()}", 1);
        }
        PlayerPrefs.Save();
    }
    public void CheckStarsData()
    {
        if (PlayerPrefs.GetInt($"OneStar{SceneManager.GetActiveScene()}", 0) == 1)
        {
            stars[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt($"TwoStar{SceneManager.GetActiveScene()}", 0) == 1)
        {
            stars[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt($"ThreeStar{SceneManager.GetActiveScene()}", 0) == 1)
        {
            stars[2].SetActive(true);
        }
    }
}
