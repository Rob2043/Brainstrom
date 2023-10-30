using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalMunu : MonoBehaviour
{
    public void MenuMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextBuildIndex);
        }
        else
        {
            // Если это последняя сцена в сборке, то можно загрузить меню или что-то другое.
            SceneManager.LoadScene("MainMenu");
        }
    }
}
