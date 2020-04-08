using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public uint power;
    public Animator playerAnimator;

    public delegate void OnSoulUpdate(Soul soul);
    public event OnSoulUpdate OnSoulUpdateEvent;

    public List<Soul> souls = new List<Soul>();

    public void IntegrateSoul(Soul soul)
    {
        souls.Add(soul);
        if (soul.animatorAttackToggle != "")
        {
            playerAnimator.SetBool(soul.animatorAttackToggle, true);
        }
        if (OnSoulUpdateEvent != null)
            OnSoulUpdateEvent(soul);

        playerAnimator.Play("GrabSoul");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(power);
                foreach (Soul soul in souls)
                {
                    if (soul is IEffectOnHit)
                        StartCoroutine(((IEffectOnHit)soul).EffectOnHit(other));
                }
            }
        }
    }
}
