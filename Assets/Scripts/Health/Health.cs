using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("InvicibilityTimer")]
    [SerializeField] private float invincibilityTimer;
    [SerializeField] private int highlightLength;
    private SpriteRenderer spriteRenderer;

    [Header("UI")]
    [SerializeField] GameObject deathScreen;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            //player hurt
            animator.SetTrigger("hurt");
            //Invincibility Timer
            StartCoroutine(Invincibility());
        }
        else
        {
            //player dies
            if (!dead)
            {
                animator.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                deathScreen.GetComponent<DeathScreen>().DeathOverlay();
            }            
        }
    }

    public void ApplyPotion(float _val)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _val, 0, startingHealth);
    }
    
    private IEnumerator Invincibility(float duration)
    {
        Physics2D.IgnoreLayerCollision(8,9,true);
        //Length of Invincibility
        for (int i = 0; i < highlightLength; i++)
        {
            spriteRenderer.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(invincibilityTimer / (highlightLength * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invincibilityTimer / (highlightLength * 2));
        }
        Physics2D.IgnoreLayerCollision(8,9,false);
    }

    public void ApplyInvincibility(float duration)
    {
        StartCoroutine(Invincibility(duration));
    }
}