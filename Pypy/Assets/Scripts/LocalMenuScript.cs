using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private Animator starAnimation;
    [SerializeField] private GameObject PanelExit;


    private void Start()
    {
        starAnimation.SetBool("Star",true);
    }
    public void NextLevel()
    {
        GameManager level = FindObjectOfType<GameManager>();
        int currentLevel = PlayerPrefs.GetInt("level", 1);
        currentLevel++; // Увеличиваем текущий уровень.
        level.SetCurrentLevel(currentLevel); // Сохраняем текущий уровень.
        SceneManager.LoadScene($"Level {currentLevel}"); // Переход на следующий уровень.
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPanel()
    {
        PanelExit.SetActive(true);
    }

    public void YesExit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NoReturn()
    {
        PanelExit.SetActive(false);
    }
}