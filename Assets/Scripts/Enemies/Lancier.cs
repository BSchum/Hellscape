using System.Collections;
using UnityEngine;
using SDG.Unity.Scripts;

public class Lancier : Enemy, IDamagable
{
    public GameObject projectile;
    public Transform projectileSpawn;
    public float aimDelay;
    public float bonusSpeedDuration = 4;
    public int bonusSpeed = 2;
    public float range;
    public Motor motor;

    GameObject target;
    bool isAiming = false;
    bool isRunningAway = false;
    Animator animator;

    private void Start()
    {
        range = Constants.Rooms.ROOM_SIZE_Y / 2;
        animator = this.GetComponent<Animator>();
        target = PlayerContext.instance.player;
        motor.speed = bonusSpeed;
    }

    void Update()
    {
        if (target != null && PlayerContext.instance.currentRoomNumber == roomNumber && room.doorsClosed)
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
                motor.Move(transform.position - target.transform.position);
            }
        }
    }

    public override void TakeDamage(uint amount)
    {
        base.TakeDamage(amount);
        if (isAiming)
        {
            isAiming = false;
        }

        StopAllCoroutines();
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

        yield return new WaitForSeconds(bonusSpeedDuration);

        isRunningAway = false;
    }
}
