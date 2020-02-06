using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddle : MonoBehaviour
{
    public uint damage;
    public float tickGap;
    float lastTick;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == Constants.Tags.PLAYER_TAG)
        {
            if(lastTick + tickGap < Time.time)
            {
                other.GetComponent<IDamagable>().TakeDamage(damage);
                lastTick = Time.time;
            }
        }
    }
}
