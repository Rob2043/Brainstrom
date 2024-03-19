using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextStarScript : MonoBehaviour
{  
    [SerializeField] private GameObject ThirdStar;
    
    private readonly Text text;
    private int star = 1;

    [SerializeField] private float DieTime;
    void Start()
    {
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
    private void LateUpdate() {
        if(Time.timeScale == 1f && DieTime > 0) text.text = $"{(int)DieTime}";
    }
    private int OnStar(ref int stars){
        stars += star;
        return SceneManager.GetActiveScene().buildIndex + 1;
    }
    private void OnEnable() {
        MainButtons.CheckStars += OnStar;
    }
    private void OnDisable() {
        MainButtons.CheckStars -= OnStar;
    }
}
