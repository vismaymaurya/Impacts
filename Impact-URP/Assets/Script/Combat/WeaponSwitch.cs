using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Combact
{
    public class WeaponSwitch : MonoBehaviour
    {
        public int selectedWeapon = 0;
        private StarterAssetsInputs _input;

        private void Start()
        {
            SelectedWeapon();
            _input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            UpdateSwitch();
        }

        public void UpdateSwitch()
        {
            int previousSelectedWeapon = selectedWeapon;

            if (_input.scroll > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (_input.scroll < 0f)
            {
                if (selectedWeapon <= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon--;
            }

            if (_input.alpha01)
            {
                selectedWeapon = 0;
            }

            if (_input.alpha02 && transform.childCount >= 1)
            {
                selectedWeapon = 1;
            }

            if (previousSelectedWeapon != selectedWeapon) { SelectedWeapon(); }
        }

        private void SelectedWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                    SwitchActiveWeapon(i);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }

        private void SwitchActiveWeapon(int i)
        {
            var activeWeapon = transform.GetChild(i).GetComponentInChildren<Weapon>();
            if (activeWeapon != null)
            {
                WeaponSetup.instance.SwitchWeapon(activeWeapon.weaponConfig);
            }
            else
            {
                WeaponSetup.instance.SetDefault();
            }
        }
    }
}
