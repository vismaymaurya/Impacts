﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int bulletDamage;

    public static BulletDamage instance;

    private void Awake()
    {
        instance = this;
    }
}
