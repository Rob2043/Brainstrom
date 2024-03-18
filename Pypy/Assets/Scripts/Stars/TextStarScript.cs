using UnityEngine;
using UnityEngine.UI;

public class TextStarScript : MonoBehaviour
{  
    [SerializeField] private GameObject ThirdStar;
    
    private readonly Text text;

    public float DieTime;
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
                ThirdStar.SetActive(false);
            }
        }
    }
    private void LateUpdate() {
        if(Time.timeScale == 1f) text.text = $"{(int)DieTime}";
    }
}
