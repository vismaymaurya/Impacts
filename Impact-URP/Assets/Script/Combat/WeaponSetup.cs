using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Imapct.Combact
{
    public class WeaponSetup : MonoBehaviour
    {
        [SerializeField] Transform weaponSlot_01Transform = null;
        [SerializeField] Transform weaponSlot_02Transform = null;
        [SerializeField] WeaponConfig defaultWeapon = null;
        [SerializeField] Rig rig;
        [Header("Weapon Holder Slots")]
        [SerializeField] GameObject weaponSlot_01;
        [SerializeField] GameObject weaponSlot_02;

        [HideInInspector]
        public WeaponConfig currentWeaponConfig = null;
        private Weapon currentWeapon;
        private static WeaponSetup s_Instance;
        private StarterAssetsInputs _input;
        private Animator animator;

        [HideInInspector]
        public bool pickup;
        [HideInInspector]
        public bool isFire = false;

        private float timer = 0;
        public float timeTochangeAnim = 4f;

        public static WeaponSetup instance
        {
            get
            {
                return s_Instance;
            }
        }

        private void Awake()
        {
            s_Instance = this;
            _input = GetComponent<StarterAssetsInputs>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            animator.SetLayerWeight(1, 0);
            rig.weight = 0;
            if (currentWeaponConfig == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            pickup = _input.pickup;
            if (_input.pickup)
            {
                _input.pickup = false;
            }
            ChangeAnimIdleToFire();
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon = AttachWeapon(weapon);
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            return weapon.Spawn(weaponSlot_01Transform, weaponSlot_02Transform);
        }

        public void UpdateWeapon(WeaponConfig weapon)
        {
            EquipWeapon(weapon);
        }

        public void SwitchWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.SwapAnimation(animator);
        }

        public void SetDefault()
        {
            Animator animator = GetComponent<Animator>();
            defaultWeapon.SwapAnimation(animator);
        }

        public WeaponConfig GetWeapoonConfig()
        {
            return currentWeaponConfig;
        }

        private void ChangeAnimIdleToFire()
        {
            timer += Time.deltaTime;
            var Slotcheck = weaponSlot_01.transform.childCount > 0 || weaponSlot_02.transform.childCount > 0;
            if (Slotcheck)
            {
                if (_input.mouse1)
                {
                    timer = 0;
                    isFire = true;
                }
            }
            

            if (timer >= timeTochangeAnim)
            {
                isFire = false;
            }

            if (isFire || _input.fire2)
            {
                if (Slotcheck)
                {
                    animator.SetLayerWeight(1, 1);
                    rig.weight = 1;
                }
            }
            else
            {
                animator.SetLayerWeight(1, 0);
                rig.weight = 0;
            }

            if (currentWeaponConfig.weaponTypeArray == 1)
            {
                if (_input.mouse1)
                {
                    animator.SetTrigger("SingleShot");
                }
            }
        }
    }
}
