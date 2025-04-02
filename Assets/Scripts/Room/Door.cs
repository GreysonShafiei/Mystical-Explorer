using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private string requiredKeyName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collectible collectible = collision.GetComponent<Collectible>();
            if (collectible != null && collectible.KeyList().Contains(requiredKeyName))
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    cam.MoveToNextRoom(nextRoom);
                }
                else
                {
                    cam.MoveToNextRoom(prevRoom);
                }
            }            
        }
    }
}
