using UnityEngine;
using CustomEventBus;

public class StarAnimation : MonoBehaviour
{
    public float _time = 0;
    public float Amp = 0.25f;
    public float Freq = 2;
    public float Offset = 0;
    public Vector3 StartPos;

    void Start()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        _time += Time.deltaTime;
        Offset = Amp * Mathf.Sin(_time * Freq);
        transform.position = StartPos + new Vector3(0, Offset, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventBus.AddStarsInPlay.Invoke();
            gameObject.SetActive(false);
        }
    }
}