using UnityEngine;
using Pypy.Consts;
using CustomEventBus;
using Pypy;

namespace Pypy.Death
{
    public class DeathOfBullet : MonoBehaviour
    {
        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.CompareTag(StringConstants.PLAYER_TAG))
            {
                EventBus.DeathPlayer.Invoke();
                gameObject.GetComponentInParent<ICannon>().isActive = false;
                //I will add effects of destruction of bullet here
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
            }
        }
    }
}


