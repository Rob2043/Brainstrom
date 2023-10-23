using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    private bool isActiveButtonSound;
    [SerializeField] private Button[] LevelButtons;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private Button AudioButton;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelLevel;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    int MaxLevel;
    private void Start()
    {
        MaxLevel = PlayerPrefs.GetInt("MaxLevel",1);
        ButtonInteractible();
        if (PlayerPrefs.GetInt("isSoundOn", 1) == 1)
        {
            isActiveButtonSound = true;
        }
        else
        {
            isActiveButtonSound = false;
        }
    }

    public void MainButtonPlayOnClick()
    {
<<<<<<< HEAD
        panelLevel.SetActive(true);
=======
        //SceneManager.LoadScene("Level 1");
        int currentLevel = PlayerPrefs.GetInt("level", 1);
        SceneManager.LoadScene($"Level {currentLevel}");
>>>>>>> 946b6dc69fe2c38ca0159d71629be41fac8a3d44
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonSettingsOpen()
    {
        panelSettings.SetActive(true);
    }

    public void ButtonSoundOnClick()
    {
        if (isActiveButtonSound)
        {
            AudioButton.image.sprite = ButtonOffSprite;
            for (int i = 1; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
            }
            PlayerPrefs.SetInt("isSoundOn", 0);
            isActiveButtonSound = false;
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            for (int i = 1; i < Audio.Length; i++)
            {
                Audio[i].enabled = false;
            }
            PlayerPrefs.SetInt("isSoundOn", 1);
            isActiveButtonSound = true;
        }
    }

    public void BackToMainMenu()
    {
        panelSettings.SetActive(false);
    }

    public void ScinClic()
    {
        SceneManager.LoadScene("Scins");
    }
    public void LevelButtonOnClick(){
        int sceneSelect = int.Parse(gameObject.name);
        Debug.Log(sceneSelect);
        SceneManager.LoadScene($"level {sceneSelect}");
    }
    private void ButtonInteractible(){
        for(int i = MaxLevel; i < LevelButtons.Length;i++){
            LevelButtons[i].interactable = false;
        }
    }
}
