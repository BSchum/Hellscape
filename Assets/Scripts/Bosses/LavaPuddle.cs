using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddle : MonoBehaviour
{
    public uint damage;
    public float tickGap;
    float lastTick;
    public float lastTime = 7f;

    private void Start()
    {
        Destroy(gameObject, lastTime);
    }

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
