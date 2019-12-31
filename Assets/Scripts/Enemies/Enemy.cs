using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 100;
    public float damageMultiplier = 1f;
    public int roomNumber;
    public SimpleRoom room;
    public virtual void TakeDamage(int amount)
    {
        health -= amount * damageMultiplier;
        Debug.Log($"{this.name} a subit {amount}, il lui reste {health} PV");

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
