using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Motor))]
public class LittleDoggoIA : MonoBehaviour
{
    GameObject target;
    Motor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<Motor>();
    }

    private void FixedUpdate()
    {
        var possibleTargets = Physics.OverlapSphere(this.transform.position, Constants.Enemies.LittleDoggo.ATTACK_RADIUS).Where(c => c.tag == Constants.Tags.PLAYER_TAG).ToList();
        Debug.Log(possibleTargets.Count());
        if (possibleTargets.Count() > 1){
            target = possibleTargets[0].gameObject;
            motor.Look(target.transform.position);
            motor.Move(target.transform.position - this.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, Constants.Enemies.LittleDoggo.ATTACK_RADIUS);
    }
}
