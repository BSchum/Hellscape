using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using SDG.Platform.Entities;
using UnityEngine;

[RequireComponent(typeof(Motor))]
public class Player : MonoBehaviour
{
    private Animator animator;
    private Motor motor;
    [SerializeField]
    private Camera camera;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        motor = this.GetComponent<Motor>();
        camera = Camera.main;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.BRIDGE_TAG) && !isMoving)
        {
            Debug.Log("Je deplace la camera");
            isMoving = true;
            MoveCamera(other.GetComponent<Bridge>().direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isMoving = false;
    }

    private void MoveCamera(Direction direction)
    {
        switch (direction)
        {
            case Direction.Top:
                camera.transform.Translate(new Vector3(-Constants.Rooms.ROOM_SIZE_X, 0, 0));
                break;
            case Direction.Bottom:
                camera.transform.Translate(new Vector3(Constants.Rooms.ROOM_SIZE_X, 0, 0));
                break;
            case Direction.Right:
                camera.transform.Translate(new Vector3(0, 0, -Constants.Rooms.ROOM_SIZE_Y));
                break;
            case Direction.Left:
                camera.transform.Translate(new Vector3(0, 0, Constants.Rooms.ROOM_SIZE_Y));
                break;
            default:
                break;
        }
    }
}
