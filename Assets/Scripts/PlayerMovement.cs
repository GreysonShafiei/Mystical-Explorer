using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerBody;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float jumpCooldown;
    private float horizontalInput;

    private void Awake()
    {   
        //Get references to character features
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        //Player flipping when going left or right
        if (horizontalInput > 0.01f) 
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        

        //Setter for animator parameters
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());


        //Wall jump mechanic
        if (jumpCooldown > 0.2f)
        {
            

            playerBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerBody.velocity.y);
            if (!isGrounded() && onWall())
            {
                playerBody.gravityScale = 0;
                playerBody.velocity = Vector2.zero;
            }
            else
            {
                playerBody.gravityScale = 7;
            }
        }
        else
        {
            jumpCooldown += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            jump();
        }
    }

    private void jump()
    {
        if (isGrounded())
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpStrength);
            animator.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                playerBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                playerBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            jumpCooldown = 0;
        }
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
    
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0 , new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
