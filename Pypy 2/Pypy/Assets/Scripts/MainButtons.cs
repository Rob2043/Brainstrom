using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void MainButtonPlayOnClick(){
        int level = PlayerPrefs.GetInt("level",1);
        SceneManager.LoadScene($"Scene{level}");
    }
    public void ButtonQuit(){
        Application.Quit();
    }
}
