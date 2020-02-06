using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private uint power;
    public uint Power { get { return power; } set { power = value; } }
    [SerializeField]
    private float attackSpeed = 0.5f;
    [SerializeField]
    private Animator animator;

    private bool attackEnabled = true;
    public bool AttackEnabled { get { return attackEnabled; } private set { attackEnabled = value; } }

    public delegate void OnSoulUpdate(Soul soul);
    public event OnSoulUpdate OnSoulUpdateEvent;

    private List<Soul> souls = new List<Soul>();

    private float lastAttack;

    public void Attack()
    {
        StartCoroutine(AttackAnimation());
    }

    public void IntegrateSoul(Soul soul)
    {
        souls.Add(soul);
        animator.SetBool(soul.animatorAttackToggle, true);
        if (OnSoulUpdateEvent != null)
            OnSoulUpdateEvent(soul);
    }

    IEnumerator AttackAnimation()
    {
        lastAttack = Time.time;

        animator.SetTrigger("sword hit");
        
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
        {
            yield return new WaitForEndOfFrame();
        }

        GetComponent<CapsuleCollider>().enabled = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        attackEnabled = false;
        while (lastAttack + attackSpeed > Time.time)
        {
            yield return new WaitForEndOfFrame();
        }

        attackEnabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.ENEMY_TAG)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Power);
                foreach(Soul soul in souls)
                {
                    if (soul is IEffectOnHit)
                        ((IEffectOnHit)soul).EffectOnHit(other);
                }
            }
        }
    }
}
