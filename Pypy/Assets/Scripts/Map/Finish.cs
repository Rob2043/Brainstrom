using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Pypy.Consts;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource _winAudio;
    [SerializeField] private AudioSource _gamePlayMusic;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _countStars;
    private int earnstars = 0;
    private int alredyEarnedStars = 0;
    private string NameOfScene;
    private int localScene;
    private void Awake()
    {
        localScene = SceneManager.GetActiveScene().buildIndex - 3;
        NameOfScene = $"{localScene}";
        if (PlayerPrefs.GetInt($"{NameOfScene}_Stars {1}", 0) == 0)
            earnstars++;
        for(int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.GetInt($"{NameOfScene}_Stars {i}", 0) == 1)
                alredyEarnedStars++;
        }
        EventBus.AddStarsInPlay = AddStar;
    }
    private void AddStar() => earnstars++;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringConstants.PLAYER_TAG))
        {
            EventBus.CallOffMoving.Invoke();
            earnstars += EventBus.CheckStars.Invoke();
            int count;
            if (alredyEarnedStars >= earnstars)
                count = alredyEarnedStars;
            else
                count = earnstars;
            EventBus.ShowAllStars.Invoke(count);
            EventBus.AddStars.Invoke(earnstars);
            EventBus.OnWinFireworks.Invoke();
            
            _countStars.text = $"{EventBus.GetStars.Invoke()}";
            MusicTransaction(_gamePlayMusic, _winAudio);
            _winAudio.Play();
            _gamePlayMusic.enabled = false;
            _endPanel.SetActive(true);
            
            for (int i = 1; i <= earnstars; i++)
            {
                PlayerPrefs.SetInt($"{NameOfScene}_Stars {i}", 1);
            }
            if (PlayerPrefs.GetInt("MaxLevel") <= localScene++)
                PlayerPrefs.SetInt("MaxLevel", localScene++);
            PlayerPrefs.Save();
            EventBus.Save.Invoke();
            //float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
            //float EndLevelValue = Level - (int)Level;
            //if (EndLevelValue == 0)
            //{
            //    InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
            //    Ads.ShowAd();
            //}
        }
    }
    IEnumerator MusicTransaction(AudioSource satartAudio, AudioSource endAudio)
    {
        float time = 0.1f;
        while (satartAudio.volume > 0)
        {
            yield return new WaitForSeconds(time);
            satartAudio.volume -= time;
            endAudio.volume += time;
            yield return null;
        }
    }
}
