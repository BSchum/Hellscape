using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;

    public KeyCode forward = KeyCode.Z;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.Q;
    public KeyCode right = KeyCode.D;

    public KeyCode hitKey = KeyCode.Mouse0;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forward))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(backward))
        {
            this.transform.position += -this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(left))
        {
            this.transform.position += -this.transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(right))
        {
            this.transform.position += this.transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(hitKey))
        {
            animator.SetTrigger("sword hit");
        }

        var dir = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)).normalized;
        transform.LookAt(transform.position + new Vector3(dir.x, 0, dir.y));
    }
}
