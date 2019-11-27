﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Projectile
{
    public float lifetime;
    public float damage;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        motor.Move(transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spear " + other.name);

        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
