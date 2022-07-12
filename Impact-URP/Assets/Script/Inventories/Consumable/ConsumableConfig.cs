using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories.Consumable
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable/New Consumable", order = 0)]
    public class ConsumableConfig : ScriptableObject
    {
        [Tooltip("Restoration amount to heal/recover mana and energy")]
        [SerializeField] int consumableRestoration;

        public int GetRestorationAmount()
        {
            return consumableRestoration;
        }
    }
}