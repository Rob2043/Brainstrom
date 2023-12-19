
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarActivetes : MonoBehaviour
{
    [SerializeField] private GameObject ImageStar2;
    private GameObject starsEarned;

    private void OnTriggerEnter(Collider other)
    {
        starsEarned = GameObject.FindGameObjectWithTag("GlobalManager");
        CheckAmountStar checkAmountStar = starsEarned.GetComponent<CheckAmountStar>();

        if (other.CompareTag("Player") || other.CompareTag("SpawnEmpty"))
        {
            gameObject.SetActive(false);
            GameManager gameManager = starsEarned.GetComponent<GameManager>();
            if (gameManager != null)
            {
                if (!PlayerPrefs.HasKey($"TwoStar{SceneManager.GetActiveScene()}"))
                {
                    gameManager.CountStars++;
                    checkAmountStar.SaveStartData(false, true, false);
                }
            }
            else
            {
                Debug.LogWarning("GameManager component not found on the 'starsEarned' object.");
            }
            ImageStar2.SetActive(true);
        }
    }

}
