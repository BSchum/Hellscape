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
    public float bonusSpeedDuration = 4;
    public float bonusSpeed = 2;
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
        target = PlayerContext.instance.player;
    }

    void Update()
    {
        if (target != null && PlayerContext.instance.currentRoomNumber == roomNumber)
        {
            if (!isRunningAway)
            {
                motor.Look(target.transform.position);
                if (!isAiming)
                {
                    StartCoroutine(Aiming());
                }
            }
            else
            {
                motor.speed = hasBonusMooveSpeed ? motor.speed : bonusSpeed;
                motor.Move(transform.position - target.transform.position);
            }
        }
    }

    public override void TakeDamage(int amount)
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
