using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    private bool isActiveButtonSound;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private Button AudioButton;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    private void Start() {
        if(PlayerPrefs.GetInt("isSoundOn",1) == 1){
            isActiveButtonSound = true;
        } else isActiveButtonSound = false;
    }
    public void MainButtonPlayOnClick(){
        int level = PlayerPrefs.GetInt("level",1);
        SceneManager.LoadScene($"Level {level}");
    }
    public void ButtonQuit(){
        Application.Quit();
    }
    public void ButtonSettingsOpen(){
        panelSettings.SetActive(true);
    }
    public void ButtonSoundOnClick(){
        if(isActiveButtonSound){
            AudioButton.image.sprite = ButtonOffSprite;
            for(int i = 1; i < Audio.Length; i++){
                Audio[i].mute = true;
            }
            PlayerPrefs.SetInt("isSoundOn",0);
            isActiveButtonSound = false;
        } else {
            AudioButton.image.sprite = ButtonOnSprite;
            for(int i = 1; i < Audio.Length; i++){
                Audio[i].mute = false;
            }
            PlayerPrefs.SetInt("isSoundOn",1);
            isActiveButtonSound = true;
        }
    }
    public void BackToMainMenu(){
        panelSettings.SetActive(false);
    }
}
