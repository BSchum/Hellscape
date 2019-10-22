using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(Constants.Inputs.PLAYER_HORIZONTAL) > 0)
        {
            this.transform.position += this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetAxis(Constants.Inputs.PLAYER_HORIZONTAL) < 0)
        {
            this.transform.position += -this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetAxis(Constants.Inputs.PLAYER_VERTICAL) > 0)
        {
            this.transform.position += -this.transform.right * Time.deltaTime * speed;
        }
        if (Input.GetAxis(Constants.Inputs.PLAYER_VERTICAL) < 0)
        {
            this.transform.position += this.transform.right * Time.deltaTime * speed;
        }

        if (Input.GetButton(Constants.Inputs.PLAYER_HIT))
        {
            animator.SetTrigger("sword hit");
        }

        var dir = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0));
        transform.LookAt(transform.position + new Vector3(dir.x, 0, dir.y));
    }
}
