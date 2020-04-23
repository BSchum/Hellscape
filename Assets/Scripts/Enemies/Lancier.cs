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

    GameObject _target;
    bool isAiming = false;
    bool isRunningAway = false;
    private void Start()
    {
        range = Constants.Rooms.ROOM_SIZE_Y / 2;
        _target = playerContext.player;
        motor.speed = bonusSpeed;
    }

    void Update()
    {
        if (_target != null && playerContext.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            if (!isRunningAway && health > 0)
            {
                motor.Look(_target.transform.position);
                if (!isAiming)
                {
                    StartCoroutine(Aiming());
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (isRunningAway && health > 0 && _target != null)
        {
            motor.Move(transform.position - _target.transform.position);
            motor.LookSmooth(-(_target.transform.position - transform.position), 5f);
        }
    }

    public override void TakeDamage(uint amount)
    {
        base.TakeDamage(amount);
        if (isAiming)
        {
            isAiming = false;
        }
        _animator.SetBool("isFleeing", true);
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
        _animator.SetTrigger("Launch");
    }

    IEnumerator RunAway()
    {
        isRunningAway = true;

        yield return new WaitForSeconds(bonusSpeedDuration);

        isRunningAway = false;
        _animator.SetBool("isFleeing", false);

    }
}
