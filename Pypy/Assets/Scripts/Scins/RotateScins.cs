using System.Collections;
using CustomEventBus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScinScript : MonoBehaviour
{
    private bool isRotating = false;
    private void CheckSwipe(Vector2 direction)
    {
        if (isRotating)
        {
            return;
        }
        Vector3 force = Vector3.zero;
        if (direction == Vector2.left)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y - 40, 0)));
        } else if(direction == Vector2.right)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y + 40, 0)));
        }
    }
    private IEnumerator RotateTo(Vector3 newRotation)
    {
        isRotating = true;
        float duration = 0.5f;
        float elapsedTime = 0f;
        Vector3 initialRotation = transform.eulerAngles;

        while (elapsedTime < duration)
        {
            transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = newRotation;
        isRotating = false;
    }
    void OnEnable()
    {
        EventBus.WasMoving += CheckSwipe;
    }
    void OnDisable()
    {
        EventBus.WasMoving -= CheckSwipe;
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}