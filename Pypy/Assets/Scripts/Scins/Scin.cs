using CustomEventBus;
using UnityEngine;
using UnityEngine.UI;

public class Scin : MonoBehaviour
{
    public Button clickButton;
    [SerializeField] private int price;
    private Text textButton;
    [SerializeField] private string text;
    private void Start()
    {
        textButton = clickButton.GetComponentInChildren<Text>();
        if (PlayerPrefs.GetInt($"{gameObject.name}_enable", 0) == 1)
        {
            Iinstance.instance.SelectScin = gameObject;
            text = "Selected";
        }
        else if (PlayerPrefs.GetInt($"{gameObject.name}_isBuy", 0) == 1)
        {
            text = "Select";
        }
        else text = $"Buy {price}";
    }
    public void OnClick()
    {
        if (textButton.text == "Select")
        {
            Iinstance.instance.SelectScin = gameObject;
            textButton.text = "Selected";
            EventBus.ScinCheckButton.Invoke(gameObject);
        }
        else if (textButton.text == $"Buy {price}")
        {
            if (price <= Iinstance.instance.stars)
            {
                textButton.text = "Selected";
                Iinstance.instance.SelectScin = gameObject;
                Iinstance.instance.stars -= price;
                EventBus.ScinCheckButton.Invoke(gameObject);
                // SaveData(1, 1);
            }
            else return;
        }
    }

    private void OnEnable()
    {
        EventBus.ScinCheckButton += Check;
        EventBus.ChangeNameText += ChangeText;
        // EventBus.ChangeNameText += ChangeButtonFunc;
    }
    private void OnDisable()
    {
        EventBus.ScinCheckButton -= Check;
        EventBus.ChangeNameText -= ChangeText;
        // EventBus.ChangeNameText -= ChangeButtonFunc;
    }
    private void Check(GameObject ingameObject)
    {
        if (textButton.text == "Selected" && ingameObject != gameObject)
        {
            textButton.text = "Select";
            SaveData(0, 1);
        }
        else
            SaveData(1, 1);
    }
    private void ChangeText(GameObject ingameObject)
    {
        if (ingameObject == gameObject)
            textButton.text = text;        
    }

    // private void ChangeButtonFunc(GameObject ingameObject)
    // {
    //     clickButton.onClick.RemoveAllListeners();        
    //     if (ingameObject == gameObject)
    //         Debug.Log(gameObject.name + "work");
    //         clickButton.onClick.AddListener(OnClick);
    // }
    private void SaveData(int enable, int isBuy)
    {
        PlayerPrefs.SetInt($"{gameObject.name}_enable", enable);
        PlayerPrefs.SetInt($"{gameObject.name}_isBuy", isBuy);
        PlayerPrefs.Save();
    }
}
