using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooperativeEnemy : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float attackForce = 10f;
    [SerializeField] private float cooperationRadius = 6f;

    private Rigidbody2D rb;
    private Transform player;
    private bool canAttack = true;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null || !canAttack || isAttacking) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && !NearbyAllyIsAttacking())
        {
            StartCoroutine(ChargeAttack());
        }
    }

    private IEnumerator ChargeAttack()
    {
        isAttacking = true;
        canAttack = false;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * attackForce;

        yield return new WaitForSeconds(0.3f); // dash time
        rb.velocity = Vector2.zero;

        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private bool NearbyAllyIsAttacking()
    {
        CooperativeEnemy[] allEnemies = FindObjectsOfType<CooperativeEnemy>();
        foreach (var ally in allEnemies)
        {
            if (ally == this) continue;
            float dist = Vector2.Distance(transform.position, ally.transform.position);
            if (dist <= cooperationRadius && ally.isAttacking)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking && collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}