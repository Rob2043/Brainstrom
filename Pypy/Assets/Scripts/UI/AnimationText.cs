using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    public float scaleFactor = 1.04f;
    public float duration = 1.0f;
    private Vector3 initialScale;
    private void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(ScaleTo(initialScale * scaleFactor, duration));
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
