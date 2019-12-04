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
    private Sword sword;
    public Bag Bag { get; private set; } = new Bag();
    private Chest _chest;

    private float attackSpeed = 0.5f;
    private float lastAttack = 0.0f;

    bool _isGrounded = true;
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
        if (_isGrounded)
        {
            motor.Move(new Vector3(Input.GetAxisRaw(Constants.Inputs.PLAYER_HORIZONTAL), 0, Input.GetAxisRaw(Constants.Inputs.PLAYER_VERTICAL)));
        }
        motor.LookAtMouse();

        if (Input.GetButtonDown(Constants.Inputs.PLAYER_HIT) && lastAttack + attackSpeed <= Time.time)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetButtonDown(Constants.Inputs.PLAYER_INTERACT) && _chest != null)
        {
            InteractWithChest();
        }
    }

    IEnumerator Attack()
    {
        lastAttack = Time.time;
        animator.SetTrigger("sword hit");
        sword.GetComponent<CapsuleCollider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0).Length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        sword.GetComponent<CapsuleCollider>().enabled = false;

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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.FLOOR_TAG || collision.gameObject.tag == Constants.Tags.BRIDGE_TAG)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.FLOOR_TAG || collision.gameObject.tag == Constants.Tags.BRIDGE_TAG)
        {
            _isGrounded = false;
        }
    }
}
