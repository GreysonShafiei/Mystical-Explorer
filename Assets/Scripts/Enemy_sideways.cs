using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float trapMoveRange;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
        leftEdge = transform.position.x - trapMoveRange;
        rightEdge = transform.position.x + trapMoveRange;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(originalScale.x * (movingLeft ? -1 : 1), originalScale.y, originalScale.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(originalScale.x * (movingLeft ? -1 : 1), originalScale.y, originalScale.z);
            }
            else
            {
                movingLeft = true;
            }
        }
        transform.localScale = new Vector3(originalScale.x * (movingLeft ? 1 : -1), originalScale.y, originalScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
