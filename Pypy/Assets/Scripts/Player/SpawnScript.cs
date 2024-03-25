using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private void Start()
    {
        Instantiate(Iinstance.instance.SelectScin, gameObject.transform.position, gameObject.transform.rotation);
    }
}
