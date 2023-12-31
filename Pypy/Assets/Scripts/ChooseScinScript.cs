﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChooseScinScript : MonoBehaviour
{
    [SerializeField] private MovePlayerScript CheckBuy;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private Text TextButtonBuy;
    [SerializeField] private GameObject ButtonChoose;
    [SerializeField] private Text TextForPrice;
    [SerializeField] private Text TextForCountStars;
    [SerializeField] private Text TextName;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CheckScin checkScin;
    [SerializeField] private AudioSource audioSource;
    [SerializeField]  AudioSource AudioForButton;
    private GameObject playerCheck;

    private bool isRotating = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (PlayerPrefs.HasKey("isSoundOn"))
        {
            if (PlayerPrefs.GetInt("isSoundOn") == 0)
            {
                audioSource.enabled = false;
                AudioForButton.enabled = false;
            }
            else
            {
                audioSource.enabled = true;
                AudioForButton.enabled = true;
            }
        }
        else
        {
            audioSource.enabled = true;
            AudioForButton.enabled = true;
        }
    }

    private void LateUpdate()
    {
        playerCheck = checkScin.ScanObjct;
        CheckBuy = playerCheck.GetComponent<MovePlayerScript>();
        TextForCountStars.text = $"{gameManager.CountStars}";
        if (CheckBuy != null)
        {
            TextName.text = $"{CheckBuy.dataScins.Name}";
            if (CheckBuy.checkScin == true)
            {
                TextButtonBuy.text = "Selected";
            }
            else
            {
                TextButtonBuy.text = "Choose";
            }
            if (CheckBuy.allowForBuy == false)
            {
                ButtonBuy.SetActive(true);
                TextForPrice.text = $"Buy {CheckBuy.dataScins.Price}";
                ButtonChoose.SetActive(false);
            }
            else
            {
                ButtonBuy.SetActive(false);
                ButtonChoose.SetActive(true);
            }
        }
    }

    public void LeftChoose()
    {
        AudioForButton.Play();
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y - 40, 0)));
        }
    }

    public void RightChoose()
    {
        AudioForButton.Play();
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y + 40, 0)));
        }
    }

    private IEnumerator RotateTo(Vector3 newRotation)
    {
        isRotating = true;
        float duration = 0.5f; // Продолжительность анимации в секундах
        float elapsedTime = 0f;
        Vector3 initialRotation = transform.eulerAngles;

        while (elapsedTime < duration)
        {
            transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = newRotation; // Устанавливаем окончательный угол
        isRotating = false;
    }
}