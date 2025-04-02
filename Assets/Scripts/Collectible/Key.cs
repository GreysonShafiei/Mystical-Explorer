using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered key trigger");
            Collectible collectible = collision.GetComponent<Collectible>();

            if (collectible != null)
            {
                string keyname = gameObject.name;
                collectible.AddKey(keyname);
                Debug.Log(keyname);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Collectible script NOT found on player!");
            }
        }
    }
}
