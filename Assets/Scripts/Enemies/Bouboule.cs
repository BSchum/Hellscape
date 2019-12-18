using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bouboule : Enemy
{
    public float chargeDuration = 2f;
    public Motor motor;
    public Rigidbody rb;
    public Animator animator;
    public float damage = 10;
    public float repulseForce = 10;

    float range;
    bool _isCharging;
    GameObject target;

    private void Start()
    {
        range = Constants.Rooms.ROOM_SIZE_Y / 4;
    }

    private void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, range).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
            if (targets.Count() > 0)
                this.target = targets.FirstOrDefault().gameObject;
        }
        else if (target != null)
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

        animator.SetBool("isCharging", true);

        motor.Look(target.transform.position);

        yield return new WaitForSeconds(chargeDuration);

        var dir = target.transform.position - transform.position;
        rb.AddForce(dir.normalized * motor.speed);

        _isCharging = false;

        animator.SetBool("isCharging", false);
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
