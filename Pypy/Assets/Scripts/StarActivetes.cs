
using UnityEngine;

public class StarActivetes : MonoBehaviour
{
    [SerializeField] private GameObject ImageStar2;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ImageStar2.SetActive(true);
            animator.SetBool("Star", false);
            gameObject.SetActive(false);
        }
    }
}
