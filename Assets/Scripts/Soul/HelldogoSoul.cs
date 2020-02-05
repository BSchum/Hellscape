using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelldogoSoul : Soul, IEffectOnHit
{
    public uint damage;
    public float duration;
    public float fireDelay;

    public void EffectOnHit(Collider other)
    {
        StartCoroutine(ApplyDot(other.GetComponent<Enemy>()));
    }

    private IEnumerator ApplyDot(Enemy enemy)
    {
        float startTime = Time.time + duration;

        while (startTime > Time.time)
        {
            if (enemy != null)
                enemy.TakeDamage(damage);
            yield return new WaitForSeconds(fireDelay);
        }
    }
}
