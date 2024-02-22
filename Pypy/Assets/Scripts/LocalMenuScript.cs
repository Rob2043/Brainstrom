using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject PanelExit;
    [SerializeField] private GameObject PanelClound;
    [SerializeField] private GameObject ButtonExit;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    [SerializeField] private TextStarScript time;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource AudioForButton;
    private AudioSource WonAudio;

    private int maxLevel;

    private void Start()
    {
        MovePlayerScript player = FindAnyObjectByType<MovePlayerScript>();
        WonAudio = player.Audio;
        player.MainAudio = audioSource;
        maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        StartCoroutine(AnimationClound());
        if (PlayerPrefs.HasKey("isSoundOn"))
        {
            if (PlayerPrefs.GetInt("isSoundOn") == 0)
            {
                audioSource.enabled = false;
                AudioForButton.enabled = false;
                WonAudio.enabled = false; 
            }
            else
            {
                audioSource.enabled = true;
                AudioForButton.enabled = true;
                WonAudio.enabled = true;
            }
        }
        else
        {
            audioSource.enabled = true;
            AudioForButton.enabled = true;
        }

    }

    public IEnumerator AnimationClound()
    {
        Cloud1Animator.SetBool("CloseClound", true);
        Cloud2Animator.SetBool("CloseClound2", true);
        yield return new WaitForSeconds(1);
        Cloud1Animator.SetBool("CloseClound", false);
        Cloud2Animator.SetBool("CloseClound2", false);
        yield return new WaitForSeconds(1);
        time.CanTime = true;
        PanelClound.SetActive(false);
    }

    public void NextLevel()
    {
        AudioForButton.Play();
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 2)
        {
            if (maxLevel > currentLevel)
            {
                SceneManager.LoadScene(currentLevel);
            }
            else
            {
                PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex); // Save the current level to PlayerPrefs
                PlayerPrefs.Save(); // Save the PlayerPrefs data
                SceneManager.LoadScene(currentLevel);
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
        Time.timeScale = 1;
        PanelExit.SetActive(false);
        ButtonExit.SetActive(true);
    }
    public void Again()
    {
        AudioForButton.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}