using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Combact
{
    public class ProjectileFireEnemy : MonoBehaviour
    {
        [Tooltip("Index 0 - Ground, Index 1 - Stone, Index 2 - Wood, Index 3 - Metal, Index 4 - Blood")]
        [SerializeField] GameObject[] vfxHitEffects;
        public float fireSpeed;
        public int projectileDamage = 2;

        private Rigidbody rb;
        private int damage;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            damage = projectileDamage / 2;
            rb.velocity = transform.forward * fireSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            var ingroneTag = other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Player");
            if (other.GetComponent<Target>())
            {
                if (other.GetComponent<Target>().targetName == "Stone")
                {
                    Instantiate(vfxHitEffects[1], transform.position, Quaternion.identity);
                }
                else if (other.GetComponent<Target>().targetName == "Wood")
                {
                    Instantiate(vfxHitEffects[2], transform.position, Quaternion.identity);
                }
                else if (other.GetComponent<Target>().targetName == "Metal")
                {
                    Instantiate(vfxHitEffects[3], transform.position, Quaternion.identity);
                }
                else if (other.GetComponent<Target>().targetName == "Blood")
                {
                    Instantiate(vfxHitEffects[4], transform.position, Quaternion.identity);
                }
            }
            else if (!other.GetComponent<Target>() || ingroneTag)
            {
                Instantiate(vfxHitEffects[0], transform.position, Quaternion.identity);
            }

            Health health = other.gameObject.GetComponent<Health>();
            if (health)
            {
                health.TakeDamge(damage);
            }
            Destroy(gameObject);
        }
    }
}
