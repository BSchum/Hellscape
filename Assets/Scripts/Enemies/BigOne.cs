using SDG.Platform.Entities;
using SDG.Unity.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BigOne : Enemy
{

    float attackRange = 5.0f;
    float chargingTime = 1f;
    float circularAttackTime = 3f;
    public Projector projector;
    GameObject target;
    bool _isRotating = false;
    bool _isCharging = false;

    float lastCircularAttack = 0.0f;
    float circularAttackCooldown = 6f;

    float lastSmashAttack = 0.0f;
    float smashAttackCooldown = 3f;
    [SerializeField]
    Motor motor;
    List<Direction> directions;
    Animator animator;
    public float changeDirectionCooldown = 3f;
    float lastDirectionChange = 0.0f;

    Vector3 currentDirection;
    private void Start()
    {
        target = PlayerContext.instance.player;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (target != null && PlayerContext.instance.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            var toTarget = (target.transform.position - transform.position).normalized;
            if (!_isCharging)
            {
                //Move randomly in the platform, change direction every 2seconds or if a wall/an enemy is on the way
                var ray = new Ray(this.transform.position, currentDirection);

                if (!_isRotating)
                {
                    if (Time.time >= changeDirectionCooldown + lastDirectionChange || Physics.Raycast(ray, 5))
                    {
                        ChangeDirection();
                        lastDirectionChange = Time.time;
                    }
                    motor.Move(currentDirection);
                }
                else
                {
                    motor.Move((target.transform.position - this.transform.position).normalized);
                }

                
            }

            if (Vector3.Distance(target.transform.position, this.transform.position) < attackRange)
            {
                var hitPos = Vector3.Dot(toTarget, transform.forward);

                //Si devant le joueur
                if (hitPos > 0 && !_isRotating && lastSmashAttack + smashAttackCooldown < Time.time)
                {
                    //On se prepare a taper
                    lastSmashAttack = Time.time;
                    projector.enabled = true;
                    damageMultiplier = 1f;
                    StartCoroutine(SmashAttack());
                }
                // Si derriere le joueur
                if (hitPos < 0 && !_isRotating && lastCircularAttack + circularAttackCooldown < Time.time )
                {
                    //On fait tourner et on suis
                    damageMultiplier = 2f;
                    _isRotating = true;
                    lastCircularAttack = Time.time;
                    StartCoroutine(CircularAttack());
                }
            }

        }
    }
    void ChangeDirection()
    {
        directions = Enum.GetValues(typeof(Direction)).OfType<Direction>().ToList();
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
        animator.SetTrigger("Charge");
        yield return new WaitForSeconds(chargingTime);
        _isCharging = false;
        animator.SetTrigger("Attack");
        projector.enabled = false;
        var targets = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
        foreach(var target in targets)
        {
            target.GetComponent<Rigidbody>().AddExplosionForce(1000f,this.transform.position, attackRange, 5f);
        }
    }

    IEnumerator CircularAttack()
    {
        animator.SetBool("isRotating", true);
        yield return new WaitForSeconds(circularAttackTime);
        _isRotating = false;
        animator.SetBool("isRotating", false);

    }
}
