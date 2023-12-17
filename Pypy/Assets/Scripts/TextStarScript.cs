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
    // Start is called before the first frame update
    void Start()
    {
        text.text = DieTime.ToString();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DieTime > 0 && check)
        {
            DieTime -= Time.deltaTime;
        }
        int sum = (int)DieTime;
        text.text = "Time: " + sum.ToString();
        if(DieTime <= 0 && check)
        {
            Debug.Log("Test Star");
            check = false;
            ThirdStar.SetActive(false);
        }
    }
}
