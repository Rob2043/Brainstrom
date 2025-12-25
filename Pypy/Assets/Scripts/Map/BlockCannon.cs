using System.Collections;
using System.Collections.Generic;
using Pypy;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BlockCannon : MonoBehaviour, ICannon
{
    [SerializeField] private float Period;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 direction;
    public bool isActive { get; set; } = true;
    private Queue<Rigidbody> _pool = new Queue<Rigidbody>();
    private const float DELAY = 1;
    private const float SPEED = 10f;
    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            var a = Instantiate(Prefab);
            a.transform.SetParent(gameObject.transform);
            a.transform.localPosition = spawnPoint.localPosition;
            a.SetActive(false);
            _pool.Enqueue(a.GetComponent<Rigidbody>());
        }
    }
    private void Start()
    {
        if (Period != 0)
        {
            StartCoroutine(Fire());
        }
    }
    private IEnumerator Fire()
    {
        if (isActive == true)
        {
            yield return new WaitForSeconds(Period);
            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            obj.AddForce(direction * SPEED, ForceMode.Impulse);
            StartCoroutine(BackToPool(obj));
        }
    }
    private IEnumerator BackToPool(Rigidbody obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(DELAY);
            obj.gameObject.SetActive(false);
            obj.transform.localPosition = spawnPoint.localPosition;
            _pool.Enqueue(obj);
            break;
        }
        StartCoroutine(Fire());
    }
}