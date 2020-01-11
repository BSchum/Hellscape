using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddle : MonoBehaviour
{
    public float damage = 3;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Constants.Tags.PLAYER_TAG)
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
