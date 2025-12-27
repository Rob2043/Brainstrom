using UnityEngine;

public class StarsLevel : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt($"{transform.parent.name}_{name}", 0) is 0)
        {
            gameObject.SetActive(false);
        }
        else gameObject.SetActive(true);
    }
}
