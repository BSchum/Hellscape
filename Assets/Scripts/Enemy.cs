using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 100;
    public void TakeDamage(float amount)
    {
        Debug.Log(this + " a subit " + amount); 
        if(health > 0)
        {
            health -= amount;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
