using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LittleDoggo : Enemy
{
    GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target != null )
        {
            transform.LookAt(target.transform);

            if ((target.transform.position - this.transform.position).magnitude > 2f)
            {
                GetComponent<Rigidbody>().AddForce((target.transform.position - this.transform.position).normalized * 35);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Constants.Tags.PLAYER_TAG)
        {
            target = other.gameObject;
        }
    }
}
