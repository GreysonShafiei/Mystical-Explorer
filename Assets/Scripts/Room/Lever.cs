using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{
    public GameObject onVisual;
    public GameObject offVisual;

    private bool leverActivation = false;
    public bool unlock;

    private Coroutine resetCoroutine;

    private void Start()
    {
        if (onVisual != null) onVisual.SetActive(true);
        if (offVisual != null) offVisual.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Lever triggered by: " + collision.name);
        if (collision.CompareTag("Fireball"))
        {
            leverActivation = true;
            unlock = true;

            if (onVisual != null) onVisual.SetActive(true);
            if (offVisual != null) offVisual.SetActive(false);

            // Start/reset the countdown
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(TimedReset());
        }
    }

    private IEnumerator TimedReset()
    {
        yield return new WaitForSeconds(15f); // wait 15 seconds

        leverActivation = false;
        unlock = false;

        if (onVisual != null) onVisual.SetActive(false);
        if (offVisual != null) offVisual.SetActive(true);
    }
}