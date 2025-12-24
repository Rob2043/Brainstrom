using UnityEngine;
using Pypy.Consts;
using CustomEventBus;
using Pypy;

public class DeathOfFalling : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(StringConstants.PLAYER_TAG))
        {
            EventBus.DeathPlayer.Invoke();
        }
    }
}
