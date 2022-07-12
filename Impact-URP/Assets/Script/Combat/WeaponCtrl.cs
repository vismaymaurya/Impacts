using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private Transform currentTransfor;

    private void Start()
    {
        currentTransfor.rotation = transform.rotation;
        _input = GetComponentInParent<StarterAssetsInputs>();
    }

    private void FixedUpdate()
    {
        //if (_input.fire1)
        //{
        //    WeaponRotor();
        //}
        //else
        //{
        //    transform.rotation = currentTransfor.rotation;
        //}
        WeaponRotor();
    }

    private void WeaponRotor()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(_input.look) - transform.position;
        float angle = Mathf.Atan2(dir.y ,dir.z) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
