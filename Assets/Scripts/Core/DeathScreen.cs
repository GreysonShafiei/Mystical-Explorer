using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathscreen;
    public void DeathOverlay()
    {
        deathscreen.SetActive(true);
    }
}
