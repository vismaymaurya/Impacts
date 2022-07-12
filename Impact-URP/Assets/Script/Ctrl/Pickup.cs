using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imapct.Combact;
using StarterAssets;

namespace Imapct.Ctrl
{
    public class Pickup : MonoBehaviour
    {
        [Header("Weapon")]
        [SerializeField] WeaponConfig weaponConfig;
        [Header("Pickup Config Value's")]
        [Tooltip("Weapon Pickup Radius")]
        public float pickupRadius = 2f;
        [Tooltip("Pickup Radius Color for Gizmos")]
        public Color pickupColor;
        [Tooltip("Set offset to get proper value")]
        public Vector3 offset;
        [Tooltip("throwSpeed use when pickup object drop")]
        public float throwSpeed = 10f;
        [Tooltip("ItemDropPercentage")]
        [Range(0, 100)]
        public float dropChancePercentage = 0;

        private Vector3 target;

        private bool Picked = false;

        private void Update()
        {
            WeaponPickup();
        }

        private void WeaponPickup()
        {
            target = WeaponSetup.instance.gameObject.transform.position;
            float distace = Vector3.Distance(target, this.transform.position + offset);

            if (!Picked)
            {
                if (distace <= pickupRadius)
                {
                    if (WeaponSetup.instance.pickup)
                    {
                        Picked = true;
                    }
                }
            }

            if (Picked)
            {
                WeaponSetup.instance.UpdateWeapon(weaponConfig);
                Destroy(this.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = pickupColor;
            Gizmos.DrawWireSphere(transform.position, pickupRadius);
        }
    }
}
