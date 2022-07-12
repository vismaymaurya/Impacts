using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Combact
{
    public class WeaponDrop : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        private void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
        }

        private void Update()
        {
            DropWeapon();
        }

        private void FixedUpdate()
        {
            AutomaticDrop();
        }

        private void AutomaticDrop()
        {
            Animator animator = GetComponent<Animator>();
            if (transform.childCount > 1)
            {
                StartCoroutine(DelayUpdate(.05f));
            }
        }

        private void DropWeapon()
        {
            if (_input.drop)
            {
                ActiveWeapon();
            }
        }

        private void ActiveWeapon()
        {
            var activeWeapon = GetComponentInChildren<Weapon>();
            if (activeWeapon != null && gameObject.activeInHierarchy)
            {
                UpdateWeapon(activeWeapon);
                WeaponSetup.instance.SetDefault();
            }
        }

        private void UpdateWeapon(Weapon activeWeapon)
        {
            var dropWeapon = Instantiate(activeWeapon.weaponConfig.weaponPickup);
            Vector3 position = gameObject.GetComponentInParent<WeaponSetup>().gameObject.transform.position +
                new Vector3(Random.Range(.1f, 1f), .5f, Random.Range(.1f, 1f));
            dropWeapon.transform.position = position;

            activeWeapon.name = "Destroying";
            Destroy(activeWeapon.gameObject);
        }

        private void UpdateAnimation()
        {
            Transform weapon = transform.Find("Weapon");
            if (weapon != null)
            {
                WeaponSetup.instance.SwitchWeapon(weapon.gameObject.GetComponent<Weapon>().weaponConfig);
            }
        }

        private void FindChildWeapon()
        {
            var activeWeapon = transform.GetChild(0).GetComponent<Weapon>();
            UpdateWeapon(activeWeapon);
        }

        private IEnumerator DelayUpdate(float seconds)
        {
            FindChildWeapon();
            yield return new WaitForSeconds(seconds);
            UpdateAnimation();
        }
    }
}