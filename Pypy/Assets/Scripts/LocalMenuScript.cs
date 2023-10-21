using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMenuScript : MonoBehaviour
{
    [SerializeField] private Animator starAnimation;
    [SerializeField] private GameObject Panel;

    private void Start()
    {
        starAnimation.SetBool("Star",true);
    }
    public void NextLevel()
    {
        int Scene = SceneManager.GetActiveScene().buildIndex + 1;
        if (Scene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(Scene);
        }
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPanel()
    {
        Panel.SetActive(true);
    }

    public void YesExit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NoReturn()
    {
        Panel.SetActive(false);
    }
}