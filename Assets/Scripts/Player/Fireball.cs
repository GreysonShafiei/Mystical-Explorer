using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fireballDamage;
    private bool hit;
    private float direction;
    private float fireballLifetime;

    private BoxCollider2D boxCollider2D;
    private Animator anim;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        //If fireball is active for 5 seconds deactivate
        fireballLifetime += Time.deltaTime;
        if (fireballLifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider2D.enabled = false;
        anim.SetTrigger("explode");
        if (collision.CompareTag("EnemyTarget"))
        {
            collision.GetComponent<SkullHealth>().Hit(fireballDamage);
        }
    }

    public void SetDirection(float _direction)
    {
        fireballLifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
