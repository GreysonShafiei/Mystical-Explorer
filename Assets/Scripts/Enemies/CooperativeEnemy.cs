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

    private static bool groupAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null || !canAttack || isAttacking || groupAttacking) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(TriggerGroupAttack());
        }
    }

    private IEnumerator TriggerGroupAttack()
    {
        groupAttacking = true;

        CooperativeEnemy[] allies = FindObjectsOfType<CooperativeEnemy>();
        List<CooperativeEnemy> group = new List<CooperativeEnemy>();

        foreach (var ally in allies)
        {
            if (Vector2.Distance(player.position, ally.transform.position) <= cooperationRadius && ally.canAttack && !ally.isAttacking)
            {
                group.Add(ally);
            }
        }

        // Small delay before attack starts
        yield return new WaitForSeconds(0.2f);

        foreach (var ally in group)
        {
            ally.StartCoroutine(ally.ChargeAttack());
        }

        // Wait for cooldown duration before allowing another group attack
        yield return new WaitForSeconds(attackCooldown + 0.1f);
        groupAttacking = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}
