﻿using System.Collections;
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

    float _randomDelay;
    private void Start()
    {
        _randomDelay = Random.Range(Time.time, Time.time + 5.00f);
        range = Constants.Rooms.ROOM_SIZE_Y / 2;
        _target = playerContext.player;
        motor.speed = bonusSpeed;
    }

    void Update()
    {
        if (Time.time < _randomDelay)
            return;
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
        if (Time.time < _randomDelay)
            return;

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
        StartCoroutine(RunAway());
    }

    void Shoot()
    {
        var go  = Instantiate(projectile, projectileSpawn.position, transform.rotation);
        go.GetComponent<Spear>().damage *= (uint)damageMultiplier;
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
        _animator.SetBool("isFleeing", true);
        isRunningAway = true;
        yield return new WaitForSeconds(bonusSpeedDuration);
        isRunningAway = false;
        _animator.SetBool("isFleeing", false);
    }
}
