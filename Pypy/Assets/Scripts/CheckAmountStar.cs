using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckAmountStar : MonoBehaviour
{
    public void SaveStartData(bool isOneStar, bool isTwoStar, bool isThreeStar)
    {
        if (isOneStar)
        {
            PlayerPrefs.SetInt($"OneStar{SceneManager.GetActiveScene().name}", 1);
        }
        if (isTwoStar)
        {
            PlayerPrefs.SetInt($"TwoStar{SceneManager.GetActiveScene().name}", 1);
        }
        if (isThreeStar)
        {
            PlayerPrefs.SetInt($"ThreeStar{SceneManager.GetActiveScene().name}", 1);
        }
        PlayerPrefs.Save();
    }
    public void CheckStarsData(GameObject[] stars, int indexLevel)
    {
        if (PlayerPrefs.GetInt($"OneStarLevel " + indexLevel, 0) == 1)
        {
            stars[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt($"TwoStarLevel " + indexLevel, 0) == 1)
        {
            stars[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt($"ThreeStarLevel" + indexLevel, 0) == 1)
        {
            stars[2].SetActive(true);
        }
    }
}
