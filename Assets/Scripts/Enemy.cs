using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Enemy : MonoBehaviour, IDamagable
{
    public void TakeDamage(float amount)
    {
        Debug.Log(this + " a subit " + amount); 
    }
}
