using UnityEngine;

public class CheckScin : MonoBehaviour
{
    private DataItems DataScin;

    private GameObject selectScin;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Colision") && other.CompareTag("Player"))
        {
            DataScin = other.gameObject.GetComponent<DataItems>();
            selectScin = other.gameObject;
        }
    }

    public void BuyButton()
    {
        if(Iinstance.instance.stars >= DataScin.Price)
        {
            Iinstance.instance.stars -= DataScin.Price;
            Iinstance.instance.SelectScin = selectScin;
            Iinstance.instance.DataScins.Add(DataScin.Name, 1);
        }
    }
}
