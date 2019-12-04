using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 100;
    public float damageMultiplier = 1f;
    public virtual void TakeDamage(float amount)
    {
        Debug.Log(this.name + " a subit " + amount); 
        if(health > 0)
        {
            health -= amount * damageMultiplier;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
