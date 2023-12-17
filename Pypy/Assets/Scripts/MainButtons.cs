using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
    [SerializeField] private Sprite ButtonOnLevel;
    [SerializeField] private Sprite ButtonOffLevel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    int MaxLevel;
    private bool checkAnimation = false;
    private string sceneSelect;
    private void Start()
    {
        MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
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
    private void Update()
    {
        if (MaxLevel != PlayerPrefs.GetInt("MaxLevel", 1))
        {
            for (int j = 0; j < LevelButtons.Length; j++)
            {
                LevelButtons[j].GetComponent<Button>().enabled = true;
                LevelButtons[j].image.sprite = ButtonOnLevel;
            }
            MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
            ButtonInteractible();
        }
    }

    public void MainButtonPlayOnClick()
    {
        panelLevel.SetActive(true);
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
        for (int i = MaxLevel; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].GetComponent<Button>().enabled = false;
            LevelButtons[i].image.sprite = ButtonOffLevel;
        }
    }
}
