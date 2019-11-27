using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LittleDoggo : Enemy
{
    GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            var targets = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG);
            if (targets.Count() > 0)
                target = Physics.OverlapSphere(this.transform.position, 10).Where(c => c.tag == Constants.Tags.PLAYER_TAG).FirstOrDefault().gameObject;
        }
        else if(target != null)
        {
            transform.LookAt(target.transform);

            if ((target.transform.position - this.transform.position).magnitude > 3f)
            {
                GetComponent<Rigidbody>().AddForce((target.transform.position - this.transform.position).normalized * speed);
            }
        }

    }
}
