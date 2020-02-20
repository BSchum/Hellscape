using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bouboule : Enemy
{
    [Header("Charge")]
    public float chargeCastTime;
    public float chargeDuration;
    public uint damage = 1;
    public float repulseForce = 10;

    bool _isCharging;
    GameObject _target;
    Motor _motor;
    Rigidbody _rb;
    Animator _animator;

    private void Start()
    {
        _target = PlayerContext.instance.player;
        _animator = GetComponent<Animator>();
        _motor = GetComponent<Motor>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (_target != null && PlayerContext.instance.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            if (!_isCharging)
            {
                StopAllCoroutines();
                StartCoroutine(Charging());
            }
        }
    }

    private IEnumerator Charging()
    {
        _isCharging = true;
        _motor.Look(_target.transform.position);
        _animator.SetBool("isCharging", true);
        yield return new WaitForSeconds(chargeCastTime);
        _animator.SetBool("isCharging", false);
        var dir = _target.transform.position - transform.position;
        _rb.AddForce(dir.normalized * _motor.speed);

        yield return new WaitForSeconds(chargeDuration);
        _rb.AddForce(-(dir.normalized * _motor.speed) / 2);
        _isCharging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);

            collision.rigidbody.AddForce(collision.transform.forward * -1 * repulseForce);
        }
    }
}
