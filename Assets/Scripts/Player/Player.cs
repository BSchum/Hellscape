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
        if (Input.GetKey(KeyCode.Z))
        {
            this.transform.position += this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += -this.transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.position += -this.transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += this.transform.right * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("sword hit");
        }

        //var rotationAngle = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(Input.mousePosition.x, rotationAngle.y, rotationAngle.z);
        //Vector3 dir = transform.rotation.eulerAngles;
        //dir.y = Input.mousePosition.normalized.y;
        //transform.Rotate(dir, Space.World);

        Vector3 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(lookDir);
        lookDir.y = transform.position.y;
        var angle = Quaternion.Angle(Quaternion.Euler(lookDir), transform.rotation);
        transform.rotation = Quaternion.AngleAxis(angle, transform.up);
    }
}
