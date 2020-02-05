using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HellDoggo : Boss
{
    public Rigidbody rb;
    public Animator animator;
    public float range;

    public Claw claw;
    public float clawRange = 10;
    public float clawCastTime = 1.5f;

    public float chargeCastTime = 2;
    public uint chargeDamage = 2;
    public float chargeForce = 100;
    public float repulseForce = 50;
    public float chargeCooldown = 7;

    public GameObject lavaPuddlePrefab;
    public float lavaPuddleLifeTime = 7;
    public float lavaSpittingTime = 7;
    public float lavaSpawingDelay = 2;
    public float lavaSpittingCooldown = 14;

    GameObject target;
    bool isAbleToMove = true;
    bool isClawing = false;
    bool isCharging = false;
    bool chargeIsOnCooldown;
    bool isSpittingLava;

    [HideInInspector]
    public bool isChained = false;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpittingLava());
        TakeDamage((uint)health);
    }

    void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, range).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
            if (targets.Count() > 0)
                this.target = targets.FirstOrDefault().gameObject;
        }
        else if (target != null && !isChained)
        {
            if (isAbleToMove)
            {
                motor.Look(target.transform.position);
                motor.Move(target.transform.position - transform.position);
            }

            if (Vector3.Distance(target.transform.position, transform.position) < clawRange && !isClawing && !isCharging)
            {
                StartCoroutine(ClawHit());
            }

            if (!chargeIsOnCooldown && !isCharging && !isClawing)
            {
                StartCoroutine(ChargeForward());
            }
        }
    }
    public void FinishClawing()
    {
        isAbleToMove = true;
        isClawing = true;
    }
    IEnumerator ClawHit()
    {
        isClawing = true;
        isAbleToMove = false;
        animator.SetTrigger("IsCastingClaw");
        yield return new WaitForSeconds(clawCastTime);

        isClawing = false;

        animator.SetTrigger("IsClawing");
    }

    IEnumerator ChargeForward()
    {
        isAbleToMove = false;
        isCharging = true;

        rb.AddForce(transform.forward * chargeForce);

        yield return new WaitForSeconds(chargeCastTime);

        isCharging = false;
        isAbleToMove = true;

        chargeIsOnCooldown = true;

        yield return new WaitForSeconds(chargeCooldown);

        chargeIsOnCooldown = false;
    }

    IEnumerator SpittingLava()
    {
        isSpittingLava = true;

        var startTime = Time.time + lavaSpittingTime;
        var lavaSpawnTime = Time.time + lavaSpawingDelay;

        while (startTime > Time.time)
        {
            if (lavaSpawnTime <= Time.time)
            {
                Destroy(Instantiate(lavaPuddlePrefab, transform.position, lavaPuddlePrefab.transform.rotation), lavaPuddleLifeTime);
                lavaSpawnTime = Time.time + lavaSpawingDelay;
            }

            yield return new WaitForEndOfFrame();
        }

        isSpittingLava = false;

        yield return new WaitForSeconds(lavaSpittingCooldown);

        StartCoroutine(SpittingLava());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCharging)
        {
            if (collision.gameObject.tag == Constants.Tags.PLAYER_TAG)
            {
                collision.transform.GetComponent<Player>().TakeDamage(chargeDamage);
                collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * repulseForce);
            }
        }
    }
}
