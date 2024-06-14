using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource _winAudio;
    [SerializeField] private AudioSource _gamePlayMusic;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Text _countStars;
    private int stars = 1;
    private void Awake()
    {
        EventBus.AddStarsInPlay = AddStar;
    }
    private void AddStar() => stars++;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            stars += EventBus.CheckStars.Invoke();
            EventBus.AddStars.Invoke(stars);
            _countStars.text = $"{EventBus.GetStars.Invoke()}";
            _winAudio.Play();
            _gamePlayMusic.enabled = false;
            _endPanel.SetActive(true);
            int localScene = SceneManager.GetActiveScene().buildIndex - 2;
            for (int i = 1; i <= stars; i++)
                PlayerPrefs.SetInt($"{localScene}_Stars {i}", 1);
            if (PlayerPrefs.GetInt("MaxLevel") <= localScene++)
                PlayerPrefs.SetInt("MaxLevel", localScene++);
            PlayerPrefs.Save();
            //float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
            //float EndLevelValue = Level - (int)Level;
            //if (EndLevelValue == 0)
            //{
            //    InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
            //    Ads.ShowAd();
            //}
        }
    }
}
