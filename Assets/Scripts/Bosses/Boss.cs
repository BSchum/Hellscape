using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour, IDamagable
{
    [Header("Comportement du boss")]
    public Stats stats;
    public Motor motor;
    [HideInInspector]
    public GameObject target;
    public Animator animator;
    public GameObject soul;

    private void Start()
    {
        motor.speed = stats.Speed;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(uint amount)
    {
        stats.TakeDamage(amount);
        if (stats.Health == 0)
        {
            Instantiate(soul, transform.position, soul.transform.rotation);
            
            Destroy(this.gameObject);
        }
    }
}
