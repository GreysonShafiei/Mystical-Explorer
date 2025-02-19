using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D playerBody;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerBody.velocity = new Vector2(Input.GetAxis("Horizontal")* speed,playerBody.velocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, speed);
        }
    }
}
