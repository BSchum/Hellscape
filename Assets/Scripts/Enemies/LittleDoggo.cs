using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LittleDoggo : Enemy
{
    public float speed = 10f;
    GameObject target;
    private void Start()
    {
        target = playerContext.player;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && playerContext.currentRoomNumber == roomNumber && room.doorsClosed)
        {
            transform.LookAt(target.transform);

            if ((target.transform.position - this.transform.position).magnitude > 3f)
            {
                GetComponent<Rigidbody>().AddForce((target.transform.position - this.transform.position).normalized * speed);
            }
        }

    }
}
