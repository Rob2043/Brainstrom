using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ChooseScinScript : MonoBehaviour
{
    private bool isRotating = false;
    private GameObject playerCheck;
    private MovePlayerScript CheckBuy;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonChoose;
    private int PriceObject;

    private void LateUpdate()
    {
        CheckScin find = FindObjectOfType<CheckScin>();
        playerCheck = find.player;
        CheckBuy = playerCheck.GetComponent<MovePlayerScript>();
        //PriceObject = CheckBuy.dataScins.Price;
        if(CheckBuy.allowForBuy == false)
        {
            ButtonBuy.SetActive(true);
            ButtonChoose.SetActive(false);
        }
        else
        {
            ButtonBuy.SetActive(false);
            ButtonChoose.SetActive(true);
        }
    }
    public void LeftChoose()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y - 72, 0)));
        }
    }

    public void RightChoose()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y + 72, 0)));
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

