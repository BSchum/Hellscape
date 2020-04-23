using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bouboule : Enemy
{
    [Header("Charge")]
    public float chargeCastTime;
    public float chargeForce = 10;
    public float repulseForce = 10;
    public uint damage = 1;
    public float chargeCooldown;
    public uint chargeDamage;
    public float chargeDuration;
    float _lastCharge;
    bool _isCharging;

    GameObject _target;
    Motor _motor;
    Rigidbody _rb;

    private void Start()
    {
        _target = playerContext.player;
        _motor = GetComponent<Motor>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(health > 0 && _target != null)
            _motor.LookSmooth(_target.transform, 10f);

        if (_target != null && playerContext.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            if (!_isCharging && _lastCharge + chargeCooldown < Time.time)
            {
                _lastCharge = Time.time;     
                StartCoroutine(Charging());
            }
        }
    }

    private IEnumerator Charging()
    {
        _isCharging = true;
        _animator.SetTrigger("Curl");
        yield return new WaitForSeconds(chargeCastTime);
        _rb.AddForce(transform.forward * chargeForce);
        yield return new WaitForSeconds(chargeDuration);
        _animator.SetTrigger("UnCurl");
        _rb.velocity = Vector3.zero;
        _isCharging = false;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.PLAYER_TAG && _rb.velocity.magnitude > 1)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);

            collision.rigidbody.AddForce(collision.transform.forward * -1 * repulseForce);
        }
    }
}
