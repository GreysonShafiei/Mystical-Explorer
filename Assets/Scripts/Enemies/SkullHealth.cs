using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject enemy;

    public void Hit(float _damage)
    {
        if (health > 0)
        {
            //Damage enemy
            health -= _damage;
        }
        else
        {
            //Kill enemy
            enemy.SetActive(false);
            Destroy(enemy);
        }
    }
}
