using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAmountStar : MonoBehaviour
{
    private readonly string scene = SceneManager.GetActiveScene().name;
    public void SaveStartData(bool isOneStar, bool isTwoStar, bool isThreeStar)
    {
        if (isOneStar)
        {
            PlayerPrefs.SetInt($"OneStar{scene}", 1);
        }
        if (isTwoStar)
        {
            PlayerPrefs.SetInt($"TwoStar{scene}", 1);
        }
        if (isThreeStar)
        {
            PlayerPrefs.SetInt($"ThreeStar{scene}", 1);
        }
        PlayerPrefs.Save();
    }
    public void CheckStarsData(GameObject[] stars, int indexLevel)
    {
        if (stars != null)
        {
            Debug.Log(stars[0].name);
            if (PlayerPrefs.GetInt($"OneStarLevel " + indexLevel, 0) == 1)
            {
                stars[0].SetActive(true);
            }
            if (PlayerPrefs.GetInt($"TwoStarLevel " + indexLevel, 0) == 1)
            {
                stars[1].SetActive(true);
            }
            if (PlayerPrefs.GetInt($"ThreeStarLevel " + indexLevel, 0) == 1)
            {
                stars[2].SetActive(true);
            }
        }
    }
}
