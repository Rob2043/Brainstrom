using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckScin : MonoBehaviour
{
    [SerializeField] private GameObject ButtBuy;
    [SerializeField] private GameObject ButtSelect;

    [Header("Lene Info Texts")]
    [SerializeField] private Text InfoText;
    [SerializeField] private Text TextNameScin;
    [SerializeField] private Text AmountStars;
    private DataItems DataScin;

    private GameObject selectScin;

    private void LateUpdate()
    {
        AmountStars.text = $"{Iinstance.instance.stars}"; 
        if (DataScin != null && Iinstance.instance.DataScins != null)
        {
            TextNameScin.text = DataScin.Name;
            if (Iinstance.instance.DataScins.ContainsKey(DataScin.Name) && Iinstance.instance.DataScins[DataScin.Name] == 1)
            {
                ButtBuy.SetActive(false);
                ButtSelect.SetActive(true);
                if(DataScin.Name != Iinstance.instance.SelectScin.name)
                    InfoText.text = "Choose";
                else
                    InfoText.text = "Select";
            }
            else
            {
                ButtBuy.SetActive(true);
                ButtSelect.SetActive(false);
                InfoText.text = $"Buy {DataScin.Price}";
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Colision") && other.CompareTag("Player"))
        {
            DataScin = other.gameObject.GetComponent<PlayerData>().dataItems;
            selectScin = other.gameObject;
        }
    }

    public void BuyButton()
    {
        Iinstance iinstance = Iinstance.instance;
        if (iinstance.stars >= DataScin.Price)
        {
            iinstance.stars -= DataScin.Price;
            iinstance.SelectScin = selectScin;
            iinstance.DataScins.Add(DataScin.Name, 1);
            iinstance.SaveData();
        }
    }

    public void SelectScin()
    {
        Iinstance.instance.SelectScin = selectScin;
        Debug.Log(selectScin);
        InfoText.text = "Select";
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
