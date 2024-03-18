using UnityEngine;
using UnityEngine.UI;

public class TextStarScript : MonoBehaviour
{

    private Text text;
    [SerializeField] private GameObject ThirdStar;

    public bool CanTime = true;
    public float DieTime;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = $"{DieTime}";
    }
    void Update()
    {
        if (CanTime)
        {
            if (DieTime > 0)
            {
                DieTime -= Time.deltaTime;
            }
            if (DieTime <= 0)
            {
                CanTime = false;
                ThirdStar.SetActive(false);
            }
        }
    }
    private void LateUpdate() {
        text.text = $"{(int)DieTime}";
    }
}
