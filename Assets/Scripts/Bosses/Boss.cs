using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour
{
    [Header("Comportement du boss")]
    public Stats stats;
    public Motor motor;
    [HideInInspector]
    public GameObject target;
    public Animator animator;
    private void Start()
    {
        motor.speed = stats.Speed;
        animator = GetComponent<Animator>();
    }
}
