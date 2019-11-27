using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SDG.Unity.Scripts;

public class Lancier : Enemy, IDamagable
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float aimDelay;
    public float damageTakenSpeed = 2;
    public float bonusSpeedDuration = 2;
    public float range;
    public Motor motor;

    GameObject target;
    bool isAiming = false;
    bool isRunningAway = false;
    bool hasBonusMooveSpeed = false;
    Animator animator;

    private void Start()
    {
        range = Constants.Rooms.ROOM_SIZE_Y / 2;
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, range).Where(c => c.tag == Constants.Tags.PLAYER_TAG);            
            if (targets.Count() > 0)
                this.target = targets.FirstOrDefault().gameObject;
        }
        else if (target != null)
        {
            if (!isRunningAway)
            {
                motor.Look(target.transform.position);
                if (!isAiming)
                {
                    StartCoroutine(Aiming());
                }

                // player facing this + magnitude < ?? => run
            }
            else
            {
                motor.speed = hasBonusMooveSpeed ? speed : bonusSpeedDuration;
                motor.Move(transform.position - target.transform.position);
            }
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (isAiming)
        {
            isAiming = false;
        }

        StopAllCoroutines();
        StartCoroutine(ObtainBonusMooveSpeed());
        StartCoroutine(RunAway());
    }

    void Shoot()
    {
        Instantiate(projectile, projectileSpawn.position, transform.rotation);
    }

    IEnumerator Aiming()
    {
        isAiming = true;

        yield return new WaitForSeconds(aimDelay);

        isAiming = false;
        animator.SetTrigger("aim");
    }

    IEnumerator RunAway()
    {
        isRunningAway = true;

        yield return new WaitForSeconds(0.5f);

        isRunningAway = false;
    }

    IEnumerator ObtainBonusMooveSpeed()
    {
        hasBonusMooveSpeed = true;

        yield return new WaitForSeconds(bonusSpeedDuration);

        hasBonusMooveSpeed = false;
    }
}
