using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D playerBody;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {   
        //Get references to character features
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInpt = Input.GetAxis("Horizontal");
        playerBody.velocity = new Vector2(Input.GetAxis("Horizontal")* speed,playerBody.velocity.y);


        //Player flipping when going left or right
        if (horizontalInpt > 0.01f) 
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInpt < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded())
        {
            jump();
        }

        //Setter for animator parameters
        animator.SetBool("Run", horizontalInpt != 0);
        animator.SetBool("grounded", isGrounded());
    }

    private void jump()
    {
        playerBody.velocity = new Vector2(playerBody.velocity.x, speed);
        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0 , Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
