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

    public Bag Bag { get; private set; } = new Bag();
    private Chest _chest;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        PlayerContext.instance.player = this.gameObject;
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

        if (Input.GetButtonDown(Constants.Inputs.PLAYER_INTERACT) && _chest != null)
        {
            InteractWithChest();
        }
    }

    private void InteractWithChest()
    {
        if (_chest.IsOpened)
        {
            if (!_chest.IsLooted)
            {
                Item item = _chest.GetItem();
                Bag.AddItem(item);
            }
        }
        else
        {
            _chest.Open();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest = other.GetComponent<Chest>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest = null;
        }
    }
}
