using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEditor;

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
    [SerializeField] private CheckAmountStar checkAmountStar;

    private int MaxLevel;
    private bool checkAnimation = false;
    private bool isActiveButtonSound;
    private string sceneSelect;
    private void Start()
    {
        MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        for (int i = MaxLevel; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].enabled = false;
            LevelButtons[i].image.sprite = ButtonOffLevel;
        }
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("isSoundOn"))
        {
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
                isActiveButtonSound = true;
                for (int i = 0; i < Audio.Length; i++)
                {
                    Audio[i].enabled = true;
                }
            }
        }
        else
        {
            AudioButton.image.sprite = ButtonOnSprite;
            isActiveButtonSound = true;
            for (int i = 0; i < Audio.Length; i++)
            {
                Audio[i].enabled = true;
            }
        }

    }
    private void LateUpdate()
    {
        if (MaxLevel != PlayerPrefs.GetInt("MaxLevel", 1))
        {
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
        Audio[2].Play();
        for (int i = 0; i < PanelLevelArray.Length; i++)
        {
            PanelLevelArray[i].SetActive(false);
        }
    }

    public void BackLevelPanel()
    {
        Audio[2].Play();
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
        Audio[2].Play();
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
                    stars[i - 1] = transitionTransform.gameObject;
                    Debug.Log(transitionTransform.gameObject.name);
                    transitionTransform.gameObject.SetActive(false);
                }
            }

            checkAmountStar.CheckStarsData(stars, j + 1);
        }
    }

    public void ButtonQuit()
    {
        Audio[2].Play();
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ButtonSettingsOpen()
    {
        Audio[2].Play();
        panelSettings.SetActive(true);
    }

    public void ButtonSoundOnClick()
    {
        Audio[2].Play();
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
        Audio[2].Play();
        panelSettings.SetActive(false);
    }

    public void ScinClic()
    {
        Audio[2].Play();
        SceneManager.LoadScene("Scins");
    }
    public void LevelButtonOnClick(GameObject localeButton)
    {
        Audio[2].Play();
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
        MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        for (int j = 0; j < LevelButtons.Length; j++)
        {
            LevelButtons[j].enabled = true;
            LevelButtons[j].image.sprite = ButtonOnLevel;
        }
    }
}
