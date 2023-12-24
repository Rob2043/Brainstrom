using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TextStarScript : MonoBehaviour
{
    [SerializeField] public float DieTime;
    private Text text;
    [SerializeField] private GameObject ThirdStar;
    private bool check = true;
    public bool CanTime = true;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = DieTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanTime == true)
        {
            if (DieTime > 0 && check)
            {
                DieTime -= Time.deltaTime;
            }
            int sum = (int)DieTime;
            text.text = "Time: " + sum.ToString();
            if (DieTime <= 0 && check)
            {
                Debug.Log("Test Star");
                check = false;
                CanTime = false;
                ThirdStar.SetActive(false);
            }
        }
    }
}
