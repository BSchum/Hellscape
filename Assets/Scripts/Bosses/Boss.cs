using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{
    public float health;
    public Motor motor;
    public GameObject soul;

    public void TakeDamage(uint amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(soul, transform.position, soul.transform.rotation);
            Destroy(gameObject);
        }
    }
}
