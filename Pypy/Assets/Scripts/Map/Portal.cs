using System.Collections;
using UnityEngine;
using Pypy.Consts;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _aimTransform;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(StringConstants.PLAYER_TAG))
        {
            StartCoroutine(OnEffectsForPortal());
            Teleport(other.gameObject);
        }  
    }
    private IEnumerator OnEffectsForPortal()
    {
        // I will add some effects later
        yield return new WaitForSeconds(1f);
    }
    private void Teleport(GameObject obj)
    {
        obj.transform.position = _aimTransform.position;
    }
}
