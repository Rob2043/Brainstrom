using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCannon : MonoBehaviour
{
    [SerializeField] private float Period;
    [SerializeField] private GameObject Prefab;
    private Queue<Rigidbody> _pool;
    private const float DELAY = 5;
    private const float SPEED = 5;
    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            var a = Instantiate(Prefab);
            a.SetActive(false);
            _pool.Enqueue(a.GetComponent<Rigidbody>());
        }
    }
    private void Start()
    {
        if(Period != 0)
        {
            StartCoroutine(Fire());   
        }
    }
    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(Time.deltaTime * Period);
        var obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.AddForce(-Vector3.forward * SPEED, ForceMode.Impulse);
        StartCoroutine(BackToPool(obj));
    }
    private IEnumerator BackToPool(Rigidbody obj)
    {
        while(true){
            yield return new WaitForSeconds(Time.deltaTime * DELAY);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            break;
        }
    }
}
