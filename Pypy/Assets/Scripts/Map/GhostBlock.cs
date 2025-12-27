using System.Collections;
using Pypy.Consts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class GhostBlock : MonoBehaviour
{
    [SerializeField] private float _timeToFall = 7f;
    private Rigidbody _rb;
    private Collider _collider;
    private Renderer _rend;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _rend = GetComponent<Renderer>();
        _rb.useGravity = false;
        _collider.isTrigger = false;
        _rb.constraints = RigidbodyConstraints.FreezeAll;   
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag(StringConstants.PLAYER_TAG))
        {
            StartCoroutine(enumerator());
        }
    }
    private IEnumerator enumerator()
    {
        _rend.material.color = Color.Lerp(_rend.material.color, Color.red, 1);
        yield return new WaitForSeconds(_timeToFall * Time.deltaTime);
        _rb.useGravity = true;
        _rb.mass = 20;
        _collider.isTrigger = true;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(5 * Time.deltaTime);
        gameObject.SetActive(false);
    }
}
