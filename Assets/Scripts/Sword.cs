﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                Debug.Log($"J'inflige des degats a {other.name} {Time.deltaTime}");
                damagable.TakeDamage(damage);
            }
        }
    }
}
