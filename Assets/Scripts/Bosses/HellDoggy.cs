using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HellDoggy : Boss, IDamagable
{
    Rigidbody rb;
    [Header("Comportement du boss")]
    public float aggroRange;
    public float movementRange;
    public float rotationSpeed = 2;
    public bool canMove;

    [HideInInspector]
    public bool isChained = false;

    [Header("Coup de griffe")]
    public float clawStrikeCooldown;
    float _lastClawStrike;
    public float clawStrikeCastTime;
    public BoxCollider clawCollider;

    [Header("Coulée de lave")]
    public GameObject lavaPuddlePrefab;
    public Transform lavaPuddleAnchor;
    public float lavaPuddleCastTime;

    [Header("Charge")]
    public float chargeCastTime;
    public float chargeCooldown;
    public float chargeForce;
    public uint chargeDamage;
    public float chargeDuration;
    float _lastCharge;
    bool _isCharging;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _lastClawStrike = 0;
        _lastCharge = chargeCooldown;
        motor.speed = stats.Speed;
    }
    private void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, aggroRange).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
            if (targets.Count() > 0)
                this.target = targets.FirstOrDefault().gameObject;
        }
        else
        {
            //Follow the player
            if (canMove && !isChained)
            {
                motor.LookSmooth(target.transform, rotationSpeed);

                if((target.transform.position - this.transform.position).magnitude > movementRange)
                {
                    animator.SetFloat("CurrentMoveSpeed", 1f, 1f, Time.time);
                }
                else
                {
                    animator.SetFloat("CurrentMoveSpeed", 0f, 1f, Time.time);
                }

                //Coup de griffe
                if (_lastClawStrike + clawStrikeCooldown < Time.time && (target.transform.position - this.transform.position).magnitude < movementRange && !_isCharging)
                {
                    _lastClawStrike = Time.time;
                    StartCoroutine(ClawStrike());
                }


                if (_lastCharge + chargeCooldown < Time.time)
                {
                    _lastCharge = Time.time;
                    StartCoroutine(Charge());
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (target != null && canMove && !isChained)
            motor.Move(target.transform, movementRange);
    }
    #region Charge
    public IEnumerator Charge()
    {
        _isCharging = true;
        canMove = false;
        animator.SetTrigger("CastCharge");
        yield return new WaitForSeconds(chargeCastTime);
        animator.SetBool("isCharging", true);
        rb.AddForce(transform.forward * chargeForce);
        yield return new WaitForSeconds(chargeDuration);
        animator.SetBool("isCharging", false);
        rb.velocity = Vector3.zero;
        canMove = true;
        _isCharging = false;

        StartCoroutine(CastLavaPuddle());
    }
    #endregion
    #region LavaPuddle
    public IEnumerator CastLavaPuddle()
    {
        yield return new WaitUntil(IsFacingPlayer);
        canMove = false;
        animator.SetTrigger("IsCastingLavaPuddle");
        yield return new WaitForSeconds(lavaPuddleCastTime);
        animator.SetTrigger("InvokeLavaPuddle");
    }

    bool IsFacingPlayer()
    {
        RaycastHit rHit;
        Physics.Raycast(new Ray(this.transform.position, transform.forward), out rHit, 300);
        if (rHit.transform != null)
        {
            return rHit.transform.tag == Constants.Tags.PLAYER_TAG;
        }
        else
            return false;

    }

    public void InvokeLavaPuddle()
    {
        Debug.Log("LavaPuddleInvoke");
        var rot = lavaPuddleAnchor.transform.rotation;
        rot.x = 0;
        rot.z = 0;
        Instantiate(lavaPuddlePrefab, lavaPuddleAnchor.position, rot);
        canMove = true;
    }
    #endregion
    #region ClawStrike
    public IEnumerator ClawStrike()
    {
        canMove = false;
        animator.SetTrigger("IsCastingClaw");
        yield return new WaitForSeconds(clawStrikeCastTime);
        animator.SetTrigger("IsClawing");
    }
    #endregion
    public void ActivateClawCollider()
    {
        clawCollider.enabled = true;
    }
    public void DesactivateClawCollider()
    {
        clawCollider.enabled = false;
    }
    /// <summary>
    /// Called by animation
    /// </summary>
    public void CanMove()
    {
        Debug.Log("Yo, je bouge via l'anim tmtc");
        canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == Constants.Tags.PLAYER_TAG && _isCharging)
        {
            collision.transform.GetComponent<IDamagable>().TakeDamage(chargeDamage);
        }
    }
}

