using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // private void OnEnable()
    // {
    //     EventBus.CheckStars += OnStar;
    // }
    // private void OnDisable()
    // {
    //     EventBus.CheckStars -= OnStar;
    // }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Finish"))
    //     {
    //         EventBus.CheckEnd.Invoke();
    //         Time.timeScale = 0f;
    //         Audio.Play();
    //         MainAudio.enabled = false;
    //         endPanel.SetActive(true);
    //         float localScene = SceneManager.GetActiveScene().buildIndex;
    //         if (PlayerPrefs.GetInt("MaxLevel") <= localScene + 1)
    //         {
    //             PlayerPrefs.SetInt("MaxLevel", (int)(localScene + 1));

    //             PlayerPrefs.Save();
    //         }
    //         //float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
    //         //float EndLevelValue = Level - (int)Level;
    //         //if (EndLevelValue == 0)
    //         //{
    //         //    InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
    //         //    Ads.ShowAd();
    //         //}
    //     }
    //     else if (other.CompareTag("star"))
    //     {
    //         star = 1;
    //     }
    // }

    // private (int, int) OnStar(int stars)
    // {
    //     return (SceneManager.GetActiveScene().buildIndex + 1, stars += star);
    // }
}
