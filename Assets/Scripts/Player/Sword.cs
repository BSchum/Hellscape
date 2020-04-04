using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Stats stats;
    public Animator playerAnimator;

    public delegate void OnSoulUpdate(Soul soul);
    public event OnSoulUpdate OnSoulUpdateEvent;

    private List<Soul> souls = new List<Soul>();

    public void IntegrateSoul(Soul soul)
    {
        souls.Add(soul);
        
        playerAnimator.SetTrigger("IntegrateSoul");
        Destroy(soul.gameObject);
        OnSoulUpdateEvent?.Invoke(soul);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                foreach (Soul soul in souls)
                {
                    if (soul is IEffectOnHit)
                        ((IEffectOnHit)soul).EffectOnHit(other);
                }
                damagable.TakeDamage(stats.Power);

            }
        }
    }
}
