using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Projectile
{
    public float lifetime;
    public uint damage;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        motor.Move(transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
