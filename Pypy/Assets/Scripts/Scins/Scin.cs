using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class Scin : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    [SerializeField] private int price;
    private TMP_Text textButton;

    private delegate void RefreshButton(GameObject scin);
    private static event RefreshButton CheckButton;
    private void Start()
    {
        textButton = clickButton.GetComponentInChildren<TMP_Text>();
        if (PlayerPrefs.GetInt($"{gameObject.name}_enable", 0) == 1)
        {
            Iinstance.instance.SelectScin = gameObject;
            textButton.text = "Selected";
        }
        else if (PlayerPrefs.GetInt($"{gameObject.name}_isBuy", 0) == 1)
        {
            textButton.text = "Select";
        }
        else textButton.text = "Buy";
    }
    public void OnClick()
    {
        if (textButton.text == "Select")
        {
            Iinstance.instance.SelectScin = gameObject;
            textButton.text = "Selected";
            CheckButton.Invoke(gameObject);
            SaveData(1, 1);
        }
        else if (textButton.text == "Buy")
        {
            if (price <= Iinstance.instance.stars)
            {
                textButton.text = "Selected";
                Iinstance.instance.SelectScin = gameObject;
                Iinstance.instance.stars -= price;
                CheckButton.Invoke(gameObject);
                SaveData(1, 1);
            }
            else return;
        }
    }
    private void Check(GameObject ingameObject)
    {
        if (textButton.text == "Selected" && ingameObject != gameObject)
        {
            textButton.text = "Select";
            SaveData(0, 1);
        }
    }
    private void OnEnable()
    {
        CheckButton += Check;
    }
    private void OnDisable()
    {
        CheckButton -= Check;
    }
    private void SaveData(int enable, int isBuy)
    {
        PlayerPrefs.SetInt($"{gameObject.name}_enable", enable);
        PlayerPrefs.SetInt($"{gameObject.name}_isBuy", isBuy);
        PlayerPrefs.Save();
    }
}
