using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Combact
{
    public class ProjectileFire : MonoBehaviour
    {
        [Tooltip("Index 0 - Ground, Index 1 - Stone, Index 2 - Wood, Index 3 - Metal, Index 4 - Blood")]
        [SerializeField] GameObject[] vfxHitEffects;
        public float fireSpeed;
        public int projectileDamage = 2;

        private Rigidbody rb;
        private GameObject player;
        private int damage;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            damage = projectileDamage + player.GetComponent<WeaponSetup>().currentWeaponConfig.GetWeaponDamage();
            rb.velocity = transform.forward * fireSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ingroneTag = collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Player");
            if (collision.gameObject.GetComponent<Target>())
            {
                if (collision.gameObject.GetComponent<Target>().targetName == "Stone")
                {
                    Instantiate(vfxHitEffects[1], transform.position, Quaternion.identity);
                }
                else if (collision.gameObject.GetComponent<Target>().targetName == "Wood")
                {
                    Instantiate(vfxHitEffects[2], transform.position, Quaternion.identity);
                }
                else if (collision.gameObject.GetComponent<Target>().targetName == "Metal")
                {
                    Instantiate(vfxHitEffects[3], transform.position, Quaternion.identity);
                }
                else if (collision.gameObject.GetComponent<Target>().targetName == "Blood")
                {
                    Instantiate(vfxHitEffects[4], transform.position, Quaternion.identity);
                }
            }
            else if (!collision.gameObject.GetComponent<Target>() || !ingroneTag)
            {
                Instantiate(vfxHitEffects[0], transform.position, Quaternion.identity);
            }

            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamge(damage);
            }
            Destroy(gameObject);
        }
    }
}
