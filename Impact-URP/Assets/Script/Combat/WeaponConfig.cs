using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Combact
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        enum WeaponPickupHand
        {
            WeaponSlot_01,
            WeaponSlot_02
        }

        enum WeaponCategory
        {
            Unarmed,
            Melee,
            Pistol,
            SubMachineGun,
            Rifle,
            AssaultRifle,
            SniperRifle,
            Shotgun,
            MachineGun,
            GranadeLauncher,
            RocketLauncher
        }

        enum TypeOfWeapon
        {
            SingleShot,
            Automatic,
            Burst
        }

        [Tooltip("Weapon Name To display on UI")]
        [SerializeField] string displayName = "";
        [Tooltip("Weapon Icon")]
        public Sprite Icon = null;
        [SerializeField] WeaponCategory weaponCategory;
        [SerializeField] TypeOfWeapon weaponType;
        [SerializeField] WeaponPickupHand weaponHand;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [Tooltip("Weapon prefab with right rig")]
        [SerializeField] Weapon equippedPrefab = null;
        [Tooltip("Weapon pickup prefabs")]
        public GameObject weaponPickup = null;
        [Tooltip("Weapon projectile prefabs")]
        public GameObject projectile = null;
        [Tooltip("Weapon Bullet/Projectile Fire Speed")]
        [SerializeField] float fireRate = .05f;
        [Tooltip("How much time Req. to reload Weapon")]
        [SerializeField] float reloadTime = 5;
        [Tooltip("Area of explosion for explosive type weapon only")]
        [SerializeField] float explosionArea;
        [Tooltip("Damage Done by projectile/Bullet")]
        [SerializeField] int weaponDamage = 10;
        [Tooltip("Max Amount of ammo hold by weapon")]
        [SerializeField] int maxAmmoAmount = 100;
        [Tooltip("Amount of ammo hold by weapon")]
        [SerializeField] int minAmmoAmount = 30;
        [Tooltip("Number of bullets/projeectile fire in burst")]
        [SerializeField] int burstAmmount = 3;
        [HideInInspector]
        public int weaponTypeArray;

        const string weaponName = "Weapon";

        public Weapon Spawn(Transform slot_01, Transform slot_02)
        {
            FindWeapon(slot_01, slot_02);
            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(slot_01, slot_02);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            return weapon;
        }

        public void SwapAnimation(Animator animator)
        {
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void FindWeapon(Transform slot_01, Transform slot_02)
        {
            Transform oldWeapon = slot_01.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = slot_02.Find(weaponName);
            }
            if (oldWeapon == null) { return; }
        }

        private Transform GetTransform(Transform slot_01, Transform slot_02)
        {
            Transform weaponSlotTransform;

            //Transform handTransform;
            //if (weaponHand == WeaponPickupHand.WeaponSlot_01) { handTransform = slot_01; }
            //else handTransform = slot_02;
            //return handTransform;

            if (slot_01.childCount == 0) { weaponSlotTransform = slot_01; }
            else weaponSlotTransform = slot_02;
            return weaponSlotTransform;
        }

        public void GetWeaponTypeArray()
        {
            if (weaponType == TypeOfWeapon.SingleShot)
            {
                weaponTypeArray = 1;
            }
            else if (weaponType == TypeOfWeapon.Automatic)
            {
                weaponTypeArray = 2;
            }
            else
            {
                weaponTypeArray = 3;
            }
        }

        public float GetFireRate()
        {
            return fireRate;
        }

        public float GetReloadTime()
        {
            return reloadTime;
        }

        public float GetExplotionArea()
        {
            return explosionArea;
        }

        public int GetWeaponDamage()
        {
            return weaponDamage;
        }

        public int GetMaxAmmoAmount()
        {
            return maxAmmoAmount;
        }

        public int GetMinAmmoAmount()
        {
            return minAmmoAmount;
        }

        public int GetBurstAmount()
        {
            return burstAmmount;
        }
    }
}
