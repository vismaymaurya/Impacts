using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories.Consumable
{
    public class Consumable : MonoBehaviour
    {
        [SerializeField] ConsumableConfig consumableConfig;

        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void UseConsumableItem()
        {
            Health health = player.GetComponent<Health>();
            if (health.GetInjuredData())
            {
                health.Heal(consumableConfig.GetRestorationAmount());
                Destroy(gameObject);
            }
        }
    }
}
