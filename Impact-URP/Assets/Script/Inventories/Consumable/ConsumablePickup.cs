using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories.Consumable
{
    public class ConsumablePickup : MonoBehaviour
    {
        [SerializeField] GameObject consumableItem;
        [Tooltip("Weapon Pickup Radius")]
        public float pickupRadius = 2f;
        [Tooltip("consumableDropPercentage")]
        [Range(0, 100)]
        public float dropChancePercentage = 0;

        private GameObject player;
        private Vector3 target;

        private bool Picked = false;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            ConsumableSetup consumableSetup = player.GetComponent<ConsumableSetup>();
            target = player.transform.position;
            float distace = Vector3.Distance(target, this.transform.position);

            if (!Picked)
            {
                if (distace <= pickupRadius)
                {
                    if (consumableSetup.pickup)
                    {
                        Picked = true;
                    }
                }
            }

            if (Picked)
            {
                consumableSetup.UpdateConsumabe(consumableItem);
                Destroy(gameObject);
            }
        }
    }

}