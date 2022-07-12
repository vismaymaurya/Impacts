using Imapct.Combact;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField] HealthBar healthBar;

    private int currenthealth;
    private bool injured = false;

    private void OnEnable()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currenthealth >= maxHealth)
        {
            injured = false;
        }
        else
        {
            injured = true;
        }
    }

    public void TakeDamge(int damageAmount)
    {
        currenthealth = currenthealth - damageAmount;
        healthBar.SetHealth(currenthealth);

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healingAmount)
    {
        if (currenthealth >= maxHealth)
        {
            currenthealth = maxHealth;
            injured = false;
        }
        if (currenthealth <= maxHealth)
        {
            injured = true;
            currenthealth = currenthealth + healingAmount;
        }
        healthBar.SetHealth(currenthealth);
    }

    public bool GetInjuredData()
    {
        return injured;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
