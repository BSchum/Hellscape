using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using SDG.Platform.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Motor))]
public class Player : MonoBehaviour, IDamagable
{
    private Animator animator;
    private Motor motor;
    [SerializeField]
    Sword sword;
    public Sword Sword { get { return sword; } }

    public Bag Bag { get; private set; } = new Bag();
    private Chest _chest;
    public Stats stats;
    private float attackSpeed = 0.5f;
    private float lastAttack = 0.0f;

    public KeyBindSO binds;

    bool _isGrounded = true;
    bool _isOnSlope = false;

    public delegate void OnStatUpdate(Stats stats);
    public event OnStatUpdate OnStatUpdateEvent;

    private void Awake()
    {
        PlayerContext.instance.player = this.gameObject;
        animator = this.GetComponentInChildren<Animator>();
        motor = this.GetComponent<Motor>();
        Bag.OnAddItemEvent += Use;
        LoadAllStats();
    }

    void Use(Item item)
    {
        stats += item.bonusStats;
        LoadAllStats();
        UpdateStatsUI();
    }
    void LoadAllStats()
    {
        motor.speed = stats.Speed;
        sword.power = stats.Power;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = 0, vertical = 0;

        _isOnSlope = OnSlope();

        if (_isOnSlope)
        {
            motor.Move(Vector3.down * 15 * Time.deltaTime);
        }
        if (_isGrounded || _isOnSlope)
        {
            horizontal += Input.GetKey(binds.moveLeft) ? -1 : 0;
            horizontal += Input.GetKey(binds.moveRight) ? 1 : 0;
            vertical += Input.GetKey(binds.moveForward) ? 1 : 0;
            vertical += Input.GetKey(binds.moveBackward) ? -1 : 0;

            motor.Move(new Vector3(horizontal, 0, vertical));
        }
        
        motor.LookAtMouse();

        if (Input.GetKeyDown(binds.attack) && lastAttack + attackSpeed <= Time.time)
        {
            lastAttack = Time.time;
            animator.SetTrigger("sword hit");
        }

        if (Input.GetKeyDown(binds.interact) && _chest != null)
        {
            InteractWithChest();
        }
    }
    public void UpdateStatsUI()
    {
        OnStatUpdateEvent(stats);
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
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

    public void TakeDamage(uint amount)
    {
        stats.TakeDamage(amount);
        if(stats.Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("On est mort!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
        OnStatUpdateEvent(stats);
    }

    #region Triggers
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
    #endregion
}
