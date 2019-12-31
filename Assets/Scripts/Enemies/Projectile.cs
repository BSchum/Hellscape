using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motor))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Motor motor;
}
