using UnityEngine;
using UnityEngine.UI;

public class StarsLevel : MonoBehaviour
{
    private void Start() {
        if(PlayerPrefs.GetInt($"{transform.parent.name}_{name}", 0) == 0){
            gameObject.SetActive(false);
        } else gameObject.SetActive(true);
    }
}
