using Imapct.Ctrl;
using Imapct.Inventories.Consumable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories
{
    public class DropItem : MonoBehaviour
    {
        [Header("Items")]
        [Tooltip("Items to be drop")]
        [SerializeField] GameObject[] items;

        [Header("Consumable")]
        [Tooltip("Consumable Items to be drop")]
        [SerializeField] GameObject[] consumables;

        private float chance;
        private int itemToDrop;
        private float dropChancePercentage;
        private float chanceConsumable;
        private int consumableItemToDrop;
        private float consumableDropChancePercentage;

        private void Start()
        {
            //Items
            chance = Random.Range(1, 100);
            itemToDrop = Random.Range(0, items.Length);
            dropChancePercentage = items[itemToDrop].GetComponent<Pickup>().dropChancePercentage;

            //Consumables
            chanceConsumable = Random.Range(1, 100);
            consumableItemToDrop = Random.Range(0, consumables.Length);
            consumableDropChancePercentage = consumables[consumableItemToDrop].GetComponent<ConsumablePickup>().dropChancePercentage;
        }

        public void Drop()
        {
            //Items
            if (chance < dropChancePercentage)
            {
                GetItem(itemToDrop);
            }

            //Consumables
            if (chanceConsumable < consumableDropChancePercentage)
            {
                GetConsumableItem(consumableItemToDrop);
            }
        }

        private void GetItem(int itemToDrop)
        {
            var dropedItem = Instantiate(items[itemToDrop]);
            dropedItem.transform.position = transform.position + new Vector3(Random.Range(0, 1), 0.25f, Random.Range(0, 1.5f));
        }

        private void GetConsumableItem(int itemToDrop)
        {
            var dropedItem = Instantiate(consumables[itemToDrop]);
            dropedItem.transform.position = transform.position + new Vector3(Random.Range(0, 1), 0.25f, Random.Range(0, 1.5f));
        }
    }
}
