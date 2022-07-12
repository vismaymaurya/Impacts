using Imapct.Combact;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Inventories.Consumable
{
    public class ConsumableSetup : MonoBehaviour
    {
        public Transform consumableHolder;

        [HideInInspector]
        public bool pickup;

        private StarterAssetsInputs _input;

        private void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            pickup = GetComponent<WeaponSetup>().pickup;
        }

        public void UpdateConsumabe(GameObject consumablePrefab)
        {
            var setupConsumable = Instantiate(consumablePrefab, consumableHolder);
            setupConsumable.transform.position = transform.position;
        }
    }
}
