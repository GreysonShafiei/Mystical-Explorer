using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator animator;
    private bool dead;

        private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            //player hurt
            animator.SetTrigger("hurt");
            //iframes
        }
        else
        {
            //player dies
            if (!dead)
            {
                animator.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }            
        }
    }

    public void ApplyPotion(float _val)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _val, 0, startingHealth);
    }
    
}