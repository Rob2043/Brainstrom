using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomEventBus;
using System;

public class TextStarScript : MonoBehaviour
{
    [SerializeField] private GameObject ThirdStar;
    [field: SerializeField] private float DieTime;
    private readonly Text text;
    private int star = 1;


    private void Start() =>
        text.text = $"{DieTime}";
    private void OnDisable()
    {
        EventBus.CheckStars += OnStar;
    }

    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (DieTime > 0)
            {
                DieTime -= Time.deltaTime;
            }
            if (DieTime <= 0)
            {
                star = 0;
                ThirdStar.SetActive(false);
            }
        }
    }
    private void LateUpdate()
    {
        if (Time.timeScale == 1f && DieTime > 0) text.text = $"{(int)DieTime}";
    }
    private (int,int) OnStar(int stars)
    {
        return (SceneManager.GetActiveScene().buildIndex + 1,stars+= star); 
    }

}
