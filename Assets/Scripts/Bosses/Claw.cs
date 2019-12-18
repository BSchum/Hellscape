using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
