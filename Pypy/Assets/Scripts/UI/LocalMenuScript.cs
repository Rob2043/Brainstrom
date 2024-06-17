using System.Collections;
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
    [SerializeField] private AudioSource _localAudio;

    private int maxLevel;
    private void Start()
    {
        Time.timeScale = 1f;
        maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        StartCoroutine(AnimationClound());
        if (PlayerPrefs.GetInt("isSoundOn", 1) is 1)
            _localAudio.Play();
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
        _localAudio.Play();
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < SceneManager.sceneCountInBuildSettings - 2)
        {
            if (maxLevel > currentLevel + 1)
            {
                SceneManager.LoadScene(currentLevel + 1);
            }
            else
            {
                PlayerPrefs.SetInt("MaxLevel", currentLevel);
                PlayerPrefs.Save();
                SceneManager.LoadScene(currentLevel + 1);
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void ToMainMenu()
    {
        _localAudio.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPanel()
    {
        _localAudio.Play();
        Time.timeScale = 0;
        if (MainPanel.activeSelf == false)
        {
            ButtonExit.SetActive(false);
            PanelExit.SetActive(true);
        }
    }
    public void NoReturn()
    {
        _localAudio.Play();
        Time.timeScale = 1f;
        PanelExit.SetActive(false);
        ButtonExit.SetActive(true);
    }
    public void Again()
    {
        _localAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}