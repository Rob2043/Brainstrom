
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarActivetes : MonoBehaviour
{
    [SerializeField] private GameObject ImageStar2;
    public Animator animator;
    private GameObject starsEarned;


    private void OnTriggerEnter(Collider other)
    {
        starsEarned = GameObject.FindGameObjectWithTag("GlobalManager");

        if (other.CompareTag("Player") || other.CompareTag("SpawnEmpty"))
        {
            GameManager gameManager = starsEarned.GetComponent<GameManager>();
            if (gameManager != null)
            {
                gameManager.CountStars++;
            }
            else
            {
                Debug.LogWarning("GameManager component not found on the 'starsEarned' object.");
            }
            ImageStar2.SetActive(true);
            animator.SetBool("Star", false);
            gameObject.SetActive(false);
        }
    }

}
