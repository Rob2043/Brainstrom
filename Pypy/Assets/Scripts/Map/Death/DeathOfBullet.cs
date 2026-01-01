using UnityEngine;
using Pypy.Consts;
using CustomEventBus;


namespace Pypy.Death
{
    public class DeathOfBullet : MonoBehaviour
    {
        [SerializeField] private GameObject ojbectOfEffect;
        private Vector3 direction;
        private void Awake()
        {
            direction = EventBus.GetDirectionOfCannon.Invoke();
            Vector3 _rotation = Vector3.zero;
            switch (direction)
            {
                case Vector3 v when v == new Vector3(-1, 0, 0):
                    _rotation = new Vector3(0, 90, 0);
                    break;
                case Vector3 v when v == new Vector3(1, 0, 0):
                    _rotation = new Vector3(0, -90, 0);
                    break;
                case Vector3 v when v == new Vector3(0, 0, -1):
                    _rotation = new Vector3(0, 0, 0);
                    break;
                case Vector3 v when v == new Vector3(0, 0, 1):
                    _rotation = new Vector3(0, 180, 0);
                    break;
            }
            ojbectOfEffect.transform.eulerAngles = _rotation;
        }
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


