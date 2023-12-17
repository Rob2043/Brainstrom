using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject PanelExit;
    [SerializeField] private GameObject PanelClound;
    [SerializeField] private GameObject ButtonExit;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    [SerializeField] private TextStarScript time;

    private void Start()
    {
        StartCoroutine(AnimationClound());
    }

    public IEnumerator AnimationClound()
    {
        Debug.Log("Test Local");
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
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            // Если это последняя сцена в сборке, то можно загрузить меню или что-то другое.
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPanel()
    {
        if (MainPanel.active == false)
        {
            ButtonExit.SetActive(false);
            PanelExit.SetActive(true);
        }
    }

    public void NoReturn()
    {
        PanelExit.SetActive(false);
        ButtonExit.SetActive(true);
    }
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}