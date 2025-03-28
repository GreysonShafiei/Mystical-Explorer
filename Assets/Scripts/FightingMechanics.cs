using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingMechanics : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform fireLocation;
    [SerializeField] private GameObject[] fireBalls;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireBalls[FindFireball()].transform.position = fireLocation.position;
        fireBalls[FindFireball()].GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int FindFireball()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
