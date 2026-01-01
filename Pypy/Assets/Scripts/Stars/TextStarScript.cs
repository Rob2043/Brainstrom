using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomEventBus;
using TMPro;

public class TextStarScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [field: SerializeField] private float DieTime;
    private int star = 1;
    private void Start()
    {
        EventBus.CheckStars = OnStar;
        text.text = $"{DieTime}";
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
            }
        }
    }
    private void LateUpdate()
    {
        if (Time.timeScale == 1f && DieTime > 0) text.text = $"{(int)DieTime}";
    }
    private int OnStar()
    {
        if (PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().buildIndex - 3}_Stars {2}", 0) == 0)
            return star;
        else return 0;
    }

}
