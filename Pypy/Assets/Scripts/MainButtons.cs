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
    private void Update() {
        if(MaxLevel != PlayerPrefs.GetInt("MaxLevel",1)){
            for(int j = 0; j < LevelButtons.Length; j++){
                LevelButtons[j].interactable = true;
            }
            MaxLevel = PlayerPrefs.GetInt("MaxLevel",1);
            ButtonInteractible();
        }
    }

    public void MainButtonPlayOnClick()
    {
        //SceneManager.LoadScene("Level 1");
        panelLevel.SetActive(true);       
        //int currentLevel = PlayerPrefs.GetInt("MaxLevel", 1); <-- Устаревшее, не раскоментировать (Сучастный вариант: строка 20)
        //SceneManager.LoadScene($"Level {MaxLevel}");
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
    public void LevelButtonOnClick(GameObject localeButton){
        string sceneSelect = localeButton.name;
        Debug.Log(sceneSelect);
        SceneManager.LoadScene($"Level {sceneSelect}");
    }
    private void ButtonInteractible(){
        for(int i = MaxLevel; i < LevelButtons.Length;i++){
            LevelButtons[i].interactable = false;
        }
    }
}
