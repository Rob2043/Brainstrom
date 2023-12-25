using UnityEngine;

public class StarActivetes : MonoBehaviour
{
    [SerializeField] private GameObject ImageStar2;
    [SerializeField] private CheckAmountStar checkAmountStar;
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("SpawnEmpty"))
        {
            gameObject.SetActive(false);                 
            gameManager.CountStars++;
            checkAmountStar.SaveStartData(false, true, false);
            PlayerPrefs.SetInt("CountStars", gameManager.CountStars);
            PlayerPrefs.Save();
            ImageStar2.SetActive(true);
        }
    }

}
