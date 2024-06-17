using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomEventBus;
public class TextStarScript : MonoBehaviour
{
    [SerializeField] private GameObject ThirdStar;
    [field: SerializeField] private float DieTime;
    private Text text;
    private int star = 1;
    private void Start() 
    {
        EventBus.CheckStars = OnStar;
        text = gameObject.GetComponent<Text>();
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
                ThirdStar.SetActive(false);
            }
        }
    }
    private void LateUpdate()
    {
        if (Time.timeScale == 1f && DieTime > 0) text.text = $"{(int)DieTime}";
    }
    private int OnStar()
    {
        ThirdStar.SetActive(true);
        if(PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().buildIndex - 2}_Stars {2}", 0) == 0)
            return star;
        else return 0; 
    }

}
