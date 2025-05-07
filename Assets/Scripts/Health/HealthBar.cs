using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UnityEngine.UI.Image totalHealthbar;
    [SerializeField] private UnityEngine.UI.Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.CurrentHealth / 10;
    }
}