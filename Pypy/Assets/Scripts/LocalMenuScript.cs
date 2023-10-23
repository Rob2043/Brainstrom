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
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("level", currentLevel); // Save the current level to PlayerPrefs
        PlayerPrefs.Save(); // Save the PlayerPrefs data
        SceneManager.LoadScene($"Level {currentLevel}"); // Load the next level
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