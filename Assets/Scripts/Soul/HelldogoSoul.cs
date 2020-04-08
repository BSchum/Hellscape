using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HellDogoSoul", menuName = "Soul/HellDogo")]
public class HelldogoSoul : Soul, IEffectOnHit
{
    public uint damage;
    public float duration;
    public float fireDelay;

    public IEnumerator EffectOnHit(Collider other)
    {
        return ApplyDot(other.GetComponent<Enemy>());
    }

    private IEnumerator ApplyDot(Enemy enemy)
    {
        float endTime = Time.time + duration;

        while (endTime > Time.time)
        {
            if (enemy != null)
                enemy.TakeDamage(damage);

            yield return new WaitForSeconds(fireDelay);
        }
    }
}
