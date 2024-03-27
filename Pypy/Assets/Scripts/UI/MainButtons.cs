﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class MainButtons : MonoBehaviour
{
    [SerializeField] private Button[] LevelButtons;
    [SerializeField] private AudioSource[] Audio;
    [SerializeField] private Button AudioButton;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Sprite ButtonOnSprite;
    [SerializeField] private Sprite ButtonOffSprite;
    [SerializeField] private Sprite ButtonOnLevel;
    [SerializeField] private Sprite ButtonOffLevel;
    [SerializeField] private Animator Cloud1Animator;
    [SerializeField] private Animator Cloud2Animator;
    [SerializeField] private PlayNextGame playNextGame;
    [SerializeField] private GameObject[] PanelLevelArray;

    private int index = 0;
    private int MaxLevel;
    private bool isActiveButtonSound;
    private string sceneSelect;
    private List<LevelButton> levelButtons;

    public delegate void AudioEnable(bool isEnabled);
    public static event AudioEnable CheckButton;
    public delegate int StarsEnable(ref int stars);
    public static event StarsEnable CheckStars;
    private void Start()
    {
        for(int i = 0; i < PanelLevelArray.Length; i++){
            LevelButton[] massive = PanelLevelArray[i].GetComponentsInChildren<LevelButton>();
            for(int j = 0; j < massive.Length; j++){
                levelButtons.Add(massive[j]);
            }
        }
        MaxLevel = PlayerPrefs.GetInt("MaxLevel", 1);
        for (int i = MaxLevel; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].enabled = false;
            LevelButtons[i].image.sprite = ButtonOffLevel;
        }
        Time.timeScale = 1f;
        if(PlayerPrefs.GetInt("isSoundOn", 1) is 1) 
        {
            isActiveButtonSound = true;
        } else
        {
            isActiveButtonSound = false;
        }
        Audio[2].Play();
        for (int i = 0; i < Audio.Length; i++)
        {
            Audio[i].enabled = isActiveButtonSound;
        }
        AudioButton.image.sprite = isActiveButtonSound ? ButtonOnSprite : ButtonOffSprite;
        CheckButton.Invoke(isActiveButtonSound);
    }
    private void LateUpdate()
    {
        if (MaxLevel != PlayerPrefs.GetInt("MaxLevel", 1))
        {
            ButtonInteractible();
        }
    }
    public void NextPanel()
    {
        Audio[2].Play();
        PanelLevelArray[index].SetActive(false);
        PanelLevelArray[index + 1].SetActive(true);
        index++;
    }
    public void PreviousPanel()
    {
        Audio[2].Play();
        PanelLevelArray[index].SetActive(false);
        PanelLevelArray[index - 1].SetActive(true);
        index--;
    }
    public void ToHome()
    {
        Audio[2].Play();
        PanelLevelArray[index].SetActive(false);
        index = 0;
    }
    public void MainButtonPlayOnClick()
    {
        Audio[2].Play();
        PanelLevelArray[0].SetActive(true);
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
        isActiveButtonSound = !isActiveButtonSound;
        Audio[2].Play();
        for (int i = 0; i < Audio.Length; i++)
        {
            Audio[i].enabled = isActiveButtonSound;
        }
        int n = isActiveButtonSound ? 1 : 0;
        PlayerPrefs.SetInt("isSoundOn", n);
        PlayerPrefs.Save();
        AudioButton.image.sprite = isActiveButtonSound ? ButtonOnSprite : ButtonOffSprite;
        CheckButton.Invoke(isActiveButtonSound);
    }

    public void BackToMainMenu()
    {
        Audio[2].Play();
        panelSettings.SetActive(false);
    }

    public void LoadScinScene()
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
        playNextGame.sceneSelect = sceneSelect;
        Cloud1Animator.SetBool("IsActiveCloud", true);
        Cloud2Animator.SetBool("IsCloundActive2", true);
        yield return new WaitForSeconds(1);
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
    private void StartCheck(){
        int stars = 1;
        int scene = CheckStars.Invoke(ref stars);
        Debug.Log(scene);
        levelButtons[scene].ActiveStars(stars);
        SavePlayerPrefsData(scene, stars);
    }
    private void OnEnable() {
        MovePlayerScript.CheckEnd += StartCheck;
    }
    private void OnDisable() {
        MovePlayerScript.CheckEnd -= StartCheck;
    }
    private void SavePlayerPrefsData(int scene, int amountStar){
        for(int i = 0; i < amountStar; i++){
            PlayerPrefs.SetInt($"{levelButtons[scene].transform.name}_{levelButtons[scene].stars[i].name}", 1);
        }
    }
}
