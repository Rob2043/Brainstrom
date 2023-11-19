﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TextStarScript : MonoBehaviour
{
    [SerializeField] public float DieTime;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject ThirdStar;
    private bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        ThirdStar = GameObject.Find("Stars 3");
        text.text = DieTime.ToString();
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
        if(DieTime <= 0)
        {
            check = false;
            ThirdStar.SetActive(false);
        }
    }
}
