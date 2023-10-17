using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyStop : MonoBehaviour
{
    private Rigidbody cube;

    private void Start()
    {
        cube = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Cube"))
        {
            cube.velocity = Vector3.zero;
            cube.angularVelocity = Vector3.zero;
        }
    }
}