using System.Collections;
using Pypy.Consts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class GhostBlock : MonoBehaviour
{
    private Rigidbody _rd;
    private Collider _collider;
    private Renderer _rend;
    private void Start()
    {
        _rd = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _rend = GetComponent<Renderer>();
        _rd.useGravity = false;
        _collider.isTrigger = false;
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
        yield return new WaitForSeconds(1 * Time.deltaTime);
        _rd.useGravity = true;
        _rd.mass = 20;
        _collider.isTrigger = true;
        yield return new WaitForSeconds(5 * Time.deltaTime);
        gameObject.SetActive(false);
    }
}
