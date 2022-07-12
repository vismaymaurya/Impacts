using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imapct.Ctrl;

namespace Imapct.Combact
{
    public class WeaponFire : MonoBehaviour
    {
        [SerializeField] Transform firePoint;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] GameObject gameObjectLight;

        private GameObject projectile;
        private float reloadTime;
        private int maxAmmoAmount;
        private int minAmmoAmount;
        private int burstAmmount;
        private float timeBtwShots;
        private float timeBtwBurst;
        private float startTimeBtwShots;
        private int currentAmmo;

        private StarterAssetsInputs _input;
        private WeaponConfig weaponConfig;
        private GameObject player;

        private void Start()
        {
            _input = GetComponentInParent<StarterAssetsInputs>();
            weaponConfig = transform.GetComponent<Weapon>().weaponConfig;
            reloadTime = weaponConfig.GetReloadTime();
            maxAmmoAmount = weaponConfig.GetMaxAmmoAmount();
            minAmmoAmount = weaponConfig.GetMinAmmoAmount();
            burstAmmount = weaponConfig.GetBurstAmount();
            startTimeBtwShots = weaponConfig.GetFireRate();
            timeBtwShots = startTimeBtwShots;
            timeBtwBurst = burstAmmount;
            currentAmmo = minAmmoAmount;

            player = GameObject.FindGameObjectWithTag("Player");
            //gameObjectLight.SetActive(false);
        }

        private void Update()
        {
            weaponConfig.GetWeaponTypeArray();
            WeaponTypeSelection();
        }

        private void WeaponTypeSelection()
        {
            var WeaponType = weaponConfig.weaponTypeArray;
            if (WeaponType == 1)
            {
                SingleShot();
            }
            else if (WeaponType == 2)
            {
                AutomaticShot();
            }
            else
            {
                BurstShot();
            }
        }

        private void Fire()
        {
            muzzleFlash.Play();
            //gameObjectLight.SetActive(_input.mouse1);
            currentAmmo--;

            var shooterCtrl = player.GetComponent<ShooterCtrl>();
            projectile = weaponConfig.projectile.gameObject;
            shooterCtrl.Fire(projectile, firePoint);
        }

        private void SingleShot()
        {
            if (_input.mouse1)
            {
                Fire();
                _input.mouse1 = false;
            }
        }

        private void AutomaticShot()
        {
            timeBtwShots += Time.deltaTime;
            if (_input.mouse1)
            {
                if (timeBtwShots >= startTimeBtwShots)
                {
                    StartCoroutine(WaitForFire(.02f));
                    timeBtwShots = 0;
                }
            }
        }

        private void BurstShot()
        {
            if (_input.mouse1)
            {
                if (timeBtwBurst >= (burstAmmount / 5))
                {
                    Fire();
                    Invoke("Fire", burstAmmount / 40);
                    Invoke("Fire", burstAmmount / 20);

                    timeBtwBurst = 0;
                }
                else
                {
                    timeBtwBurst += Time.deltaTime;
                }
                _input.mouse1 = false;
            }
        }

        private IEnumerator WaitForFire(float second)
        {
            yield return new WaitForSeconds(second);
            Fire();
        }
    }
}
