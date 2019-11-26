using SDG.Unity.Scripts;
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
    float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
            if (targets.Count() > 0)
                target = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG).FirstOrDefault().gameObject;
        }
        else if (target != null)
        {
            
            var toTarget = (target.transform.position - transform.position).normalized;
            if (_isRotating)
            {
                //Pourchasse le player
                GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * speed));
            }

            if (Vector3.Distance(target.transform.position, this.transform.position) < attackRange)
            {
                //Si devant le joueur
                if (Vector3.Dot(toTarget, transform.forward) > 0 && !_isRotating)
                {
                    //On se prepare a taper
                    projector.enabled = true;
                    damageMultiplier = 1f;
                    GetComponent<Animator>().SetTrigger("Charge");
                    StartCoroutine(SmashAttack());

                }
                // Si derriere le joueur
                if (Vector3.Dot(toTarget, transform.forward) < 0 && !_isRotating)
                {
                    //On fait tourner et on suis
                    damageMultiplier = 2f;
                    _isRotating = true;
                    GetComponent<Animator>().SetBool("isRotating", _isRotating);
                    StartCoroutine(CircularAttack());
                }
            }

        }
    }

    IEnumerator SmashAttack()
    {
        yield return new WaitForSeconds(chargingTime);
        GetComponent<Animator>().SetTrigger("Attack");
        projector.enabled = false;
        var targets = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
        foreach(var target in targets)
        {
            target.GetComponent<Rigidbody>().AddExplosionForce(20f,this.transform.position, attackRange, 5f);
        }
    }

    IEnumerator CircularAttack()
    {
        yield return new WaitForSeconds(3f);
        _isRotating = false;
        GetComponent<Animator>().SetBool("isRotating", _isRotating);
    }
}
