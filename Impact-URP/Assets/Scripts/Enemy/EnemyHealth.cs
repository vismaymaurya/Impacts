using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imapct.Inventories;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currenthealth;
    private DropItem dropItem;

    private void OnEnable()
    {
        currenthealth = maxHealth;
    }

    private void Start()
    {
        dropItem = GetComponent<DropItem>();
    }

    public void TakeDamge(int damageAmount)
    {
        currenthealth = currenthealth - damageAmount;

        if (currenthealth <= 0)
        {
            dropItem.Drop();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
