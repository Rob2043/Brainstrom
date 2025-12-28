using UnityEngine;
using CustomEventBus;

public class CameraControlFirewors : MonoBehaviour
{
    [SerializeField] private GameObject[] _fireworks = new GameObject[3];
    private ParticleSystem[] _particles = new ParticleSystem[3];

    void OnEnable()
    {
        EventBus.OnWinFireworks += OnFireworks;
        for (int i = 0; i < _fireworks.Length; i++)
        {
            _particles[i] = _fireworks[i].GetComponent<ParticleSystem>();
        }
    }
    private void OnFireworks()
    {
        for (int i = 0; i < _fireworks.Length; i++)
        {
            _fireworks[i].SetActive(true);
            _particles[i].Play();
        }
    }

    void OnDisable()
    {
        EventBus.OnWinFireworks -= OnFireworks;
    }
}
