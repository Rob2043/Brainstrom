using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MainButtons : MonoBehaviour
{

    [SerializeField] private Button[] LevelButtons;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private Button AudioButton;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelLevel;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    [SerializeField] private Sprite ButtonOnLevel;
    [SerializeField] private Sprite ButtonOffLevel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    [SerializeField] private GameObject[] PanelLevelArray;
    int MaxLevel;
    private bool checkAnimation = false;
    private string sceneSelect;
    public CheckAmountStar checkAmountStar;
    private bool isActiveButtonSound;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("isSoundOn") == 0)
        {
            AudioButton.image.sprite = ButtonOffSprite;
            isActiveButtonSound = false;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = false;
            }
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            float SaveValueSlider = PlayerPrefs.GetFloat("SliderVolume");
            isActiveButtonSound = true;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
                Audio[i].volume = SaveValueSlider;
            }
        }
    }
    private void LateUpdate()
    {
        if (MaxLevel != PlayerPrefs.GetInt("MaxLevel", 1))
        {
            for (int j = 0; j < LevelButtons.Length; j++)
            {
                LevelButtons[j].enabled = true;
                LevelButtons[j].image.sprite = ButtonOnLevel;
            }
            MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
            ButtonInteractible();
        }
    }
    public void OnAnotherMapLevels()
    {
        bool checkCountPanel = true;
        for (int i = 0; i < PanelLevelArray.Length; i++)
        {
            if (i >= 0 && i <= PanelLevelArray.Length)
            {
                if (PanelLevelArray[i].activeSelf && checkCountPanel)
                {
                    if (i != 4)
                    {
                        PanelLevelArray[i].SetActive(false);
                        PanelLevelArray[i + 1].SetActive(true);
                        checkCountPanel = false;
                    }
                    else
                    {
                        PanelLevelArray[i].SetActive(false);
                        PanelLevelArray[i - 1].SetActive(true);
                        checkCountPanel = false;
                    }
                }
            }

        }
    }

    public void ToHome()
    {
        for (int i = 0; i < PanelLevelArray.Length; i++)
        {
            PanelLevelArray[i].SetActive(false);
        }
    }

    public void BackLevelPanel()
    {
        bool checkCountPanel = true;
        for (int i = 1; i <= PanelLevelArray.Length; i++)
        {
            if (PanelLevelArray[i].activeSelf && checkCountPanel)
            {
                if (i != 1)
                {
                    PanelLevelArray[i].SetActive(false);
                    PanelLevelArray[i - 1].SetActive(true);
                    checkCountPanel = false;
                }
            }
        }
    }

    public void MainButtonPlayOnClick()
    {
        panelLevel.SetActive(true);
        GameObject[] stars = new GameObject[3];

        for (int j = 0; j < LevelButtons.Length; j++)
        {
            Debug.Log("Level");
            // Очистка массива перед использованием
            Array.Clear(stars, 0, stars.Length);

            for (int i = 1; i <= 3; i++)
            {
                Transform transitionTransform = LevelButtons[j].gameObject.transform.Find($"Stars " + i);
                if (transitionTransform != null)
                {
                    GameObject transitiohnObject = transitionTransform.gameObject;
                    stars[i - 1] = transitiohnObject;
                    Debug.Log(transitiohnObject.name);
                    transitiohnObject.gameObject.SetActive(false);
                }
            }

            checkAmountStar.CheckStarsData(stars, j + 1);
        }
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
            AudioButton.image.sprite = ButtonOnSprite;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
            }
            PlayerPrefs.SetInt("isSoundOn", 1);
            isActiveButtonSound = false;
        }
        else
        {
            AudioButton.image.sprite = ButtonOffSprite;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = false;
            }
            PlayerPrefs.SetInt("isSoundOn", 0);
            isActiveButtonSound = true;
        }
        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        panelSettings.SetActive(false);
    }

    public void ScinClic()
    {
        SceneManager.LoadScene("Scins");
    }
    public void LevelButtonOnClick(GameObject localeButton)
    {
        sceneSelect = localeButton.name;
        StartCoroutine(Animation());
    }

    public IEnumerator Animation()
    {
        Debug.Log("Test Active");
        Cloud1Animator.SetBool("IsActiveCloud", true);
        Cloud2Animator.SetBool("IsCloundActive2", true);
        yield return new WaitForSeconds(1);
        checkAnimation = true;
        Debug.Log(checkAnimation);
        Debug.Log(sceneSelect);
        SceneManager.LoadScene($"Level {sceneSelect}");
    }

    private void ButtonInteractible()
    {
        for (int i = 60; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].enabled = false;
            LevelButtons[i].image.sprite = ButtonOffLevel;
        }
    }
}
