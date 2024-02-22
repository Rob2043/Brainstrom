﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    private GameObject TextCountStars;
    [SerializeField] private float speed;
    public GameObject panel;
    public bool checkScin = false;
    private Rigidbody rb;
    public bool checkStar;
    public bool allowForBuy = false;
    public DataItems dataScins;
    private GameObject[] Box = { };
    public AudioSource Audio;
    public AudioSource MainAudio;


    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        SwipeScript.SwipeEvent += HandleSwipePlayer;
        SwipeScript1.SwipeEvent += HandleSwipePlayer;
        TextCountStars = GameObject.FindGameObjectWithTag("CountStar");

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Scins")
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            panel = GameObject.FindGameObjectWithTag("localPanel");
            panel.SetActive(false);
        }
    }

    private void Update()
    {
        GameObject BoxInScene = GameObject.FindGameObjectWithTag("Cube");
        Box.Append(BoxInScene);
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
        if (gameObject.CompareTag("Player") && other.CompareTag("Finish"))
        {
            Audio.Play();
            MainAudio.enabled = false;
            panel.SetActive(true);
            speed = 0f;
            if (PlayerPrefs.GetInt("MaxLevel") <= SceneManager.GetActiveScene().buildIndex + 1)
            {
                PlayerPrefs.SetInt("MaxLevel", SceneManager.GetActiveScene().buildIndex + 1); // Save the current level to PlayerPrefs
                PlayerPrefs.Save(); // Save the PlayerPrefs data
            }
            GameObject gameManager = GameObject.FindGameObjectWithTag("GlobalManager");
            GameObject Time = GameObject.FindGameObjectWithTag("Time");
            GameObject ThirdStar = gameManager.GetComponent<GameManager>().ThirdStar;
            CheckAmountStar checkAmountStar = gameManager.GetComponent<CheckAmountStar>();
            float Level = (float)(SceneManager.GetActiveScene().buildIndex - 1) / 3;
            float EndLevelValue = Level - (int)Level;
            if (EndLevelValue == 0)
            {
                InterstitialAdExample Ads = GetComponent<InterstitialAdExample>();
                Ads.ShowAd();
            }
            foreach (GameObject b in Box)
            {
                b.GetComponent<Cup>().checkLevel = false;
            }
            if (!PlayerPrefs.HasKey($"OneStar{SceneManager.GetActiveScene().name}"))
            {
                gameManager.GetComponent<GameManager>().CountStars += 1;
                checkAmountStar.SaveStartData(true, false, false);
            }
            if (Time != null && gameManager != null)
            {
                float dieTime = Time.GetComponent<TextStarScript>().DieTime;
                if (dieTime > 0)
                {
                    ThirdStar.SetActive(true);
                    if (!PlayerPrefs.HasKey($"ThreeStar{SceneManager.GetActiveScene().name}"))
                    {
                        gameManager.GetComponent<GameManager>().CountStars += 1;
                        checkAmountStar.SaveStartData(false, false, true);
                    }
                }
                else
                {
                    gameManager.GetComponent<GameManager>().CountStars += 0;
                }
                Time.SetActive(false);
                
            }
            TextCountStars.GetComponent<Text>().text = gameManager.GetComponent<GameManager>().CountStars.ToString();
            PlayerPrefs.SetInt("CountStars", gameManager.GetComponent<GameManager>().CountStars);
            PlayerPrefs.Save();
        }
    }
}
