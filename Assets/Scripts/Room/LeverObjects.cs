using System.Collections.Generic;
using UnityEngine;

public class LeverObjects : MonoBehaviour
{
    public List<Lever> levers;
    public List<BoxCollider2D> targetColliders;
    public List<GameObject> targetObjects;
    public List<GameObject> objectsTurnOn;

    private bool isHidden = false;

    void Start()
    {
        if (targetObjects == null || targetObjects.Count == 0)
        {
            Debug.LogError("LeverObjects: Target Objects not assigned!");
        }
    }

    void Update()
    {
        if (targetObjects == null || targetObjects.Count == 0) return;

        bool allLeversOn = true;

        foreach (Lever lever in levers)
        {
            if (lever == null || !lever.unlock)
            {
                allLeversOn = false;
                break;
            }
        }

        // If all levers are ON and the object is not hidden, hide it
        if (allLeversOn && !isHidden)
        {
            foreach (BoxCollider2D box in targetColliders)
            {
                if (targetColliders != null) box.enabled = false;
            }

            foreach (GameObject obj in targetObjects)
            {
                if (obj != null) obj.SetActive(false);
            }

            foreach (GameObject obj in objectsTurnOn)
            {
                if (obj != null) obj.SetActive(true);
            }

            isHidden = true;
            Debug.Log("Hiding object: all levers are ON.");
        }

        // If not all levers are ON and the object is hidden, show it
        else if (!allLeversOn && isHidden)
        {
            foreach (BoxCollider2D box in targetColliders)
            {
                if (targetColliders != null) box.enabled = true;
            }

            foreach (GameObject obj in targetObjects)
            {
                if (obj != null) obj.SetActive(true);
            }

            foreach (GameObject obj in objectsTurnOn)
            {
                if (obj != null) obj.SetActive(false);
            }

            isHidden = false;
            Debug.Log("Showing object: one or more levers turned OFF.");
        }
    }
}
