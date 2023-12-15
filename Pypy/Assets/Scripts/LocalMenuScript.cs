using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject PanelExit;
    [SerializeField] private GameObject ButtonExit;

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
        ButtonExit.SetActive(false);
        PanelExit.SetActive(true);
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