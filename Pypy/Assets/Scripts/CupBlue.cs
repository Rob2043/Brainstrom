using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CupBlue : MonoBehaviour
{
    [SerializeField] KeyCode keyOne;
    [SerializeField] KeyCode keyTwo;
    [SerializeField] Vector3 moveDirection;
    

    private void Start()
    {
        SwipeScript.SwipeEvent += HandleSwipe;
    }
    private void HandleSwipe(Vector2 direction)
    {
        // Здесь вы можете выполнить необходимые действия в зависимости от направления свайпа.
        if (direction == Vector2.up)
        {
            
            GetComponent<Rigidbody>().velocity -= moveDirection;
        }
        else if (direction == Vector2.down)
        {
            GetComponent<Rigidbody>().velocity += moveDirection;
        }
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKey(keyOne))
    //    {
    //        GetComponent<Rigidbody>().velocity += moveDirection;
    //    }
    //    if (Input.GetKey(keyTwo))
    //    {
    //        GetComponent<Rigidbody>().velocity -= moveDirection;
    //    }
    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            PlayerPrefs.SetInt("level",SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}