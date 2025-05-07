using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_vertical : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float trapMoveRange;
    [SerializeField] private float waitPeriod = 0;
    private bool movingDown;
    private float bottomEdge;
    private float topEdge;
    private Vector3 originalScale;
    private bool canMove = false;

    private void Awake()
    {
        originalScale = transform.localScale;
        bottomEdge = transform.position.y - trapMoveRange;
        topEdge = transform.position.y + trapMoveRange;        
    }

    private void Start()
    {
        StartCoroutine(WaitToMove(waitPeriod));
    }

    private void Update()
    {
        if (!canMove) return;
        
        if (movingDown)
        {
            if (transform.position.y > bottomEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = false;
            }
        }
        else
        {
            if (transform.position.y < topEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = true;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, movingDown ? -90f : 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator WaitToMove(float waitPeriod)
    {
        yield return new WaitForSeconds(waitPeriod);
        canMove = true;
    }
}
