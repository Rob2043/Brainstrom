using System.Collections;
using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private GameObject PanelExit;
    [SerializeField] private GameObject PanelClound;
    [SerializeField] private GameObject ButtonExit;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    [SerializeField] private AudioSource AudioForButton;

    private int maxLevel;

    private void Start()
    {
        maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        StartCoroutine(AnimationClound());
    }
    private void OnEnable()
    {
        EventBus.CheckButton += AudioEnable;
    }
    private void OnDisable()
    {
        EventBus.CheckButton -= AudioEnable;
    }
    public IEnumerator AnimationClound()
    {
        Cloud1Animator.SetBool("CloseClound", true);
        Cloud2Animator.SetBool("CloseClound2", true);
        yield return new WaitForSeconds(1);
        Cloud1Animator.SetBool("CloseClound", false);
        Cloud2Animator.SetBool("CloseClound2", false);
        yield return new WaitForSeconds(1);
        PanelClound.SetActive(false);
    }

    public void NextLevel()
    {
        AudioForButton.Play();
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 2)
        {
            if (maxLevel > currentLevel + 1)
            {
                SceneManager.LoadScene(currentLevel + 1);
            }
            else
            {
                PlayerPrefs.SetInt("MaxLevel", currentLevel); // Save the current level to PlayerPrefs
                PlayerPrefs.Save(); // Save the PlayerPrefs data
                SceneManager.LoadScene(currentLevel + 1);
            }
        }
        else
        {
            // Если это последняя сцена в сборке, то можно загрузить меню или что-то другое.
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void ToMainMenu()
    {
        AudioForButton.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPanel()
    {
        AudioForButton.Play();
        Time.timeScale = 0;
        if (MainPanel.activeSelf == false)
        {
            ButtonExit.SetActive(false);
            PanelExit.SetActive(true);
        }
    }

    public void NoReturn()
    {
        AudioForButton.Play();
        Time.timeScale = 1f;
        PanelExit.SetActive(false);
        ButtonExit.SetActive(true);
    }
    public void Again()
    {
        AudioForButton.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void AudioEnable(bool isActiveButtonSound)
    {
        for (int i = 0; i < Audio.Length; i++)
        {
            Audio[i].enabled = isActiveButtonSound;
        }
        int n = isActiveButtonSound ? 1 : 0;
        PlayerPrefs.SetInt("isSoundOn", n);
        PlayerPrefs.Save();
    }
}