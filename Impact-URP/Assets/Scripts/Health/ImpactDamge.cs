using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDamge : MonoBehaviour
{
    private int damage;


    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Head"))
        //{
        //    damage = BulletDamage.instance.bulletDamage + ((BulletDamage.instance.bulletDamage * 80) / 100);
        //    SuperHealthScript.instance.health(damage);
        //}
        //else if (collision.gameObject.CompareTag("Body"))
        //{
        //    damage = BulletDamage.instance.bulletDamage + ((BulletDamage.instance.bulletDamage * 40) / 100);
        //    SuperHealthScript.instance.health(damage);
        //}
        //else if (collision.gameObject.CompareTag("Arms"))
        //{
        //    damage = BulletDamage.instance.bulletDamage - ((BulletDamage.instance.bulletDamage * 50) / 100);
        //    SuperHealthScript.instance.health(damage);
        //}
        //else if (collision.gameObject.CompareTag("Legs"))
        //{
        //    damage = BulletDamage.instance.bulletDamage + ((BulletDamage.instance.bulletDamage * 10) / 100);
        //    SuperHealthScript.instance.health(damage);
        //}
    }
}
