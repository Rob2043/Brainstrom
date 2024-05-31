using UnityEngine;
using CustomEventBus;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventBus.ChangeNameText.Invoke(other.gameObject);
    }
}
