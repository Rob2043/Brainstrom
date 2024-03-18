using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private float speed;
    private Rigidbody rb;
    public AudioSource Audio;
    public AudioSource MainAudio;


    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        SwipeScript.SwipeEvent += HandleSwipePlayer;
        SwipeScript1.SwipeEvent += HandleSwipePlayer;
    }
    private void HandleSwipePlayer(Vector2 direction)
    {
        if (rb != null)
        {
            Vector3 force = Vector3.zero;

            if (direction == Vector2.left)
            {
                force = new Vector3(0, 0, -moveDirection.z * speed);
            }
            else if (direction == Vector2.right)
            {
                force = new Vector3(0, 0, moveDirection.z * speed);
            }
            else if (direction == Vector2.up)
            {
                force = new Vector3(-moveDirection.x * speed, 0, 0);
            }
            else if (direction == Vector2.down)
            {
                force = new Vector3(moveDirection.x * speed, 0, 0);
            }

            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Time.timeScale = 0f;
            Audio.Play();
            MainAudio.enabled = false;
            endPanel.SetActive(true);
            float localScene = SceneManager.GetActiveScene().buildIndex;
            if (PlayerPrefs.GetInt("MaxLevel") <= localScene + 1)
            {
                PlayerPrefs.SetInt("MaxLevel", (int)(localScene + 1)); // Save the current level to PlayerPrefs
                PlayerPrefs.Save(); // Save the PlayerPrefs data
            }
            //float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
            //float EndLevelValue = Level - (int)Level;
            //if (EndLevelValue == 0)
            //{
            //    InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
            //    Ads.ShowAd();
            //}
        }
    }
}
