using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //[SerializeField] private Transform prevRoom;
    //[SerializeField] private Transform nextRoom;
    //[SerializeField] private CameraController cam;
    [SerializeField] private BoxCollider2D doorBoxCollider;
    [SerializeField] private string requiredKeyName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Collectible collectible = collision.collider.GetComponent<Collectible>();
            if (collectible != null && collectible.KeyList().Contains(requiredKeyName))
            {
                if (requiredKeyName.IndexOf("Level", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    SceneManager.LoadScene(requiredKeyName);
                }

                //Destroy the blocker
                Destroy(doorBoxCollider);
            }            
        }
    }
}
