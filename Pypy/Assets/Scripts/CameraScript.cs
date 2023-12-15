using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 TargetPosition;


    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        position = transform.position;
        TargetPosition = Target.GetComponent<Transform>().position;
    }

    private void Update()
    {
        position.y = TargetPosition.y + (position.y - TargetPosition.y);
        position.x = TargetPosition.x + (position.x - TargetPosition.x);
    }
}
