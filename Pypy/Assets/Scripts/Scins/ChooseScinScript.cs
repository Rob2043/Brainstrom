using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChooseScinScript : MonoBehaviour
{
    [SerializeField] private AudioSource AudioForButton;

    private bool isRotating = false;

    public void LeftChoose()
    {
        AudioForButton.Play();
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y - 40, 0)));
        }
    }

    public void RightChoose()
    {
        AudioForButton.Play();
        if (!isRotating)
        {
            StartCoroutine(RotateTo(new Vector3(0, transform.eulerAngles.y + 40, 0)));
        }
    }

    private IEnumerator RotateTo(Vector3 newRotation)
    {
        isRotating = true;
        float duration = 0.5f; // Продолжительность анимации в секундах
        float elapsedTime = 0f;
        Vector3 initialRotation = transform.eulerAngles;

        while (elapsedTime < duration)
        {
            transform.eulerAngles = Vector3.Lerp(initialRotation, newRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = newRotation; // Устанавливаем окончательный угол
        isRotating = false;
    }
}