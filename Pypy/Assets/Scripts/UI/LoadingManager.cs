using System.Collections;
using UnityEngine;
using CustomEventBus;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private Slider LoadingSlider;
    private float elapsedTime = 0f;

    private void Awake()
    {
        EventBus.LodingScene = LoadLevel;
        if (mainmenu == null)
        {
            LoadLevel("MainMenu");
            //YG2.GameReadyAPI();
        }
    }


    public void LoadLevel(string levelToLoad)
    {
        if (mainmenu != null)
            mainmenu.SetActive(false);
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(levelToLoad));
    }

    IEnumerator LoadSceneAsync(string levelToLoad)
    {
        float randomDelay = Random.Range(2f, 3f);
        while (elapsedTime < randomDelay)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / randomDelay);
            LoadingSlider.value = progress;
            yield return null;
        }
        EventBus.ActivateClouds?.Invoke();
        yield return new WaitForSeconds(0.85f);
        SceneManager.LoadSceneAsync(levelToLoad);
    }
}
