using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{
    public float resetTimer = 15f;
    public GameObject onVisual;
    public GameObject offVisual;

    private bool leverActivation = false;
    public bool unlock;

    private Coroutine resetCoroutine;

    private void Start()
    {
        if (onVisual != null) onVisual.SetActive(false);
        if (offVisual != null) offVisual.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Lever triggered by: " + collision.name);
        if (collision.CompareTag("Fireball"))
        {
            Debug.Log("Fireball detected!");

            // Toggle state
            leverActivation = !leverActivation;
            unlock = leverActivation;
            Debug.Log("Lever auto-reset. New unlock = " + unlock);

            // Update visuals based on new state
            if (onVisual != null) onVisual.SetActive(leverActivation);
            if (offVisual != null) offVisual.SetActive(!leverActivation);

            // Reset countdown timer
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(TimedReset());
        }
    }

    private IEnumerator TimedReset()
    {
        yield return new WaitForSeconds(resetTimer);

        // Toggle again after delay
        leverActivation = !leverActivation;
        unlock = leverActivation;

        if (onVisual != null) onVisual.SetActive(leverActivation);
        if (offVisual != null) offVisual.SetActive(!leverActivation);
    }
}