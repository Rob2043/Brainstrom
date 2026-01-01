using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using Pypy;
using UnityEngine;

public class BlockCannon : MonoBehaviour, ICannon
{
    [SerializeField] private float Period;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 direction;
    [SerializeField] private GameObject _fireEffect;
    public bool isActive { get; set; } = true;
    private const float DELAY = 1;
    private const float SPEED = 10f;
    
    private Queue<Rigidbody> _pool = new Queue<Rigidbody>();
    private ParticleSystem _fireParticleSystem;
    private Animator animator;
    private AudioSource _audioSource;

    void Awake()
    {
        EventBus.GetDirectionOfCannon += () => direction;
        _fireParticleSystem = _fireEffect.GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
    public IEnumerator OnFireEffect()
    {
        _fireEffect.SetActive(true);
        _fireParticleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        _fireParticleSystem.Stop();
        _fireEffect.SetActive(false);
    }
    private void OffFireEffect()
    {
        animator.SetBool("OnFire",false);
    }
    private IEnumerator Fire()
    {
        if (isActive == true)
        {
            yield return new WaitForSeconds(Period);
            animator.SetBool("OnFire",true);
            _audioSource.Play();
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