using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CONSTANTS.TAGS.ENEMY_TAG)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.takeDamage(damage);
            }
        }
    }
}
