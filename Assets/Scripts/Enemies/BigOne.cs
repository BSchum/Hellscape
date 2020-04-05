using SDG.Platform.Entities;
using SDG.Unity.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Motor), typeof(Animator))]
public class BigOne : Enemy
{
    [Header("Smash Attack")]
    public float attackRange = 5.0f;
    public float chargingTime = 1f;
    public float smashAttackCooldown = 3f;
    public Projector projector;

    [Header("CircularAttack")]
    public float circularAttackTime = 3f;
    public float circularAttackCooldown = 6f;

    [Header("Movement")]
    public float changeDirectionCooldown = 3f;

    bool _isRotating = false;
    bool _isCharging = false;

    float _lastCircularAttack = 0.0f;
    float _lastSmashAttack = 0.0f;
    float _lastDirectionChange = 0.0f;
    
    List<Direction> directions;
    Vector3 currentDirection;

    Motor _motor;
    Animator _animator;
    GameObject _target;


    private void Start()
    {
        _target = playerContext.player;
        _animator = GetComponent<Animator>();
        _motor = GetComponent<Motor>();
        directions = Enum.GetValues(typeof(Direction)).OfType<Direction>().ToList();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (_target != null && playerContext.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            var toTarget = (_target.transform.position - transform.position).normalized;
            if (!_isCharging)
            {
                Move();
            }

            if (Vector3.Distance(_target.transform.position, this.transform.position) < attackRange)
            {
                var hitPos = Vector3.Dot(toTarget, transform.forward);

                //Si devant le joueur
                if (hitPos > 0 && !_isRotating && _lastSmashAttack + smashAttackCooldown < Time.time)
                {
                    //On se prepare a taper
                    damageMultiplier = 1f;
                    StartCoroutine(SmashAttack());
                }
                // Si derriere le joueur
                if (hitPos < 0 && !_isRotating && _lastCircularAttack + circularAttackCooldown < Time.time )
                {
                    //On fait tourner et on suis
                    damageMultiplier = 2f;
                    StartCoroutine(CircularAttack());
                }
            }

        }
    }
    void Move()
    {
        //Move randomly in the platform, change direction every 2seconds or if a wall/an enemy is on the way
        var ray = new Ray(this.transform.position, currentDirection);

        if (!_isRotating)
        {
            if (Time.time >= changeDirectionCooldown + _lastDirectionChange || Physics.Raycast(ray, 5))
            {
                ChangeDirection();
                _lastDirectionChange = Time.time;
            }
            _motor.Move(currentDirection);
        }
        else
        {
            _motor.Move((_target.transform.position - this.transform.position).normalized);
        }
    }
    void ChangeDirection()
    {
        var index = UnityEngine.Random.Range(0, directions.Count - 1);
        var randomDir = directions[index];
        switch (randomDir)
        {
            case Direction.Bottom:
                currentDirection = Vector3.back;
                break;
            case Direction.Top:
                currentDirection = Vector3.forward;
                break;
            case Direction.Left:
                currentDirection = Vector3.left;
                break;
            case Direction.Right:
                currentDirection = Vector3.right;
                break;
            default:
                currentDirection = Vector3.right;
                break;
        }
        this.transform.rotation = Quaternion.LookRotation(currentDirection);
    }
    IEnumerator SmashAttack()
    {
        _isCharging = true;
        _animator.SetTrigger("Charge");
        _lastSmashAttack = Time.time;
        projector.enabled = true;
        yield return new WaitForSeconds(chargingTime);

        _isCharging = false;
        _animator.SetTrigger("Attack");
        projector.enabled = false;
        var targets = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
        foreach(var target in targets)
        {
            target.GetComponent<Rigidbody>().AddExplosionForce(1000f,this.transform.position, attackRange, 5f);
        }
    }
    IEnumerator CircularAttack()
    {
        _isRotating = true;
        _lastCircularAttack = Time.time;
        _animator.SetBool("isRotating", true);
        yield return new WaitForSeconds(circularAttackTime);
        _isRotating = false;
        _animator.SetBool("isRotating", false);

    }
}
