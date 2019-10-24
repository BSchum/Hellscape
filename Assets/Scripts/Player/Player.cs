using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motor))]
public class Player : MonoBehaviour
{
    private Animator animator;
    private Motor motor;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        motor = this.GetComponent<Motor>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.LookAtMouse();
        motor.Move(new Vector3(Input.GetAxisRaw(Constants.Inputs.PLAYER_HORIZONTAL), 0, Input.GetAxisRaw(Constants.Inputs.PLAYER_VERTICAL)));

        if (Input.GetButton(Constants.Inputs.PLAYER_HIT))
        {
            animator.SetTrigger("sword hit");
        }
    }

}
